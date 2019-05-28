using System.Globalization;

namespace DiDemo.Services.CompanyServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public Company GetCompany(long id)
        {
            var company = _repository.GetCompany(id);
            if (company != null)
            {
                company.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(company.Name.ToLower());
            }

            return company;
        }
    }
}
