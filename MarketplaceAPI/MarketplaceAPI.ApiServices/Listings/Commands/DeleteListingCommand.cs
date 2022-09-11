using MarketplaceAPI.Infrastructure.Persistence.Context;
using MediatR;

namespace MarketplaceAPI.ApiServices.Listings.Commands; 

public class DeleteListingCommand : IRequest<Unit>
{
    public readonly int _id;

    public DeleteListingCommand(int id)
    {
        _id = id;
    }
}

public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand>
{
    private readonly SqlDbContext _dbContext;

    public DeleteListingCommandHandler(SqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
    {
        var listing = await _dbContext.Listings.FindAsync(request._id);

        if (listing == null)
        {
            throw new Exception("Listing was not found");
        }
        else
        {
            _dbContext.Listings.Remove(listing);
        
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return Unit.Value;
        }
    }
}