using FileUploader.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileUploader.DataAccess.EntityTypeConfigurations;

internal class FileGroupConfiguration : IEntityTypeConfiguration<FileGroup>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<FileGroup> builder)
    {
        builder.HasMany(fg => fg.Files)
            .WithOne(f => f.FileGroup)
            .HasForeignKey(f => f.FileGroupId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(fg => fg.Token).IsUnique();
    }
}
