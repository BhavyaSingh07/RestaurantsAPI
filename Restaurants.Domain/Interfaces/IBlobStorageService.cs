namespace Restaurants.Domain.Interfaces
{
    public interface IBlobStorageService
    {
        string? GetBlobSas(string? blobUrl);
        Task<string> UploadToBlobAsync(Stream data, string fileName);
    }
}
