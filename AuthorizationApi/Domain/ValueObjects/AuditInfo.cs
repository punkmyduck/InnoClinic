namespace AuthorizationApi.Domain.ValueObjects
{
    public sealed record AuditInfo(DateTime CreatedAt, Guid CreatedBy, DateTime? UpdatedAt, Guid? UpdatedBy)
    {
        public AuditInfo WithUpdate(DateTime UpdatedAt, Guid UpdatedBy)
        {
            return new AuditInfo(CreatedAt, CreatedBy, UpdatedAt, UpdatedBy);
        }
    }
}
