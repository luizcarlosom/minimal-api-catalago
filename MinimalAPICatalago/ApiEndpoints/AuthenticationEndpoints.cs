using Microsoft.AspNetCore.Authorization;
using MinimalAPICatalago.Models;
using MinimalAPICatalago.Services;

namespace MinimalAPICatalago.ApiEndpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticatorEndpoints(this WebApplication app)
    {
        app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
        {
            if (userModel == null) return Results.BadRequest("Login Inválido");

            if (userModel.UserName == "luizmaciel" && userModel.Password == "numsey#123")
            {
                var tokenString = tokenService.TokenGenerate(app.Configuration["Jwt:Key"],
                    app.Configuration["Jwt:Issuer"],
                    app.Configuration["Jwt:Audience"],
                    userModel);

                return Results.Ok(new { token = tokenString });
            }
            else
            {
                return Results.BadRequest("Login inválido");
            }
        }).Produces(StatusCodes.Status400BadRequest)
                    .Produces(StatusCodes.Status200OK)
                    .WithName("Login")
                    .WithTags("Autenticação");
    }
}
