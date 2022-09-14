using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Notice
{
    public partial class NoticeBoard : BaseEntity
    {
        /// Gets or sets the Notice
        /// </summary>
        public string Notice { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the PublishedFrom
        /// </summary>
        public DateTime PublishedFrom { get; set; }

        public DateTime PublishedTo { get; set; }
        public bool DisplayForm { get; set; }

        public string ThankYou { get; set; }
        public DateTime CreatedOnUtc { get; set; }

    }
}
