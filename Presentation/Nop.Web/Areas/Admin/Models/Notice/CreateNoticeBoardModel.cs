using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Notice
{
    public partial record  CreateNoticeBoardModel: NoticeBoardModel
    {
   
        public IList<SelectListItem> Manufacturers { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();
      
    }
}
