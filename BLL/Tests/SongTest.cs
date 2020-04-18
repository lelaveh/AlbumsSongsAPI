using System;
using System.Collections;
using System.Collections.Generic;
using DAL;
using Domain;
using NUnit.Framework;

namespace BLL.Test
{
    [TestFixture]
    public class SongTest
    {
        static SongBLL songBll = new SongBLL(new SongRepo());

        [Test]
        public void GetItemByIdTest()
        {
            var song = new Song(-1, "Фольксваген", "Новый хит", 32);
            int id = songBll.CreateNewItem(song).SongId;
            var testSong = songBll.GetItemById(id);

            Assert.NotNull(testSong);
            Assert.AreEqual(song, testSong);
            songBll.DeleteItemById(id);
        }
        
        //
        [Test]
        public void UpdateItemTest()
        {
            var song = new Song(-1, "Song to update", "Update desc", 12);
            int id = songBll.CreateNewItem(song).SongId;
            var updatedSong = new Song(id, "Updated song", "Updated desc", 12);
            songBll.UpdateItem(updatedSong);

            var songTest = songBll.GetItemById(id);
            Assert.NotNull(songTest);
            Assert.AreEqual(updatedSong, songTest);
            songBll.DeleteItemById(id);

        }
        
        [Test]
        public void GetItemsWithTitleTest()
        {
            var song1 = new Song(-1, "default", "default1", -1);
            var song2 = new Song(-1, "default", "default2", -1);
            int id1 = songBll.CreateNewItem(song1).SongId;
            int id2 = songBll.CreateNewItem(song2).SongId;
            
            List<Song> songs = new List<Song>() {song1, song2};
        
            List<Song> testedSongs = songBll.GetItemsWithTitle("default") as List<Song>;
        
            Assert.NotNull(testedSongs);
            Assert.AreEqual(songs, testedSongs);
            songBll.DeleteItemById(id1);
            songBll.DeleteItemById(id2);
        }
        
        [Test]
        public void GetAllItemsTest()
        {
            var song1 = new Song(-1, "default1", "default1", -1);
            var song2 = new Song(-1, "default2", "default2", -1);
            var song3 = new Song(-1, "default3", "default3", -1);
            int id1 = songBll.CreateNewItem(song1).SongId;
            int id2 = songBll.CreateNewItem(song2).SongId;
            int id3 = songBll.CreateNewItem(song3).SongId;
            List<Song> allSongs = new List<Song>() {song1, song2, song3};
            List<Song> getAllSongs = songBll.GetAllItems() as List<Song>;
            
            Assert.NotNull(getAllSongs);
            Assert.AreEqual(allSongs, getAllSongs);
            songBll.DeleteItemById(id1);
            songBll.DeleteItemById(id2);
            songBll.DeleteItemById(id3);
        }

        [Test]
        public void DeleteItemByIdTest()
        {
            Song songToDelete = new Song(-1, "song to delete", "song to delete", -1 );
            int idToDelete = songBll.CreateNewItem(songToDelete).SongId;
            songBll.DeleteItemById(idToDelete);
            Assert.Null(songBll.GetItemById(idToDelete));
        }
        
        
        
        
    }
}