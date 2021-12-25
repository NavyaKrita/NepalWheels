using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.District
{
    public class DistrictModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }

    }
}
