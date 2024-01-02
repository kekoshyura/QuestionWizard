using Microsoft.AspNetCore.Identity;

namespace Server.Data;
public static class SeedAdministratorRoleAndUser {
    internal const string AdministratorRoleName = "Administrator";
    internal const string AdministratorUserName = "admin@questionWizzard.com";

    internal async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) {
        await SeedAdministratorRole(roleManager);
        await SeedAdministratorUser(userManager);
    }

    private async static Task SeedAdministratorRole(RoleManager<IdentityRole> roleManager) {

        bool administratorRoleExist = await roleManager.RoleExistsAsync(AdministratorRoleName);
        if (administratorRoleExist == false) {
            var role = new IdentityRole {
                Name = AdministratorRoleName
            };

            await roleManager.CreateAsync(role);
        }
    }

    private async static Task SeedAdministratorUser(UserManager<IdentityUser> userManager) {

        bool administratorUserExist = await userManager.FindByEmailAsync(AdministratorUserName) != null;
        if (administratorUserExist == false) {
            var user = new IdentityUser {
                UserName = AdministratorUserName,
                Email = AdministratorUserName,
            };

            IdentityResult identityResult = await userManager.CreateAsync(user, "Password1!");
            if (identityResult.Succeeded) {
                await userManager.AddToRoleAsync(user, AdministratorRoleName);
            }
        }
    }
}

