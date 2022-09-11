using MarketplaceAPI.Domain.Listings;
using MarketplaceAPI.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceAPI.ApiServices.Listings.Queries;

public class GetListingsQuery : IRequest<IEnumerable<Listing>>
{

}

public class GetListingsHandler : IRequestHandler<GetListingsQuery, IEnumerable<Listing>>
{
    private readonly SqlDbContext _context;

    public GetListingsHandler(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Listing>> Handle(GetListingsQuery request, CancellationToken cancellationToken)
    {
        var listings = await _context.Listings.ToListAsync(cancellationToken: cancellationToken);
        return listings;
    }
}