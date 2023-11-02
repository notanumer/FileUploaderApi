using MediatR;

namespace FileUploader.UseCases.FileGroups.FileGroupGetFileProgress;

public record FileGroupGetFileProgressQuery(string FileName) : IRequest<double>;
