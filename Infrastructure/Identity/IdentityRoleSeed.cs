using Microsoft.AspNetCore.Identity;

public static class IdentityRoleSeed
{
  public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
  {
    string[] roles = { "Admin", "Customer" };

    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole(role));
      }
    }
  }
}
