using Nop.Core.Domain.Notice;
using Nop.Web.Models.Notice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public partial interface INoticeBoardModelFactory
    {
        Task<IEnumerable<NoticeBoardModel>> PrepareNoticeModelAsync();
        Task<NoticeBoardModel> PrepareNoticeModelAsync(int id);
        Task<IEnumerable<NoticeBoardModel>> PrepareShowInOtherPagePopUpModelAsync(string url);
        Task<NoticeBoardModel> PrepareShowIntervalModelAsync(string url);
    }
}
