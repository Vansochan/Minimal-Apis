using System.Reflection;
using System.Text;
using Engine.Utils.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Minimal.Api.Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoint(typeof(Program));
builder.Services.AddRequestApiHandlers(typeof(Program));
var app = builder.Build();
app.UseEndpoint();
app.MapGet("/heath", () => "OK");

app.Run();
