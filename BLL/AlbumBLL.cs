using System.Collections.Generic;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Domain;

namespace BLL
{
    public class AlbumBLL : AbstractBLL<Album>, ABLInterface
    {
        private AlbumRepo _albumRepo;

        public AlbumBLL(ARInterface albumRepo)
        {
            _albumRepo = (AlbumRepo) albumRepo;
        }

        public override Album GetItemById(int id)
        {
            var al = _albumRepo.GetItemById(id);
            return al;
        }

        public override Album CreateNewItem(Album album)
        {
            var al = _albumRepo.SaveItem(album);
            return al;
        }

        public override int DeleteItemById(int id)
        {
            return _albumRepo.DeleteItem(id);
        }

        public override Album UpdateItem(Album album)
        {
            return _albumRepo.UpdateItem(album);
        }

        public override IEnumerable<Album> GetAllItems()
        {
            return _albumRepo.GetAllItems();
        }
    }
}