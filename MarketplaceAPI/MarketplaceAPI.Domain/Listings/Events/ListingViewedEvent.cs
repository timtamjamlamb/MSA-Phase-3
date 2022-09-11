using MediatR;

namespace MarketplaceAPI.Domain.Listings.Events;

public class ListingViewedEvent : INotification
{
    public Listing Listing { get; set; }
}