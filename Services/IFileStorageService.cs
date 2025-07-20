using VisionOfChosen_BE.DTOs.FileStorage;

namespace VisionOfChosen_BE.Services
{
    public interface IFileStorageService
    {
        Task<FileUploadResult> SaveFileAsync(IFormFile file);
        Task<List<FileUploadResult>> SaveFilesAsync(List<IFormFile> files);
    }

    public class FileStorageService : IFileStorageService
    {
        private readonly string _uploadFolder;
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public FileStorageService()
        {
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(_uploadFolder))
                Directory.CreateDirectory(_uploadFolder);
        }

        public async Task<FileUploadResult> SaveFileAsync(IFormFile file)
        {
            var result = new FileUploadResult
            {
                OriginalFileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length
            };

            if (file == null || file.Length == 0)
            {
                result.Error = "File is empty.";
                return result;
            }

            if (file.Length > MaxFileSize)
            {
                result.Error = "File exceeds max allowed size (5MB).";
                return result;
            }

            var ext = Path.GetExtension(file.FileName);
            var savedFileName = $"{Guid.NewGuid()}{ext}";
            var savePath = Path.Combine(_uploadFolder, savedFileName);

            try
            {
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                result.SavedFileName = savedFileName;
                result.Url = $"/Uploads/{savedFileName}";
            }
            catch (Exception ex)
            {
                result.Error = $"Failed to save file: {ex.Message}";
            }

            return result;
        }

        public async Task<List<FileUploadResult>> SaveFilesAsync(List<IFormFile> files)
        {
            var results = new List<FileUploadResult>();

            foreach (var file in files)
            {
                var result = await SaveFileAsync(file);
                if (result.Error == null)
                {
                    results.Add(result);
                }
            }

            return results;
        }
    }
}
