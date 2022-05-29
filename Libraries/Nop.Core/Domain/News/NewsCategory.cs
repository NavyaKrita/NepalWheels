using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.News
{

    public partial class SS_RB_Category : BaseEntity, ISlugSupported, IStoreMappingSupported
    {
        public int LanguageId { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }
        public string SEName { get; set; }
        public bool LimitedToStores { get; set; }

    }
}
