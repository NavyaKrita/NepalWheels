using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Blogs
{
    public partial record BlogLikeModel : BaseNopEntityModel
    {
        public int CustomerId { get; set; }

        public bool IsLike { get; set; }

        /// <summary>
        /// Gets or sets the blog post identifier
        /// </summary>
        public int BlogPostId { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
