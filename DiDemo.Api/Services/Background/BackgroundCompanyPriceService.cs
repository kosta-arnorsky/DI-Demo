using DiDemo.Abstractions;
using DiDemo.Formatting;
using DiDemo.Services.CompanyServices;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace DiDemo.Api.Services.Background
{
    public class BackgroundCompanyPriceService : IBackgroundCompanyPriceService
    {
        private readonly BackgroundWorkQueue _backgroundWorkQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundCompanyPriceService(
            BackgroundWorkQueue backgroundWorkQueue,
            IServiceScopeFactory serviceScopeFactory)
        {
            _backgroundWorkQueue = backgroundWorkQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ScheduleRetrievePrice(long companyId)
        {
            _backgroundWorkQueue.Enqueue((c) => new RetrievePriceOperation(companyId, _serviceScopeFactory, c).Run());
        }

        private class RetrievePriceOperation
        {
            private readonly long _companyId;
            private readonly IServiceScopeFactory _serviceScopeFactory;
            private readonly CancellationToken _cancellationToken;

            public RetrievePriceOperation(long companyId, IServiceScopeFactory serviceScopeFactory, CancellationToken cancellationToken)
            {
                _companyId = companyId;
                _serviceScopeFactory = serviceScopeFactory;
                _cancellationToken = cancellationToken;
            }

            public async Task Run()
            {
                // A long operation
                await Task.Delay(3333, _cancellationToken);

                // Example 6: scope
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var companyPriceProvider = scope.ServiceProvider.GetService<ICompanyPriceProvider>();
                    var logger = scope.ServiceProvider.GetService<ILogger>();

                    var companyPrice = companyPriceProvider.GetPrice(_companyId);
                    if (companyPrice == null)
                    {
                        logger.Log($"Price for the company ID {_companyId} is not found.");
                    }
                    else
                    {
                        logger.Log(companyPrice.ToStringFormat());
                    }
                }
            }
        }
    }
}
