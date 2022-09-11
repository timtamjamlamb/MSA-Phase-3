using MarketplaceAPI.Domain.Listings;
using MarketplaceAPI.Domain.Listings.DTO;
using MarketplaceAPI.Infrastructure.Persistence.Context;
using MediatR;

namespace MarketplaceAPI.ApiServices.Listings.Commands;

public class CreateListingCommand : IRequest
{
    public readonly CreateListingDto _dto;

    public CreateListingCommand(CreateListingDto dto)
    {
        _dto = dto;
    }
}

public class CreateListingHandler : IRequestHandler<CreateListingCommand, Unit>
{
    private readonly SqlDbContext _context;
    private readonly IMediator _mediator;

    public CreateListingHandler(SqlDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreateListingCommand request, CancellationToken cancellationToken)
    {
        var dto = request._dto;
        
        var listing = new Listing
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl
        };
        
        await _context.Listings.AddAsync(listing, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}