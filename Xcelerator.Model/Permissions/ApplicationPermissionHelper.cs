using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Xcelerator.Model.Permissions
{
    public static class ApplicationPermissionHelper
    {
        public static ReadOnlyCollection<ApplicationPermission> AllPermissions;

        public const string UserPermissionGroupName = "User Permission";
        public static ApplicationPermission ViewUser = new ApplicationPermission("View User", Operations.ReadUserOperationName, UserPermissionGroupName, "Permission to view other users account details");
        public static ApplicationPermission CreateUser = new ApplicationPermission("Create User", Operations.CreateUserOperationName, UserPermissionGroupName, "Permission to create other users account details");
        public static ApplicationPermission UpdateUser = new ApplicationPermission("Update User", Operations.UpdateUserOperationName, UserPermissionGroupName, "Permission to modify other users account details");

        public const string RolePermissionGroupName = "Role Permission";
        public static ApplicationPermission ViewRole = new ApplicationPermission("View Role", Operations.ReadRoleOperationName, RolePermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission UpdateRole = new ApplicationPermission("Update Role", Operations.CreateRoleOperationName, RolePermissionGroupName, "Permission to modify roles");
        public static ApplicationPermission AssignRole = new ApplicationPermission("Assign Role", Operations.UpdateRoleOperationName, RolePermissionGroupName, "Permission to assign roles to users");

        public const string TemplatePermissionGroupName = "Template Permission";
        public static ApplicationPermission ViewTemplateRole = new ApplicationPermission("View Template", Operations.ReadTemplateOperationName, TemplatePermissionGroupName);
        public static ApplicationPermission CreateTemplateRole = new ApplicationPermission("Create Template", Operations.CreateTemplateOperationName, TemplatePermissionGroupName);
        public static ApplicationPermission UpdateTemplateRole = new ApplicationPermission("Update Template", Operations.UpdateTemplateOperationName, TemplatePermissionGroupName);
        public static ApplicationPermission DeleteTemplateRole = new ApplicationPermission("Delete Template", Operations.DeleteTemplateOperationName, TemplatePermissionGroupName);

        public const string AuditQuestionPermissionGroupName = "Audit Question Permission";
        public static ApplicationPermission ViewAuditQuestionRole = new ApplicationPermission("View Audit Question", Operations.ReadAuditQuestionOperationName, AuditQuestionPermissionGroupName);
        public static ApplicationPermission CreateAuditQuestionRole = new ApplicationPermission("Create Audit Question", Operations.CreateAuditQuestionOperationName, AuditQuestionPermissionGroupName);
        public static ApplicationPermission UpdteAuditQuestionRole = new ApplicationPermission("Update Audit Question", Operations.CreateAuditQuestionOperationName, AuditQuestionPermissionGroupName);
        public static ApplicationPermission DeleteAuditQuestionRole = new ApplicationPermission("Delete Audit Question", Operations.UpdateAuditQuestionOperationName, AuditQuestionPermissionGroupName);
        public static ApplicationPermission AssignAuditQuestionRole = new ApplicationPermission("Assign Audit Question", Operations.AssignAuditQuestionOperationName, AuditQuestionPermissionGroupName);

        public const string AuditAnswerPermissionGroupName = "Audit Answer Permission";
        public static ApplicationPermission ViewAuditAnswerRole = new ApplicationPermission("View Audit Answer", Operations.ReadAuditAnswerOperationName, AuditAnswerPermissionGroupName);
        public static ApplicationPermission CreateAuditAnswerRole = new ApplicationPermission("Create Audit Answer", Operations.CreateAuditAnswerOperationName, AuditAnswerPermissionGroupName);
        public static ApplicationPermission UpdateAuditAnswerRole = new ApplicationPermission("Update Audit Answer", Operations.UpdateAuditAnswerOperationName, AuditAnswerPermissionGroupName);
        public static ApplicationPermission DeleteAuditAnswerRole = new ApplicationPermission("Delete Audit Answer", Operations.DeleteAuditQuestionOperationName, AuditAnswerPermissionGroupName);

        public const string ReportPermissionGroupName = "Report Permission";
        public static ApplicationPermission ViewReportRole = new ApplicationPermission("View Report", Operations.ReadReportOperationName, ReportPermissionGroupName);
        public static ApplicationPermission ViewSelfReportRole = new ApplicationPermission("View Self Report", Operations.ReadSelfReportOperationName, ReportPermissionGroupName);

        static ApplicationPermissionHelper()
        {
            List<ApplicationPermission> allPermissions = new List<ApplicationPermission>
            {
                ViewUser,
                CreateUser,
                UpdateUser,
                ViewRole,
                UpdateRole,
                AssignRole,

                ViewTemplateRole,
                CreateTemplateRole,
                UpdateTemplateRole,
                DeleteTemplateRole,

                ViewAuditQuestionRole,
                CreateAuditQuestionRole,
                UpdteAuditQuestionRole,
                DeleteAuditQuestionRole,
                AssignAuditQuestionRole,

                ViewAuditAnswerRole,
                CreateAuditAnswerRole,
                UpdateAuditAnswerRole,
                DeleteAuditAnswerRole,

                ViewReportRole,
                ViewSelfReportRole
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static ApplicationPermission GetPermissionByName(string permissionName)
        {
            return AllPermissions.FirstOrDefault(p => p.Name == permissionName);
        }

        public static ApplicationPermission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.FirstOrDefault(p => p.Value == permissionValue);
        }

        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }

        public static string[] GetAllSTPSystemAdminValues()
        {
            return new string[] { ViewTemplateRole, CreateTemplateRole, UpdateTemplateRole, DeleteTemplateRole, CreateAuditQuestionRole, UpdteAuditQuestionRole, DeleteAuditQuestionRole, AssignAuditQuestionRole };
        }

        public static string[] GetClientPermissionValues()
        {
            return new string[] { ViewTemplateRole, CreateTemplateRole, UpdateTemplateRole, DeleteTemplateRole, CreateAuditQuestionRole, UpdteAuditQuestionRole, DeleteAuditQuestionRole, AssignAuditQuestionRole };
        }

        public static string[] GetChiefAutditorPermissionValues()
        {
            return new string[] { ViewTemplateRole, CreateTemplateRole, UpdateTemplateRole, DeleteTemplateRole, CreateAuditQuestionRole, UpdteAuditQuestionRole, DeleteAuditQuestionRole, AssignAuditQuestionRole };
        }

        public static string[] GetAuditFacilitatorPermissionValues()
        {
            return new string[] { ViewTemplateRole, CreateTemplateRole, UpdateTemplateRole, DeleteTemplateRole, CreateAuditQuestionRole, UpdteAuditQuestionRole, DeleteAuditQuestionRole, AssignAuditQuestionRole };
        }
    }
}