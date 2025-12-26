namespace AuthorizationApi.Domain.ValueObjects
{
    public sealed record AuditInfo(DateTime CreatedAt, string CreatedBy, DateTime? UpdatedAt, string? UpdatedBy)
    {
        public AuditInfo WithUpdate(DateTime UpdatedAt, string UpdatedBy)
        {
            return new AuditInfo(CreatedAt, CreatedBy, UpdatedAt, UpdatedBy);
        }
    }
}
