namespace MarketplaceAPI.Domain.Listings;

public class Listing
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int Views { get; set; } = 0;
}