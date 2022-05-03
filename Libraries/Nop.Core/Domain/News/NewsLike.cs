using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.News
{
   public class NewsLike : BaseEntity
    {
        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        public bool IsLike { get; set; }

        /// <summary>
        /// Gets or sets the blog post identifier
        /// </summary>
        public int NewsId { get; set; }
        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
    
}
