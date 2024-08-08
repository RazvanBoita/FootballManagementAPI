using System.ComponentModel.DataAnnotations;

namespace FootballMgm.Api.Validators.FootballValidators;

public class ValidPreferredFootAttribute : ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return $"Preferred foot has to be R(right) or L(left) ";
    }

    public override bool IsValid(object? value)
    {
        var foot = value ?? "longvalue";
        Console.WriteLine(foot);
        char[] correctOptions = {'r', 'l', 'R', 'L'};
        return correctOptions.Contains((char) foot);
    }
}