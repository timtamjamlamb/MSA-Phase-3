using FluentValidation;
using MarketplaceAPI.ApiServices.Listings.Commands;

namespace MarketplaceAPI.ApiServices.Listings.Validation;

public class CreateListingValidator : AbstractValidator<CreateListingCommand>
{
    public CreateListingValidator()
    {
        RuleFor(t => t._dto.Name).NotEmpty().MaximumLength(255);
        
        RuleFor(t => t._dto.Description).MaximumLength(1000);
        
        RuleFor(t => t._dto.Price).NotEmpty().GreaterThan(0);
    }
}