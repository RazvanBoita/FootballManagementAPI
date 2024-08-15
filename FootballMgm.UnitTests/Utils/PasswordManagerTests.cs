using FootballMgm.Api.Utils;

namespace FootballMgm.UnitTests;
public class PasswordManagerTests
{
    [Fact]
    //Todo verifica daca hash-ul generat nu e null, are 64 caractere, are only lowercase
    public void HashValueIsValid()
    {
        //TODO Arrange
        var testPassword = "ParolaDeTest123!";

        //TODO Act
        var generatedHash = PasswordManager.HashPassword(testPassword);

        //TODO Assert
        Assert.NotNull(generatedHash);
        Assert.Equal(64, generatedHash.Length);
        Assert.Matches("^[a-f0-9]+$", generatedHash);
    }

    [Fact]
    public void HashShouldBeDifferentForDifferentPasswords()
    {
        //TODO Arrange
        const string password1 = "ParolaDeTest123!";
        const string password2 = "ParolaDeTest123?";
        const string password3 = "paroladetest123!";
        const string password4 = "salut";
        const string password5 = "PAROLADETEST123?";

        
        //TODO Act
        var generatedHashForPassword1 = PasswordManager.HashPassword(password1);
        var generatedHashForPassword2 = PasswordManager.HashPassword(password2);
        var generatedHashForPassword3 = PasswordManager.HashPassword(password3);
        var generatedHashForPassword4 = PasswordManager.HashPassword(password4);
        var generatedHashForPassword5 = PasswordManager.HashPassword(password5);

        var arrOfGeneratedHashes = new []{generatedHashForPassword1, generatedHashForPassword2, generatedHashForPassword3, generatedHashForPassword4, generatedHashForPassword5};
        
        //TODO Assert
        Assert.Equal(arrOfGeneratedHashes.Length, arrOfGeneratedHashes.Distinct().Count());
    }

    [Fact]
    public void VerifyPasswordShouldReturnCorrectResult()
    {
        //TODO Arrange
        const string correctPassword = "parolamea123";
        const string incorrectPassword = "altaparola123";
        
        //TODO Act
        var hash = PasswordManager.HashPassword(correctPassword);
        var shouldBeTrueResult = PasswordManager.VerifyPassword(correctPassword, hash);
        var shouldBeFalseResult = PasswordManager.VerifyPassword(incorrectPassword, hash);
        
        //TODO Assert
        Assert.True(shouldBeTrueResult);
        Assert.False(shouldBeFalseResult);
        
    }
}