using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core2Identity.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper: TagHelper
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleUsersTagHelper(UserManager<ApplicationUser> userMan,RoleManager<IdentityRole> roleMan)
        {
            userManager = userMan;
            roleManager = roleMan;
        }
        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();

            var role = await roleManager.FindByIdAsync(Role);

            if(role != null)
            {
                foreach (var user in userManager.Users)
                {
                    if(user!= null && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0?"Kullanıcı Yok" : string.Join(", ", names));
        }
    }
}
