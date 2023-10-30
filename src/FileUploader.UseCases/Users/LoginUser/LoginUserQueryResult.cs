namespace FileUploader.UseCases.Users.LoginUser;

public record LoginUserQueryResult
{
    required public string UserEmail { get; init; }

    required public string Token { get; init; }
}
