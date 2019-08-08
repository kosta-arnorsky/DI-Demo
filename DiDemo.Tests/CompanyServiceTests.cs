using DiDemo.Services.CompanyServices;
using Moq;
using Xunit;

namespace DiDemo.Tests
{
    public class CompanyServiceTests
    {
        [Fact]
        public void NormalizesNameCaseIfCompanyFound()
        {
            var repositoryMock = new Mock<ICompanyRepository>();
            repositoryMock
                .Setup(r => r.GetCompany(123))
                .Returns(new Company
                {
                    Name = "TEST"
                });

            // BOOKMARK: 2 mocking easier
            var service = new CompanyService(repositoryMock.Object);
            var company = service.GetCompany(123);

            Assert.NotNull(company);
            Assert.Equal("Test", company.Name);
        }

        [Fact]
        public void ReturnsNullIfCompanyNotFound()
        {
            var repositoryMock = new Mock<ICompanyRepository>();

            var service = new CompanyService(repositoryMock.Object);
            var company = service.GetCompany(123);

            Assert.Null(company);
        }
    }
}
