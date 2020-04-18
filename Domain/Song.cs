using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain
{
    public class Song
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AlbumId { get; set; }

        public Song(int songId = -1, string title = "", string description = "", int albumId= -1)
        {
            Title = title;
            Description = description;
            SongId = songId;
            AlbumId = albumId;
        }
        
        public Song() {}

        public override bool Equals(object? obj)
        {
            if (obj != null)
                if (obj.GetType() == this.GetType())
                {
                    Song song = (Song) obj;
                    return ((song.Title.Equals(this.Title)) && (song.AlbumId == this.AlbumId));
                }
            return false;
        }
    }
}