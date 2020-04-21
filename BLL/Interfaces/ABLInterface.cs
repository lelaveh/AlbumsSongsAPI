using System.Collections.Generic;
using Domain;

namespace BLL.Interfaces
{
    public interface ABLInterface : AbstractBLL<Album>
    {
        public abstract Album GetItemById(int id);
        public abstract Album CreateNewItem(Album item);

        public abstract Album UpdateItem(Album item);

        public abstract IEnumerable<Album> GetAllItems();
    }
}