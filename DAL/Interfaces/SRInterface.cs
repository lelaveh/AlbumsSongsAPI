using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface SRInterface : RepoInterface<Song>
    {
        public Song SaveItem(Song song);
        public int DeleteItem(int id);
        public Song UpdateItem(Song song);
        public Song GetItemById(int id);
        public IEnumerable<Song> GetAllItems();
        public List<Song> GetSongsFromAl(int albumId);
        public IEnumerable<Song> GetSongsWithTitle(string title);

    }
}