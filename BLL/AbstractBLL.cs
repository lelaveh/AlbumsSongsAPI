using System.Collections.Generic;

namespace BLL
{
    public abstract class AbstractBLL<T>
    {
        public abstract T GetItemById(int id);
        public abstract T CreateNewItem(T item);

        public abstract int DeleteItemById(int id);

        public abstract T UpdateItem(T item);

        public abstract IEnumerable<T> GetAllItems();

    }
}