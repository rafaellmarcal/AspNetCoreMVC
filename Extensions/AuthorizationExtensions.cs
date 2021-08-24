using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspNetCoreMVC.Extensions
{
    public class RequiredPermission : IAuthorizationRequirement
    {
        public string Permission { get; }

        public RequiredPermission(string permission)
        {
            Permission = permission;
        }
    }

    public class RequiredPermissionHandler : AuthorizationHandler<RequiredPermission>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequiredPermission requirement)
        {
            if(context.User.HasClaim(c => c.Type == "Permission" && c.Value.Contains(requirement.Permission)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
