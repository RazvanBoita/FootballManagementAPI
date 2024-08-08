using FootballMgm.Api.Controllers;
using FootballMgm.Api.Data;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;
using FootballMgm.Api.Repositories;
using FootballMgm.Api.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Services;

public class AuthService
{
    private readonly IAppUserRepository _repository;
    private readonly JwtService _jwtService;
    
    public AuthService(IAppUserRepository repository, JwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }
    
    public (string? jwt, bool Success, string Message) Login(AuthDto loginDto)
    {
        var foundUser = _repository.GetUserByUsername(loginDto.Username);
        if (foundUser is null)
        {
            return (null, false, "Username doesn't exist");
        }

        if (!PasswordManager.VerifyPassword(loginDto.Password, foundUser.PasswordHash))
        {
            return (null, false, "Credentials are wrong");
        }
        
        //TODO issue jwt
        var token = _jwtService.GenerateToken(_repository.GetUserByUsername(loginDto.Username));
        return (token, true, "You are logged in!");
    }

    public async Task<(bool Sucess, string Message)> Signup(AuthDto signupDto)
    {
        var foundUser = _repository.GetUserByUsername(signupDto.Username);
        if (foundUser is not null)
        {
            return (false, "Username already exists");
        }
    
        try
        {
            await _repository.InsertUserAsync(new AppUser
            {
                Username = signupDto.Username,
                PasswordHash = PasswordManager.HashPassword(signupDto.Password),
                Role = "Base"
            });
            
            return (true, "User is now signed up. Proceed to login.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
}