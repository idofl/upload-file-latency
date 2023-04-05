using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Cloud.Storage.V1;
using System.IO;

namespace dotnet_hello_world.Pages
{
    public class IndexModel : PageModel
    {
        public string Message {get; private set; } = "It's running!";

        public void OnGet()
        {
            Stopwatch watch = new Stopwatch();
            string bucketName = Environment.GetEnvironmentVariable("DEST_BUCKET");
            var storage = StorageClient.Create();
            using var fileStream = System.IO.File.OpenRead(System.IO.Path.Combine("wwwroot", "img", "1.bin"));
            UploadObjectOptions options = new UploadObjectOptions();

            string chunkSize = Environment.GetEnvironmentVariable("CHUNK_SIZE") ?? UploadObjectOptions.MinimumChunkSize.ToString();
            options.ChunkSize = int.Parse(chunkSize);
            watch.Start();
            storage.UploadObject(bucketName, "1.bin", null, fileStream, options);
            watch.Stop();
            Message += $" Upload took { watch.ElapsedMilliseconds }";
        }
    }
}
