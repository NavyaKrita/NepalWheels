using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Notice
{
    public partial record NoticeBoardDetailListModel : BasePagedListModel<NoticeBoardDetailModel>
    {
    }
    public partial record ParticipantsCreateModel : NoticeBoardDetailModel
    {
        public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public int ManufacturerId { get; set; }
        public string ProductId { get; set; }
    }
}
