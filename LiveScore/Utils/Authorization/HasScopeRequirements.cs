using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScore.Utils.Authorization
{

    /// <summary>
    /// Authorization policy scope requirement handler.
    /// </summary>
    public class HasScopeRequirement : AuthorizationHandler<HasScopeRequirement>, IAuthorizationRequirement
    {
        private readonly string issuer;
        private readonly string scope;

        /// <summary>
        /// Constructor that receives scope and issuer parameters.
        /// </summary>
        /// <param name="scope">Authorization policy scope</param>
        /// <param name="issuer">Authorization policy issuer</param>
        public HasScopeRequirement(string scope, string issuer)
        {
            this.scope = scope;
            this.issuer = issuer;
        }

        /// <summary>
        /// This method checks user claims for action's required permitions.
        /// </summary>
        /// <param name="context">Authorization context</param>
        /// <param name="requirement">Scope requirement</param>
        /// <returns>Successfully completed task</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == issuer))
            {
                return Task.CompletedTask;
            }

            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == issuer).Value.Split(' ');

            if (scopes.Any(s => s == scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
