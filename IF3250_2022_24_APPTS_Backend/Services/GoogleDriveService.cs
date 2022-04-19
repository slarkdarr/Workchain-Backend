using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using IF3250_2022_24_APPTS_Backend.Data;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.Upload;
using Microsoft.Extensions.Options;
using static Google.Apis.Drive.v3.DriveService;

public interface IGoogleDriveService
{
    string CreateFolder(string parent, string folderName);
    Task<UploadResponse> UploadFile(Stream file, string fileName, string fileMime, string folder);
    void DeleteFile(string fileId);

}
public class GoogleDriveService : IGoogleDriveService
{
    private DriveService _driveService;
    private DataContext _context;
    private readonly AppSettings _appSettings;

    public GoogleDriveService(
        IOptions<AppSettings> appSettings,
        DataContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;
        _driveService = GetService(_appSettings);
    }

    private DriveService GetService(AppSettings appSettings)
        
    {
        var tokenResponse = new TokenResponse()
        {
            AccessToken = appSettings.AccessToken,
            RefreshToken = _appSettings.RefreshToken,
        };

        var applicationName = appSettings.applicationName;
        var username = _appSettings.username;

        var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets()
            {
                ClientId = appSettings.ClientId,
                ClientSecret = appSettings.ClientSecret
            },
            Scopes = new[] { Scope.Drive },
            DataStore = new FileDataStore(applicationName)
        });

        var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = applicationName
        });

        return service;

    }

    public string CreateFolder(string parent, string folderName)
    {
        var driveFolder = new Google.Apis.Drive.v3.Data.File();
        driveFolder.Name = folderName;
        driveFolder.MimeType = "application/vnd.google-apps.folder";
        driveFolder.Parents = new string[] { parent };
        var command = _driveService.Files.Create(driveFolder);
        var file = command.Execute();
        return file.Id;
    }

    public async Task<UploadResponse> UploadFile(Stream file, string fileName, string fileMime, string folder)
    {


        var driveFile = new Google.Apis.Drive.v3.Data.File();
                driveFile.Name = fileName;
                driveFile.MimeType = fileMime;
                driveFile.Parents = new string[] { folder };


        var request = _driveService.Files.Create(driveFile, file, fileMime);
                request.SupportsAllDrives = true;
                request.Fields = "id,webViewLink";

                var response = request.Upload();
                if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                    throw response.Exception;

        Permission newPermission = new Permission();
        newPermission.Type = "anyone";
        newPermission.Role = "reader";
        _driveService.Permissions.Create(newPermission,request.ResponseBody.Id).Execute();

        UploadResponse uploadResponse = new UploadResponse();
        uploadResponse.link = "https://drive.google.com/uc?id=" + request.ResponseBody.Id;

        return uploadResponse;
    }
    public void DeleteFile(string fileId)
    {
        var command = _driveService.Files.Delete(fileId);
        var result = command.Execute();
    }

    public IEnumerable<Google.Apis.Drive.v3.Data.File> GetFiles(string folder)
    {

        var fileList = _driveService.Files.List();
        fileList.Q = $"mimeType!='application/vnd.google-apps.folder' and '{folder}' in parents";
        fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

        var result = new List<Google.Apis.Drive.v3.Data.File>();
        string pageToken = null;
        do
        {
            fileList.PageToken = pageToken;
            var filesResult = fileList.Execute();
            var files = filesResult.Files;
            pageToken = filesResult.NextPageToken;
            result.AddRange(files);
        } while (pageToken != null);


        return result;
    }

}