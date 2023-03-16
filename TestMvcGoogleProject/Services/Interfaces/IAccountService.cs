using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TestMvcGoogleProject.Data.Entity;

namespace TestMvcGoogleProject.Services.Interfaces;

public interface IAccountService
{
    Task<string[]> HandleGoogleCallback();

    Task Logout();
    
    AuthenticationProperties GetLoginProperties(string url);
}