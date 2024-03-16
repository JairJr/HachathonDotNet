using FFMpegCore;
using Service;
using System.Drawing;
using System.IO.Compression;

namespace ServiceImpl;

public class WorkerVideoService : IWorkerVideoService
{
    public bool ExtraiImagens(FileStream video, string name)
    {
        try
        {
            var videoPath = Path.Combine(Directory.GetCurrentDirectory(),@"\Videos\");
            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), @"\Images\");
            var outputFolder = Path.Combine(baseFolder,name);
            Directory.CreateDirectory(videoPath);
            Directory.CreateDirectory(outputFolder);
            var videoInfo = FFProbe.Analyse(video);
            var duration = videoInfo.Duration;
            //Saves video to local path
            var localVideo = Path.Combine(videoPath, video.Name);
            var salvou =  SalvaArquivoLocal(video, localVideo);
            var interval = TimeSpan.FromSeconds(20);
            for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
            {
                Console.WriteLine($"Processando frame: {currentTime}");

                var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                FFMpeg.Snapshot(videoPath, outputPath, new Size(1920, 1080), currentTime);
            }

            string destinationZipFilePath = @$"{outputFolder}\{name}.zip";

            ZipFile.CreateFromDirectory(outputFolder, destinationZipFilePath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool SalvaArquivoLocal(FileStream video, string videoPath)
    {
        try
        {
            using (FileStream saveFileStream = new FileStream(videoPath, FileMode.Create, FileAccess.Write))
            {
                // Crie um buffer para ler os bytes do arquivo original
                byte[] buffer = new byte[4096];
                int bytesRead;

                // Leia os bytes do arquivo original e escreva-os no novo arquivo
                while ((bytesRead = video.Read(buffer, 0, buffer.Length)) > 0)
                {
                    saveFileStream.Write(buffer, 0, bytesRead);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
