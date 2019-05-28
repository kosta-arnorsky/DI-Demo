namespace DiDemo.Services.CompanyServices
{
    public interface ICompanyPriceProvider
    {
        CompanyPrice GetCompany(long id);
    }
}