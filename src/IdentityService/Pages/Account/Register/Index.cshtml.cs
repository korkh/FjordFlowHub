using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer.Services;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Account.Register;

[SecurityHeaders]
[AllowAnonymous]
public class Index(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IIdentityServerInteractionService interaction
) : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IIdentityServerInteractionService _interaction = interaction;

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    // [BindProperty]
    // public bool RegisterSuccess { get; set; }

    public IActionResult OnGet(string returnUrl)
    {
        Input = new InputModel { ReturnUrl = returnUrl };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // Check if user cancelled
        if (Input.Button != "register")
        {
            return Redirect(Input.ReturnUrl ?? "~/");
        }

        // Validate model
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = Input.Username,
                Email = Input.Email,
                EmailConfirmed = true, // Set to false if you implement email verification
            };

            var result = await _userManager.CreateAsync(user, Input.Password!);

            if (result.Succeeded)
            {
                // Add default claims if needed
                await _userManager.AddClaimsAsync(
                    user,
                    [
                        //new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.Name, Input.FullName),
                    ]
                );

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (
                    _interaction.IsValidReturnUrl(Input.ReturnUrl)
                    || Url.IsLocalUrl(Input.ReturnUrl)
                )
                {
                    return Redirect(Input.ReturnUrl ?? "~/");
                }

                return Redirect("~/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}
