using FluentValidation;
using MarketplaceAPI.ApiServices.Listings.Commands;

namespace MarketplaceAPI.ApiServices.Listings.Validation;

public class DeleteListingValidator : AbstractValidator<DeleteListingCommand>
{
    public DeleteListingValidator()
    {
        RuleFor(x => x._id).NotEmpty();
    }
}