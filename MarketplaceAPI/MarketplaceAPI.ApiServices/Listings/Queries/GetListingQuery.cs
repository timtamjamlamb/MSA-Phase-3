using MarketplaceAPI.Domain.Listings;
using MarketplaceAPI.Domain.Listings.Events;
using MarketplaceAPI.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace MarketplaceAPI.ApiServices.Listings.Queries;

public class GetListingQuery : IRequest<Listing>
{
    public readonly int _id;

    public GetListingQuery(int id)
    {
        _id = id;
    }
}

public class GetListingHandler : IRequestHandler<GetListingQuery, Listing>
{
    private readonly SqlDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMemoryCache _cache;

    public GetListingHandler(SqlDbContext context, IMediator mediator, IMemoryCache cache)
    {
        _context = context;
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<Listing> Handle(GetListingQuery request, CancellationToken cancellationToken)
    {

        if (_cache.TryGetValue(request._id, out Listing listing))
        {
            await IncrementViews(cancellationToken, listing);
            return listing;
        }
        
        listing = await _context.Listings.FindAsync(request._id);

        if (listing == null)
        {
            throw new Exception("Listing was not found");
        }
        
        await IncrementViews(cancellationToken, listing);

        CacheListing(request._id,listing);
        
        await _mediator.Publish(new ListingViewedEvent(){Listing = listing}, cancellationToken);

        return listing;
    }
    
    private async Task IncrementViews(CancellationToken cancellationToken1, Listing value)
    {
        value.Views++;
        await _context.SaveChangesAsync(cancellationToken1);
    }
    
    private void CacheListing(int id,Listing listing)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1024);
            
        _cache.Set(id, listing, cacheEntryOptions);
    }
}