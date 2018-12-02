namespace Xcelerator.Common.Permissions
{
    public class Operations
    {
        public static readonly string ReadUserOperationName = "user.read";
        public static readonly string CreateUserOperationName = "user.create";
        public static readonly string UpdateUserOperationName = "user.update";

        public static readonly string ReadRoleOperationName = "role.read";
        public static readonly string CreateRoleOperationName = "role.create";
        public static readonly string UpdateRoleOperationName = "role.update";

        public static readonly string ReadTemplateOperationName = "template.read";
        public static readonly string CreateTemplateOperationName = "template.create";
        public static readonly string UpdateTemplateOperationName = "template.update";
        public static readonly string DeleteTemplateOperationName = "template.delete";
        public static readonly string ReadSelfTemplateOperationName = "template.readSelf";
        public static readonly string CreateSelfTemplateOperationName = "template.createSelf";
        public static readonly string UpdateSelfTemplateOperationName = "template.updateSelf";
        public static readonly string DeleteSelfTemplateOperationName = "template.deleteSelf";

        public static readonly string ReadAuditQuestionOperationName = "auditQuestion.read";
        public static readonly string CreateAuditQuestionOperationName = "auditQuestion.create";
        public static readonly string UpdateAuditQuestionOperationName = "auditQuestion.update";
        public static readonly string DeleteAuditQuestionOperationName = "auditQuestion.delete";
        public static readonly string AssignAuditQuestionOperationName = "auditQuestion.assign";
        public static readonly string ReadSelfAuditQuestionOperationName = "auditQuestion.readSelf";
        public static readonly string CreateSelfAuditQuestionOperationName = "auditQuestion.createSelf";
        public static readonly string UpdateSelfAuditQuestionOperationName = "auditQuestion.updateSelf";
        public static readonly string DeleteSelfAuditQuestionOperationName = "auditQuestion.deleteSelf";

        public static readonly string ReadAuditAnswerOperationName = "auditAnswer.read";
        public static readonly string CreateAuditAnswerOperationName = "auditAnswer.create";
        public static readonly string UpdateAuditAnswerOperationName = "auditAnswer.update";
        public static readonly string DeleteAuditAnswerOperationName = "auditAnswer.delete";
        public static readonly string ReadSelfAuditAnswerOperationName = "auditAnswer.readSelf";
        public static readonly string CreateSelfAuditAnswerOperationName = "auditAnswer.createSelf";
        public static readonly string UpdateSelfAuditAnswerOperationName = "auditAnswer.updateSelf";
        public static readonly string DeleteSelfAuditAnswerOperationName = "auditAnswer.deleteSelf";

        public static readonly string ReadReportOperationName = "report.read";
        public static readonly string ReadSelfReportOperationName = "report.readSelf";
    }
}
