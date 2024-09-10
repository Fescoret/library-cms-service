using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KbAis.Intern.Library.Service.Web.Data.Models;

public class User
{
    public Guid Id { get; protected init; }

    public string FirstName { get; protected set; } = null!;

    public string LastName { get; protected set;} = null!;

    public string Email { get; protected set; } = null!;

    public string Password { get; protected set; } = null!;

    public string Role { get; protected set; } = null!;

    public ICollection<Guid> Orders { get; protected set; } = null!;

    public bool IsConscious { get; protected set; }

    protected User() {

    }

    /*public class UserEmail
    {
        public string Email { get; protected set; } = null!;

        public static Result<UserEmail> Create(string emailAddress)
        {
            return Result.Success(new UserEmail() { Email = emailAddress });
        }
    }*/

    public static Result<User> Create(string email, string password, string firstName, string lastName)
    {
        var isPropertiesCorrect = Result.Combine(
            Result.SuccessIf(string.IsNullOrEmpty(email) == false, "Email is requeried"),
            Result.SuccessIf(string.IsNullOrEmpty(password) == false, "Password is requeried"),
            Result.SuccessIf(string.IsNullOrEmpty(firstName) == false, "FirstName is requeried"),
            Result.SuccessIf(string.IsNullOrEmpty(lastName) == false, "LastName is requeried")
        );

        return isPropertiesCorrect.Map(() =>
            new User() { FirstName = firstName, LastName = lastName, Email = email, Password = password, Role = "user", IsConscious = true }
        );
    }

    public bool Login(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password), "User password should not be null or empty");
        }

        if (Password == password)
        {
            return true;
        }

        return false;
    }

    public string CreateToken()
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("firstName", FirstName),
            new Claim("lastName", LastName),
            new Claim("email", Email),
            new Claim(ClaimTypes.Role, Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my security string"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                issuer: "me",
                audience: "you",
                signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public Result<User> UpdateUser(string email, string password, string firstName, string lastName)
    {
        var isPropertiesCorrect = Result.Combine(
            Result.SuccessIf(string.IsNullOrEmpty(email) == false, "Email is requeried"),
            Result.SuccessIf(string.IsNullOrEmpty(password) == false, "Password is requeried"),
            Result.SuccessIf(string.IsNullOrEmpty(firstName) == false, "FirstName is requeried"),
            Result.SuccessIf(string.IsNullOrEmpty(lastName) == false, "LastName is requeried")
        );
                
        return isPropertiesCorrect.Map(() =>
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            return this;
        });
    }

    public User ChangeRole(string role)
    {
        if (string.IsNullOrEmpty(role))
        {
            throw new ArgumentNullException(nameof(role), "User role should not be null or empty");
        }

        Role = role;
        return this;
    }

    public User ChangeConscious()
    {
        IsConscious = !IsConscious;
        return this;
    }

    public Result<User> AddActiveOrder(Guid orderId)
    {
        if (Orders.Count < 5)
        {
            if (IsConscious)
            {
                Orders.Add(orderId);
                return Result.Success(this);
            } else return Result.Failure<User>("User have expired orders");
        } else return Result.Failure<User>("User already have 5 active orders");
    }

    public Result<User> RemoveOrder(Guid orderId)
    {
        if (Orders.Contains(orderId))
        {
            Orders.Remove(orderId);
            return Result.Success(this);
        }
        else return Result.Failure<User>($"There is no order with Id: {orderId}");
    }
}
