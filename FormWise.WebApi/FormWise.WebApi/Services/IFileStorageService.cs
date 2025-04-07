namespace FormWise.WebApi.Services
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Saves the file to storage and returns the relative or absolute path.
        /// </summary>
        /// <param name="file">Uploaded file</param>
        /// <param name="targetFolder">e.g. "receipts"</param>
        /// <returns>Saved file path</returns>
        Task<string> SaveFileAsync(IFormFile file, string targetFolder);
    }

}
