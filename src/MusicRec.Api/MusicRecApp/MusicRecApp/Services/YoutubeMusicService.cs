using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;
using MusicRecApp.Model;

namespace MusicRecApp.Services
{
    public class YouTubeMusicService
    {
        private readonly YoutubeClient _youtube;

        public YouTubeMusicService()
        {
            _youtube = new YoutubeClient();
        }

        public async Task<List<SongDto>> SearchRelatedVideosAsync(string query)
        {
            string artistName = query.Split('-')[0].Trim();

            string artistQuery = $"{artistName} official music video -react -reaction -reviewed -vlog -interview -live";

            var searchBatch = await _youtube.Search.GetVideosAsync(artistQuery).CollectAsync(10);

            var filteredResults = searchBatch
                .Where(v =>
                    !v.Title.ToLower().Contains("react") &&
                    !v.Title.ToLower().Contains("reaction") &&
                    !v.Title.ToLower().Contains("reviewed") &&
                    v.Duration.Value.TotalMinutes < 7 &&
                    v.Duration.Value.TotalMinutes > 1.5
                )
                .Take(5)
                .Select(v => new SongDto
                {
                    YouTubeId = v.Id.Value,
                    Title = v.Title,
                    Artist = v.Author.ChannelTitle,
                    Thumbnail = v.Thumbnails.OrderByDescending(t => t.Resolution.Height).FirstOrDefault()?.Url
                })
                .ToList();

            return filteredResults;
        }

        public async Task<SongDto?> SearchSingleVideoAsync(string query)
        {
            try
            {
                var searchResults = await _youtube.Search.GetVideosAsync(query).CollectAsync(1);
                var v = searchResults.FirstOrDefault();

                if (v == null) return null;

                return new SongDto
                {
                    YouTubeId = v.Id.Value,
                    Title = v.Title,
                    Artist = v.Author.ChannelTitle,
                    Thumbnail = v.Thumbnails.OrderByDescending(t => t.Resolution.Height).FirstOrDefault()?.Url
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}