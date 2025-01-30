using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequest request)
    {
        return string.Empty;
        //// Step 1: Validate the username/password
        //var user = ValidateCredentials(request.UserName, request.Password);

        //if (user is null)
        //{
        //    return Unauthorized();
        //}

        //// Step 2: create token
        //var securityKey = _configuration.GetIssuerSigningKey();
        //var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //var claims = new List<Claim>
        //{
        //    new ("sub", user.Id.ToString()),
        //    new ("given_name", user.FirstName),
        //    new ("family_name", user.LastName),
        //    new ("preferred_username", user.UserName)
        //};

        //var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _configuration.GetJwtIssuer(),
        //        audience: _configuration.GetJwtAudience(),
        //        claims,
        //        notBefore: DateTime.UtcNow,
        //        expires: DateTime.UtcNow.AddSeconds(10),
        //        signInCredentials);

        //var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        //return Ok(tokenToReturn);
    }

    //private ApplicationUser? ValidateCredentials(string requestUserName, string requestPassword)
    //{
    //    var passwordHasher = new PasswordHasher<ApplicationUser>();

    //    string password = "SecurePassword123";

    //    string hashedPassword = passwordHasher.HashPassword(null, password);
    //    Console.WriteLine($"Hashed Password: {hashedPassword}");

    //    // Verify password
    //    PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);

    //    if (requestUserName.ToLower() == "test@mail.com" && requestPassword == "123")
    //    {
    //        return new ApplicationUser()
    //        {
    //            Id = 1,
    //            FirstName = "First",
    //            LastName = "Last",
    //            UserName = "test@mail.com"
    //        };
    //    }

    //    return null;
    //}
}

//public sealed class ApplicationUser
//{
//    public int Id { get; set; }
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//    public string UserName { get; set; }
//}

public sealed class AuthenticationRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}