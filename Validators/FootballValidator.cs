using FluentValidation;
using FootballMgm.Api.Dtos;


namespace FootballMgm.Api.Validators.FootballValidators;

public class FootballValidator : AbstractValidator<FootballerDto>
{
    private readonly ICollection<char> _preferredFootPossibilities = ['r', 'l', 'R', 'L'];
    public FootballValidator()
    {
        //TODO asta va merge dupa ce fac migrarea in care convertesc PreferredFoot in string 
        //Din model binding il ia ca string, nu l poate transforma in char si de aia da exceptia aia de Can't convert bla bla
        RuleFor(f => f.PreferredFoot)
            .Custom((preferredFoot, context) =>
            {
                if (preferredFoot == '\0')
                {
                    context.AddFailure("PreferredFoot", "Preferred foot is required.");
                }

                var input = preferredFoot.ToString();
                
                if (input.Length != 1)
                {
                    context.AddFailure("PreferredFoot", "Preferred foot must be exactly one character.");
                }
            });
            
        RuleFor(f => f.PreferredFoot)
            .Must(f => _preferredFootPossibilities.Contains(f))
            .WithMessage("Football preferred foot must be r/l/R/L");
        
        
        RuleFor(f => f.ShirtNumber)
            .InclusiveBetween(1, 99)
            .WithMessage("Shirt number must be between 1 and 99");
    }
}