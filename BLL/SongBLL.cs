using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Domain;

namespace BLL
{
    public class SongBLL : SBLInterface
    {
        private readonly SRInterface _songRepo;

        public SongBLL(SRInterface songRepo)
        {
            _songRepo = songRepo;
        }

        public Song GetItemById(int id)
        {
            var song = _songRepo.GetItemById(id);
            return song;
        }
        
        public Song CreateNewItem(Song song)
        {
            var savedSong = _songRepo.SaveItem(song);
            return savedSong;
        }

        public int DeleteItemById(int id)
        {
            return _songRepo.DeleteItem(id);
        }

        public Song UpdateItem(Song song)
        {
            var updatedSong = _songRepo.UpdateItem(song);
            return updatedSong;
        }

        public IEnumerable<Song> GetAllItems()
        {
            return _songRepo.GetAllItems();
        }

        public IEnumerable<Song> GetItemsWithTitle(string title)
        {
            return _songRepo.GetSongsWithTitle(title);
        }
        
        
    }
}