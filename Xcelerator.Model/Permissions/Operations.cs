namespace Xcelerator.Model.Permissions
{
    public class Operations
    {
        public const string ReadUserOperationName = "user.read";
        public const string CreateUserOperationName = "user.create";
        public const string UpdateUserOperationName = "user.update";

        public const string ReadRoleOperationName = "role.read";
        public const string CreateRoleOperationName = "role.create";
        public const string UpdateRoleOperationName = "role.update";

        public const string ReadTemplateOperationName = "template.read";
        public const string CreateTemplateOperationName = "template.create";
        public const string UpdateTemplateOperationName = "template.update";
        public const string DeleteTemplateOperationName = "template.delete";
        public const string ReadSelfTemplateOperationName = "template.readSelf";
        public const string CreateSelfTemplateOperationName = "template.createSelf";
        public const string UpdateSelfTemplateOperationName = "template.updateSelf";
        public const string DeleteSelfTemplateOperationName = "template.deleteSelf";

        public const string ReadAuditQuestionOperationName = "auditQuestion.read";
        public const string CreateAuditQuestionOperationName = "auditQuestion.create";
        public const string UpdateAuditQuestionOperationName = "auditQuestion.update";
        public const string DeleteAuditQuestionOperationName = "auditQuestion.delete";
        public const string AssignAuditQuestionOperationName = "auditQuestion.assign";
        public const string ReadSelfAuditQuestionOperationName = "auditQuestion.readSelf";
        public const string CreateSelfAuditQuestionOperationName = "auditQuestion.createSelf";
        public const string UpdateSelfAuditQuestionOperationName = "auditQuestion.updateSelf";
        public const string DeleteSelfAuditQuestionOperationName = "auditQuestion.deleteSelf";

        public const string ReadAuditAnswerOperationName = "auditAnswer.read";
        public const string CreateAuditAnswerOperationName = "auditAnswer.create";
        public const string UpdateAuditAnswerOperationName = "auditAnswer.update";
        public const string DeleteAuditAnswerOperationName = "auditAnswer.delete";
        public const string ReadSelfAuditAnswerOperationName = "auditAnswer.readSelf";
        public const string CreateSelfAuditAnswerOperationName = "auditAnswer.createSelf";
        public const string UpdateSelfAuditAnswerOperationName = "auditAnswer.updateSelf";
        public const string DeleteSelfAuditAnswerOperationName = "auditAnswer.deleteSelf";

        public const string ReadReportOperationName = "report.read";
        public const string ReadSelfReportOperationName = "report.readSelf";
    }
}
