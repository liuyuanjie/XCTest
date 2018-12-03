using Microsoft.AspNetCore.Authorization;
using Xcelerator.Common;
using Xcelerator.Model.Permissions;

namespace Xcelerator.Api.Configurations.Authorization
{
    public class Policies
    {
        public const string RequiredAuditEditPolicy = "RequiredAuditEdit";

        public static void HasRequiredAuditEdit(AuthorizationPolicyBuilder builder)
        {
            builder.RequireClaim(CustomClaimTypes.Permission, Operations.UpdateAuditQuestionOperationName);
        }
    }
}