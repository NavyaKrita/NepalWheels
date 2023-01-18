using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Sellers
{

    public enum GenreType
    {
        [Description("Service Center")]
        ServiceCenter = 1,
        [Description("Spare Parts")]
        SpareParts = 2,
        [Description("Show Room")]
        ShowRoom = 3
    }

}
