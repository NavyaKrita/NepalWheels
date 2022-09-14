using Nop.Core.Domain.Notice;
using Nop.Web.Areas.Admin.Models.Notice;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public partial interface INoticeBoardModelFactory
    {
        Task<NoticeBoardSearchModel> PrepareNoticeBoardSearchModelAsync(NoticeBoardSearchModel searchModel);
       
        Task<NoticeBoardListModel> PrepareNoticeBoardListModelAsync(NoticeBoardSearchModel searchModel);
        CreateNoticeBoardModel PrepareNoticeBoardModelAsync(CreateNoticeBoardModel model, NoticeBoard noticeBoard, bool excludeProperties = false);

        Task<NoticeBoardDetailSearchModel> PrepareParticipantsSearchModelAsync(NoticeBoardDetailSearchModel searchModel);
        Task<NoticeBoardDetailListModel> PrepareParticipantsListModelAsync(NoticeBoardDetailSearchModel searchModel);
    }
}
