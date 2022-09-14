using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Notice
{
    public partial class NoticeBoardDetail : BaseEntity
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string City { get; set; }

        public int? NoticeBoardId { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public string Notice { get; set; }
    }
}
