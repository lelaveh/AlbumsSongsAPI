using System.Collections.Generic;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Domain;

namespace BLL
{
    public class AlbumBLL :  ABLInterface
    {
        private readonly ARInterface _albumRepo;

        public AlbumBLL(ARInterface albumRepo)
        {
            _albumRepo = albumRepo;
        }

        public Album GetItemById(int id)
        {
            var al = _albumRepo.GetItemById(id);
            return al;
        }

        public Album CreateNewItem(Album album)
        {
            var al = _albumRepo.SaveItem(album);
            return al;
        }

        public int DeleteItemById(int id)
        {
            return _albumRepo.DeleteItem(id);
        }

        public Album UpdateItem(Album album)
        {
            return _albumRepo.UpdateItem(album);
        }

        public IEnumerable<Album> GetAllItems()
        {
            return _albumRepo.GetAllItems();
        }
    }
}