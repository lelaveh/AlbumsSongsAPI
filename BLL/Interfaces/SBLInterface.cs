using System.Collections.Generic;
using Domain;

namespace BLL.Interfaces
{
    public interface SBLInterface : AbstractBLL<Song>
    {
        public abstract Song GetItemById(int id);
        public abstract Song CreateNewItem(Song item);

        public abstract Song UpdateItem(Song item);

        public abstract IEnumerable<Song> GetAllItems();

        public abstract IEnumerable<Song> GetItemsWithTitle(string title);
    }
}