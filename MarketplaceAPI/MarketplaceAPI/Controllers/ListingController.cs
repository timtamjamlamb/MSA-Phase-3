using MarketplaceAPI.ApiServices.Listings.Commands;
using MarketplaceAPI.ApiServices.Listings.Queries;
using MarketplaceAPI.Domain.Listings.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceAPI.Controllers;

[Route("listings")]
[ApiController]
public class ListingController : ControllerBase
{
    private readonly IMediator _mediator;

    public ListingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetListings()
    {
        return Ok(await _mediator.Send(new GetListingsQuery()));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetListingById(int id)
    {
        return Ok(await _mediator.Send(new GetListingQuery(id)));
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateListing([FromBody] CreateListingDto listing)
    {
        var unit = await _mediator.Send(new CreateListingCommand(listing));

        return StatusCode(201);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateListing(int id, [FromBody] UpdateListingDto listing)
    {
        var unit = await _mediator.Send(new UpdateListingCommand(id, listing));

        return StatusCode(204);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteListing(int id)
    {
        await _mediator.Send(new DeleteListingCommand(id));

        return StatusCode(204);
    }
}