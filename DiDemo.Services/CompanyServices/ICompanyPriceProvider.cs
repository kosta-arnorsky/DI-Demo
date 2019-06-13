namespace DiDemo.Services.CompanyServices
{
    public interface ICompanyPriceProvider
    {
        CompanyPrice GetPrice(long id);
    }
}