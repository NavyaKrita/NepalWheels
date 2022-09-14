using Nop.Core.Domain.Notice;
using Nop.Web.Models.Notice;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public partial interface INoticeBoardModelFactory
    {      
        Task<NoticeBoardModel> PrepareNoticeModelAsync();
    }
}
