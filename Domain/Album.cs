using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Domain
{
    public class Album
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }

        public List<Song> SongList { get; set; }

        public string Title { get; set; }
        
        public string Artist { get; set; }

        public Album(List<Song> songs, int albumId = -1, string title = "", string artist = "")
        {
            AlbumId = albumId;
            SongList = songs;
            Title = title;
            Artist = artist;
        }
        
        public Album() {}

        public override bool Equals(object? obj)
        {
            if (obj != null)
                if (obj.GetType() == this.GetType())
                {
                    Album album = (Album) obj;
                    return ((album.Title.Equals(this.Title) && (album.Artist.Equals(this.Artist))));
                }
            return false;
        }
    }
}