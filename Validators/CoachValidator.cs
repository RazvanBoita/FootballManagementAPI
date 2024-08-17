using FluentValidation;
using FootballMgm.Api.Dtos;

namespace FootballMgm.Api.Validators.FootballValidators;

public class CoachValidator : AbstractValidator<CoachDto>
{
    public CoachValidator()
    {
        RuleFor(c => c.TeamName).NotEmpty().WithMessage("Team name cannot be empty");
        RuleFor(c => c.XpYears).NotEmpty().WithMessage("Xp years cannot be empty");
        RuleFor(c => c.XpYears).InclusiveBetween(0,51).WithMessage("Xp years must be between 0 and 50");
    }
}