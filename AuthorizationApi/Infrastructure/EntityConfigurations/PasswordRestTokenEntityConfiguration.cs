using AuthorizationApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthorizationApi.Infrastructure.EntityConfigurations
{
    public sealed class PasswordResetTokenEntityConfiguration : IEntityTypeConfiguration<PasswordResetTokenEntity>
    {
        public void Configure(EntityTypeBuilder<PasswordResetTokenEntity> builder)
        {
            builder.ToTable("password_reset_tokens");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.TokenHash)
                .IsUnique();

            builder.HasIndex(x => x.AccountId);

            builder.Property(x => x.ExpiresAt)
                .IsRequired();
        }
    }
}
