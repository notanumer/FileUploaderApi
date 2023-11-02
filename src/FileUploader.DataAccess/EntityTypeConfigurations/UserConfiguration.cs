using FileUploader.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileUploader.DataAccess.EntityTypeConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(u => u.FileGroups)
            .WithOne(fg => fg.CreatedByUser)
            .HasForeignKey(fg => fg.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
