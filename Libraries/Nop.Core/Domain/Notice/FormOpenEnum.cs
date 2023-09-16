using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Notice
{
    public enum FormPopUpType
    {
        [Description( "Pop Up only in Home page")]
        PopUpOnlyInHome = 1,
        [Description("Pop Up in chosen pages")]
        PopUpInDefinePages = 2,
        [Description("Display PopUp in any pages")]
        PopUpInAnyPages = 3
    }
}
