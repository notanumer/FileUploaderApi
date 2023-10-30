namespace FileUploader.Domain;

public class File
{
    public int Id { get; private set; }

    required public string Name { get; set; }

    required public string Type { get; set; }

    required public byte[] Data { get; set; }

    public int FileGroupId { get; set; }

    public FileGroup? FileGroup { get; set; }
}
