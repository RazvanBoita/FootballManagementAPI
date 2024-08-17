using FluentValidation;
using FootballMgm.Api.Dtos;

namespace FootballMgm.Api.Validators.FootballValidators;

public class AuthValidator : AbstractValidator<AuthDto>
{
    private const int UsernameMinLength = 5;
    private const int UsernameMaxLength = 18;
    private const int PasswordMinLength = 6;
    private const int PasswordMaxLength = 25;
    public AuthValidator()
    {
        RuleFor(a => a.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(a => a.Username)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage($"Username must be between {UsernameMinLength} and {UsernameMaxLength} characters");
        
        RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(a => a.Password)
            .Length(PasswordMinLength, PasswordMaxLength).WithMessage($"Password must be between {PasswordMinLength} and {PasswordMaxLength} characters");
        
    }
}