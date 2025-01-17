﻿using Nop.Core.Domain.Vendors;
using Nop.Web.Areas.Admin.Models.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public partial interface INkVendorLocationModelFactory
    {
        #region NkVendorLocations
        Task<NkVendorLocationSearchModel> PrepareNkVendorLocationSearchModelAsync(NkVendorLocationSearchModel searchModel);
        Task<NkVendorLocationListModel> PrepareNkVendorLocationListModelAsync(NkVendorLocationSearchModel searchModel);
        Task<CreateVendorLocationModel> PrepareNkVendorLocationModelAsync(CreateVendorLocationModel model, NkVendorLocation vendor, bool excludeProperties = false);
        Task<NkVendorLocationSearchModel> PrepareNkVendorLocationAdminSearchModelAsync(NkVendorLocationSearchModel searchModel);
        #endregion
    }
}
