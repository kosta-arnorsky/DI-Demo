namespace DiDemo.Services.CompanyServices
{
    public interface ICompanyRepository
    {
        Company GetCompany(long id);

        Company FindCompany(string name);
    }
}