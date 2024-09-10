using FluentValidation;
using FluentValidation.AspNetCore;
using KbAis.Intern.Library.Service.Web;
using KbAis.Intern.Library.Service.Web.Data.Models;
using KbAis.Intern.Library.Service.Web.Data.Validators;
using KbAis.Intern.Library.Service.Web.Features.Authors;
using KbAis.Intern.Library.Service.Web.Features.Authors.CreateAuthor;
using KbAis.Intern.Library.Service.Web.Features.Authors.CreateAuthor.V1;
using KbAis.Intern.Library.Service.Web.Features.Authors.DeletingAuthor;
using KbAis.Intern.Library.Service.Web.Features.Authors.GettingAllAuthors;
using KbAis.Intern.Library.Service.Web.Features.Authors.GettingAuthorById;
using KbAis.Intern.Library.Service.Web.Features.Authors.UpdateAuthor;
using KbAis.Intern.Library.Service.Web.Features.Authors.UpdateAuthor.V1;
using KbAis.Intern.Library.Service.Web.Features.Booking;
using KbAis.Intern.Library.Service.Web.Features.Booking.GettingAllOrders;
using KbAis.Intern.Library.Service.Web.Features.Booking.InitiatingNewOrder;
using KbAis.Intern.Library.Service.Web.Features.Booking.InitiatingNewOrder.V1;
using KbAis.Intern.Library.Service.Web.Features.Booking.IssuingOrder;
using KbAis.Intern.Library.Service.Web.Features.Booking.ReturningOrder;
using KbAis.Intern.Library.Service.Web.Features.Books;
using KbAis.Intern.Library.Service.Web.Features.Books.CreatingBook;
using KbAis.Intern.Library.Service.Web.Features.Books.CreatingBook.V1;
using KbAis.Intern.Library.Service.Web.Features.Books.DeletingBook;
using KbAis.Intern.Library.Service.Web.Features.Books.GettingAllBooks;
using KbAis.Intern.Library.Service.Web.Features.Books.GettingBookById;
using KbAis.Intern.Library.Service.Web.Features.Books.UpdatingBookInfo;
using KbAis.Intern.Library.Service.Web.Features.Books.UpdatingBookInfo.V1;
using KbAis.Intern.Library.Service.Web.Features.Covers;
using KbAis.Intern.Library.Service.Web.Features.Covers.CreatingCover;
using KbAis.Intern.Library.Service.Web.Features.Covers.CreatingCover.V1;
using KbAis.Intern.Library.Service.Web.Features.Covers.DeletingCover;
using KbAis.Intern.Library.Service.Web.Features.Covers.GettingAllCovers;
using KbAis.Intern.Library.Service.Web.Features.Covers.GettingCoverById;
using KbAis.Intern.Library.Service.Web.Features.Covers.UpdatingCoverInfo;
using KbAis.Intern.Library.Service.Web.Features.Covers.UpdatingCoverInfo.V1;
using KbAis.Intern.Library.Service.Web.Features.Users;
using KbAis.Intern.Library.Service.Web.Features.Users.DeletingUser;
using KbAis.Intern.Library.Service.Web.Features.Users.GetingUserById;
using KbAis.Intern.Library.Service.Web.Features.Users.GettingAllUsers;
using KbAis.Intern.Library.Service.Web.Features.Users.LoginingUser;
using KbAis.Intern.Library.Service.Web.Features.Users.RegisteringUser;
using KbAis.Intern.Library.Service.Web.Features.Users.RegisteringUser.V1;
using KbAis.Intern.Library.Service.Web.Features.Users.UpdatingUserInfo;
using KbAis.Intern.Library.Service.Web.Features.Users.UpdatingUserInfo.V1;
using Marten;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var MyAllowSpecificOrigins = "LibraryAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

builder.Services.AddMarten(o =>
{
    o.UseDefaultSerialization(nonPublicMembersStorage: NonPublicMembersStorage.NonPublicSetters);
    o.Connection(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "me",
        ValidAudience = "you",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my security string"))
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
});

builder.Services.AddControllers();
//Authors
builder.Services.AddScoped<GetAllAuthorsCommandHandler>();
builder.Services.AddScoped<GetAuthorByIdCommandHandler>();
builder.Services.AddScoped<CreateAuthorCommandHandler>();
builder.Services.AddScoped<DeleteAuthorCommandHandler>();
builder.Services.AddScoped<UpdateAuthorCommandHandler>();
//Booking
builder.Services.AddScoped<GetAllOrdersCommandHandler>();
builder.Services.AddScoped<InitiateNewOrderCommandHandler>();
builder.Services.AddScoped<IssueOrderCommandHandler>();
builder.Services.AddScoped<ReturnOrderCommandHandler>();
//Books
builder.Services.AddScoped<CreateBookCommandHandler>();
builder.Services.AddScoped<DeleteBookCommandHandler>();
builder.Services.AddScoped<GetAllBooksCommandHandler>();
builder.Services.AddScoped<GetBookByIdCommandHandler>();
builder.Services.AddScoped<UpdateBookCommandHandler>();
//Covers
builder.Services.AddScoped<CreateCoverCommandHandler>();
builder.Services.AddScoped<DeleteCoverCommandHandler>();
builder.Services.AddScoped<GetAllCoversCommandHandler>();
builder.Services.AddScoped<GetCoverByIdCommandHandler>();
builder.Services.AddScoped<UpdateCoverCommandHandler>();
//Users
builder.Services.AddScoped<DeleteUserCommandHandler>();
builder.Services.AddScoped<GetAllUsersCommandHandler>();
builder.Services.AddScoped<GetUserByIdCommandHandler>();
builder.Services.AddScoped<LoginUserCommandHandler>();
builder.Services.AddScoped<RegisterUserCommandHandler>();
builder.Services.AddScoped<UpdateUserCommandHandler>();
//Validators
builder.Services.AddScoped<CreateAuthorRequestValidator>();
builder.Services.AddScoped<UpdateAuthorRequestValidator>();
builder.Services.AddScoped<InitiateNewOrderRequestValidator>();
builder.Services.AddScoped<CreateBookRequestValidator>();
builder.Services.AddScoped<UpdateBookRequestValidator>();
builder.Services.AddScoped<CreateCoverRequestValidator>();
builder.Services.AddScoped<UpdateCoverRequestValidator>();
builder.Services.AddScoped<RegisterUserRequestValidator>();
builder.Services.AddScoped<UpdateUserRequestValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<AuthorValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AuthorEndpoints.V1.Map(app);
BookEndpoints.V1.Map(app);
CoverEndpoints.V1.Map(app);
UserEndpoints.V1.Map(app);
OrderEndpoints.V1.Map(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.Use(async (context, next) =>
{
    //context.Response.Headers.Add("X-Developed-By", "Victor");
    context.Response.Headers.Server = "Really cool server";
    await next.Invoke();
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
