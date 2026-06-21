using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeAnalyzer.Api.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(
            string fileName,
            Stream fileStream);

        Task<Stream> DownloadAsync(
            string blobName);
    }
}
