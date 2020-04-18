using System.Collections.Generic;
using DAL;
using Domain;
using NUnit.Framework;

namespace BLL.Test
{
    [TestFixture]
    public class AlbumTest
    {
        private static AlbumBLL albumBll = new AlbumBLL(new AlbumRepo(new SongRepo()));

        [Test]
        public void GetItemByIdTest()
        {
            Album album = new Album(null, -1, "by id", "by id");
            int id = albumBll.CreateNewItem(album).AlbumId;
            Album albumTest = albumBll.GetItemById(id);

            Assert.NotNull(albumTest);
            Assert.AreEqual(album, albumTest);
            albumBll.DeleteItemById(id);
        }


        [Test]
        public void DeleteItemByIdTest()
        {
            Album albumToDelete = new Album(null, -1, "to delete", "to delete");
            int id = albumBll.CreateNewItem(albumToDelete).AlbumId;
            albumBll.DeleteItemById(id);
            Assert.Null(albumBll.GetItemById(id));
        }

        [Test]
        public void UpdateItemTest()
        {
            Album album = new Album(null, -1, "to update", "to update");
            int id = albumBll.CreateNewItem(album).AlbumId;
            Album updatedAlbum = new Album(null, id, "updated", "updated");
            albumBll.UpdateItem(updatedAlbum);
            Album albumTest = albumBll.GetItemById(id);

            Assert.NotNull(albumTest);
            Assert.AreEqual(updatedAlbum, albumTest);
            albumBll.DeleteItemById(id);
        }

        [Test]
        public void GetAllItemsTest()
        {
            Album album1 = new Album(null, -1, "1", "1");
            Album album2 = new Album(null, -1, "2", "2");
            Album album3 = new Album(null, -1, "3", "3");

            int id1 = albumBll.CreateNewItem(album1).AlbumId;
            int id2 = albumBll.CreateNewItem(album2).AlbumId;
            int id3 = albumBll.CreateNewItem(album3).AlbumId;

            List<Album> albums = new List<Album>() {album1, album2, album3};
            List<Album> albumsToTest = albumBll.GetAllItems() as List<Album>;

            Assert.NotNull(albumsToTest);
            Assert.AreEqual(albums, albumsToTest);

            albumBll.DeleteItemById(id1);
            albumBll.DeleteItemById(id2);
            albumBll.DeleteItemById(id3);
        }
    }
}