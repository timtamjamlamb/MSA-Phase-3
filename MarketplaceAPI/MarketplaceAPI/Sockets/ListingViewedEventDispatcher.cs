using MarketplaceAPI.ApiServices.Listings.Queries;
using MarketplaceAPI.Domain.Listings.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace MarketplaceAPI.Sockets;

public class ListingEventsClientDispatcher : INotificationHandler<ListingViewedEvent>
{
    private readonly IHubContext<ListingEventsClientHub> _hubContext;

    public ListingEventsClientDispatcher(IHubContext<ListingEventsClientHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task Handle(ListingViewedEvent @event, CancellationToken cancellationToken)
    {
        return _hubContext.Clients.All.SendAsync("listingViewed", @event, cancellationToken);
    }
}