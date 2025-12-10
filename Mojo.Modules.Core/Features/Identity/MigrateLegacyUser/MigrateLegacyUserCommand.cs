using FluentValidation;

namespace Mojo.Modules.Core.Features.Identity.MigrateLegacyUser;

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