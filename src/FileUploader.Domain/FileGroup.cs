namespace FileUploader.Domain;

public class FileGroup
{
    public int Id { get; private set; }

    public DateTimeOffset CreatedAt { get; set; }

    public string? Token { get; set; }

    public DateTimeOffset? DownloadedByTokenAt { get; set; }

    public int CreatedByUserId { get; set; }

    public User? CreatedByUser { get; set; }

    public ICollection<File> Files { get; private set; } = new List<File>();
}
