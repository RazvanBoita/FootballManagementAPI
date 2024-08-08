using System.ComponentModel.DataAnnotations;

namespace FootballMgm.Api.Validators.FootballValidators;

public class ValidShirtNoAttribute : ValidationAttribute
{
    public override bool IsValid(object? shirtNo)
    {
        return (int?) shirtNo is not null and >= 1 and <= 99;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The shirt number has to be between 1 and 99";
    }
}