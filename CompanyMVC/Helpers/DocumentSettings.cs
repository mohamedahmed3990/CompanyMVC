namespace CompanyMVC.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. get folder path

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            // 2.  get file name and make it unique

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. get file path

            string filePath = Path.Combine(folderPath, fileName);

            // 4. save file as streams

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if(File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
