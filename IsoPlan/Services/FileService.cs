using IsoPlan.Exceptions;
using IsoPlan.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IsoPlan.Services
{
    public interface IFileService
    {
        string getFullPath(string path);
        void Create(IFormFile file, string path);
        void Delete(string path);
        void DeleteDirectory(string path);
    }
    public class FileService : IFileService
    {
        private readonly AppSettings _appSettings;

        public FileService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string getFullPath(string path)
        {
            return Path.Combine(_appSettings.FilesPath, path);
        }
        public void Create(IFormFile file, string path)
        {
            string fullPath = Path.Combine(_appSettings.FilesPath, path, file.FileName);

            if (File.Exists(fullPath))
            {
                throw new AppException("{0} existe déjà. Veuillez le renommer avant le téléchargement.", file.FileName);
            }

            string folderPath = Path.Combine(_appSettings.FilesPath, path);
            Directory.CreateDirectory(folderPath);

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
        }

        public void Delete(string path)
        {
            string fullPath = Path.Combine(_appSettings.FilesPath, path);
            File.Delete(fullPath);
        }

        public void DeleteDirectory(string path)
        {
            string fullPath = Path.Combine(_appSettings.FilesPath, path);
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true);
            }
        }
    }
}
