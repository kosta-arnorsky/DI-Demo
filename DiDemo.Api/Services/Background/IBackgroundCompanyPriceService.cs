namespace DiDemo.Api.Services.Background
{
    public interface IBackgroundCompanyPriceService
    {
        void ScheduleRetrievePrice(long companyId);
    }
}