using FluentValidation;

namespace Mojo.Modules.Identity.Features.GetLegacyUser;

public record GetLegacyUserQuery(string Email, Guid SiteGuid)
{
    public class GetLegacyUserQueryValidator : AbstractValidator<GetLegacyUserQuery>
    {
        public GetLegacyUserQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.");
            RuleFor(x => x.SiteGuid).NotEmpty().WithMessage("SiteGuid cannot be empty.");
        }
    }
}