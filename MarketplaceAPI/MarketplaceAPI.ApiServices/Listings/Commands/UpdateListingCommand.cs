using MarketplaceAPI.Domain.Listings.DTO;
using MarketplaceAPI.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceAPI.ApiServices.Listings.Commands;

public class UpdateListingCommand : IRequest<Unit>
{
    public readonly int _id;
    public readonly UpdateListingDto _dto;

    public UpdateListingCommand(int id, UpdateListingDto dto)
    {
        _id = id;
        _dto = dto;
    }
}

public class UpdateListingHandler : IRequestHandler<UpdateListingCommand>
{
    private readonly SqlDbContext _context;

    public UpdateListingHandler(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
    {
        var listing = await _context.Listings.FirstOrDefaultAsync(x => x.Id == request._id, cancellationToken: cancellationToken);
        
        if (listing is null)
        {
            throw new Exception("Listing was not found");
        }
        
        listing.Name = request._dto.Name;
        listing.Description = request._dto.Description;
        listing.Price = request._dto.Price;
        listing.ImageUrl = request._dto.ImageUrl;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}