namespace MusicRecApp.Model
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public double Tempo { get; set; } 
        public double Energy { get; set; } 
    }
}
