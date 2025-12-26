using AuthorizationApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthorizationApi.Infrastructure.EntityConfigurations
{
    public class EmailVerificationTokenEntityConfiguration : IEntityTypeConfiguration<EmailVerificationTokenEntity>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationTokenEntity> builder)
        {
            builder.ToTable("email_verification_tokens");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.TokenHash)
                .IsUnique();

            builder.HasIndex(x => x.AccountId);

            builder.Property(x => x.ExpiresAt)
                .IsRequired();
        }
    }

}
