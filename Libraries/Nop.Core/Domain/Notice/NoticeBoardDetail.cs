using System;


namespace Nop.Core.Domain.Notice
{
    public partial class NoticeBoardDetail : BaseEntity
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
      
        public string Address { get; set; }
   
        public string BikeName { get; set; }
        public string CC { get; set; }

        public int? NoticeBoardId { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public string Notice { get; set; }
        public string Category { get; set; }
        public string LeadGenerate { get; set; }

        public string Products { get; set; }
        public int ManufacturerId { get; set; }
    }
}
