using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Domain;

namespace BLL
{
    public class SongBLL : AbstractBLL<Song>, SBLInterface
    {
        private readonly SongRepo _songRepo;

        public SongBLL(SRInterface songRepo)
        {
            _songRepo = (SongRepo) songRepo;
        }

        public override Song GetItemById(int id)
        {
            var song = _songRepo.GetItemById(id);
            return song;
        }
        
        public override Song CreateNewItem(Song song)
        {
            var savedSong = _songRepo.SaveItem(song);
            return savedSong;
        }

        public override int DeleteItemById(int id)
        {
            return _songRepo.DeleteItem(id);
        }

        public override Song UpdateItem(Song song)
        {
            var updatedSong = _songRepo.UpdateItem(song);
            return updatedSong;
        }

        public override IEnumerable<Song> GetAllItems()
        {
            return _songRepo.GetAllItems();
        }

        public IEnumerable<Song> GetItemsWithTitle(string title)
        {
            return _songRepo.GetSongsWithTitle(title);
        }
        
        
    }
}