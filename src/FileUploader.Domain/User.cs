using System.ComponentModel.DataAnnotations;

namespace FileUploader.Domain;

public class User
{
    public int Id { get; private set; }

    [MaxLength(100)]
    required public string Name { get; set; }

    [MaxLength(100)]
    required public string LastName { get; set; }

    [EmailAddress]
    required public string Email { get; set; }

    required public byte[] PasswordHash { get; set; }

    required public byte[] PasswordSalt { get; set; }

    public ICollection<FileGroup> FileGroups { get; private set; } = new List<FileGroup>();

    required public DateTimeOffset CreatedAt { get; set; }
}
