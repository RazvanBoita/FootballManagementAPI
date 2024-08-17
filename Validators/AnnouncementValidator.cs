using FluentValidation;
using FootballMgm.Api.Dtos;

namespace FootballMgm.Api.Validators.FootballValidators;

public class AnnouncementValidator : AbstractValidator<AnnouncementDto>
{
    private const int MinLength = 10;
    private const int MaxLength = 350;
    public AnnouncementValidator()
    {
        RuleFor(a => a.Content)
            .NotEmpty()
            .WithMessage("Content cannot be empty.");
        
        RuleFor(a => a.Content)
            .Length(MinLength, MaxLength)
            .WithMessage($"Content must be between {MinLength} and {MaxLength} characters.");
        
    }
}