using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.District
{
    public record DistrictModel: BaseNopEntityModel
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }

    }
}
