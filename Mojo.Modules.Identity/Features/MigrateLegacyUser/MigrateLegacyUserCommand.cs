using FluentValidation;

namespace Mojo.Modules.Identity.Features.MigrateLegacyUser;

public record MigrateLegacyUserCommand(string OldPassword)
{
    public class MigrateLegacyUserCommandValidator : AbstractValidator<MigrateLegacyUserCommand>
    {
        public MigrateLegacyUserCommandValidator()
        {
            RuleFor(x => x.OldPassword).NotNull().WithMessage("Password cannot be empty.");
        }
    }
}