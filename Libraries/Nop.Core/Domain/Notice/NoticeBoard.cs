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
        public string TermsAndCondition { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public bool Name { get; set; }
       
        public bool PhoneNumber { get; set; }
        
        public bool EmailAddress { get; set; }
      
        public bool Age { get; set; }
      
        public bool Address { get; set; }
       
        public bool City { get; set; }
        
        public bool BikeName { get; set; }
       
        public bool CC { get; set; }

    }
}
