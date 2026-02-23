using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;

namespace MusicRecApp.Services
{
    public class YouTubeService
    {
        private readonly YoutubeClient _youtube;

        public YouTubeService()
        {
            _youtube = new YoutubeClient();
        }

        public async Task<dynamic> SearchTrack(string query)
        {
            try
            {
                var results = await _youtube.Search.GetVideosAsync(query);
                var video = results.FirstOrDefault();

                if (video == null) return null;

                return new
                {
                    YouTubeId = video.Id.Value,
                    Title = video.Title,
                    Artist = video.Author.ChannelTitle,
                    Thumbnail = video.Thumbnails.FirstOrDefault()?.Url
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare YouTube: {ex.Message}");
                return null;
            }
        }
    }
}