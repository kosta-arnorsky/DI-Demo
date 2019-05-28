using DiDemo.Services.CompanyServices;

namespace DiDemo.Api.Extensions
{
    public static class CompanyPriceExtension
    {
        public static string ToStringFormat(this CompanyPrice companyPrice)
        {
            string price = companyPrice.Price.HasValue
                ? companyPrice.Price.Value.ToString("C")
                : "N/A";
            return $"{companyPrice.Name}: {price}";
        }
    }
}
