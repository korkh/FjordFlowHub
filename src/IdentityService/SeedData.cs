using System.Security.Claims;
using Duende.IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        //Manager service
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        //Check if any users in database
        if (userMgr.Users.Any())
            return;

        var alice = userMgr.FindByNameAsync("alice").Result;
        if (alice == null)
        {
            alice = new ApplicationUser
            {
                UserName = "alice",
                Email = "AliceSmith@example.com",
                EmailConfirmed = true,
            };

            //Creating user hardcoded
            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr
                .AddClaimsAsync(
                    alice,
                    new Claim[]
                    {
                        //Add more claims if need
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    }
                )
                .Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("alice created");
        }
        else
        {
            Log.Debug("alice already exists");
        }

        var bob = userMgr.FindByNameAsync("bob").Result;
        if (bob == null)
        {
            bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@example.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr
                .AddClaimsAsync(
                    bob,
                    new Claim[]
                    { //We can add other claims
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    }
                )
                .Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("bob created");
        }
        else
        {
            Log.Debug("bob already exists");
        }

        var petro = userMgr.FindByNameAsync("petro").Result;
        if (petro == null)
        {
            petro = new ApplicationUser
            {
                UserName = "petro",
                Email = "petro@test.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(petro, "Pa$$w0rd").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr
                .AddClaimsAsync(
                    petro,
                    new Claim[]
                    { //We can add other claims
                        new Claim(JwtClaimTypes.Name, "petro Smith"),
                    }
                )
                .Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("petro created");
        }
        else
        {
            Log.Debug("petro already exists");
        }
    }
}
