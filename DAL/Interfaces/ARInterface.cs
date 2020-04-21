using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface ARInterface : RepoInterface<Album>
    {
        public new  Album GetItemById(int id);

        public Album SaveItem(Album album);
        public int DeleteItem(int id);
        public Album UpdateItem(Album album);
        public IEnumerable<Album> GetAllItems();
    }
}