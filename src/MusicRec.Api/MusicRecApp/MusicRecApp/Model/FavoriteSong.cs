namespace MusicRecApp.Model
{
    public class FavoriteSong
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string YoutubeId { get; set; } 
        public string Title { get; set; }
        public string Artist { get; set; }
    }
}
