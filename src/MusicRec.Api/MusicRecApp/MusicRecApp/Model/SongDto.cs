namespace MusicRecApp.Model
{
    public class SongDto
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string? YouTubeId { get; set; } 
        public string? Thumbnail { get; set; }
    }
}