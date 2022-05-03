using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
namespace Nop.Web.Models.News
{
  
    public partial record NewsLikeModel : BaseNopEntityModel
    {
        public int CustomerId { get; set; }

        public bool IsLike { get; set; }

        /// <summary>
        /// Gets or sets the blog post identifier
        /// </summary>
        public int NewsId { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
