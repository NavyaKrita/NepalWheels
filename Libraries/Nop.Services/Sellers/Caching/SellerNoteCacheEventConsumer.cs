using Nop.Core.Domain.Vendors;
using Nop.Services.Caching;

namespace Nop.Services.Sellers.Caching
{
    /// <summary>
    /// Represents a seller note cache event consumer
    /// </summary>
    public partial class SellerNoteCacheEventConsumer : CacheEventConsumer<VendorNote>
    {
    }
}
