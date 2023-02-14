using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

public class YouTubeDownloader // Note: actual namespace depends on the project name.
{
 
        static async Task Main(string[] args)
    {
        Console.WriteLine("-----------------------");
        Console.WriteLine("By Ammar R. Jabbar");
        Console.WriteLine("-----------------------");

        Console.WriteLine("Enter the YouTube video URL: ");
      
        string youtubeUrl = Console.ReadLine();
        Console.WriteLine("Write the format video");
        string format = Console.ReadLine();
        Console.WriteLine("Waiting ................");
        var fileName = await DownloadAndConvertVideoAsync(youtubeUrl, format);
        Console.WriteLine("Download Complete!");
        Console.WriteLine("Filename : " + fileName);

        // open file location
        Process.Start("explorer.exe", "/select," + fileName);

        // prvent console from closing
        Console.ReadLine();
        



    }


    static async Task<string> DownloadAndConvertVideoAsync(string videosUrl, string outputFormat)
    {
        // The YouTube video URL to download


        // The desired file extension of the downloaded video
        var desiredExtension = "." + outputFormat;

        // Create a new YoutubeClient instance
        var youtube = new YoutubeClient();

        // Get the metadata for the video
        var video = await youtube.Videos.GetAsync(videosUrl);

        // Get the best available video and audio streams for the video
        var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(video.Id);

        // Get the MuxedStreamInfo to combine the video and audio streams
        var muxedStreamInfo = streamInfoSet.GetMuxedStreams().GetWithHighestVideoQuality();


    
        

        // Download the combined stream to a file on disk
        var fileName = Path.ChangeExtension(video.Title, desiredExtension);
        await youtube.Videos.Streams.DownloadAsync(muxedStreamInfo, fileName);

        return fileName;

    }

}