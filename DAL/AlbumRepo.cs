using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Interfaces;
using Domain;

namespace DAL
{
    public class AlbumRepo : ARInterface
    {
        public AlbumRepo(SRInterface songRepo)
        {
            _songRepo = songRepo;
        } 
        private SRInterface _songRepo;
        public Album GetItemById(int id)
        {
            SqlParameter parameter = new SqlParameter("id", SqlDbType.Int, 50) {Value = id};
            var sqlSelectAlbum = $"SELECT * FROM albums WHERE id = {parameter.Value}";
            RepoInterface<Album> repoInterface = this;
            var res = repoInterface.QueryDB(sqlSelectAlbum);
            if (res.Rows.Count > 0)
            {
                var rawCol = res.Rows[0].ItemArray;
                var alId = Convert.ToInt32(rawCol[0]);
                var alTitle = rawCol[1].ToString();
                var alArtist = rawCol[2].ToString();
                var songList = _songRepo.GetSongsFromAl(alId);        
                return new Album(songList, alId, alTitle, alArtist);    
            }
            return null;
        }

        public Album SaveItem(Album album)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("title", SqlDbType.VarChar, 50) {Value = album.Title},
                new SqlParameter("artist", SqlDbType.VarChar, 50) {Value = album.Artist}
            };
            var sqlInsertAlbum= $"INSERT INTO albums (title, artist) VALUES (\"{parameter[0].Value}\", \"{parameter[1].Value}\" )";
            RepoInterface<Album> repoInterface = this;
            int id = repoInterface.InsertIntoDB(sqlInsertAlbum);
            album.AlbumId = id;
            
            if (album.SongList != null)
                foreach (Song song in album.SongList)
                {
                    song.AlbumId = album.AlbumId;
                    _songRepo.SaveItem(song);
                }
            return album;
        }

        public int DeleteItem(int id)
        {
            SqlParameter parameter = new SqlParameter("id", SqlDbType.Int, 50) {Value = id};
            var sqlDeleteAlbum = $"DELETE FROM albums WHERE id = {parameter.Value}";
            var sqlDeleteCards = $"DELETE FROM songs WHERE album_id = {parameter.Value}";
            RepoInterface<Album> repoInterface = this;
            repoInterface.QueryDB(sqlDeleteAlbum);
            repoInterface.QueryDB(sqlDeleteCards);
            return id;
        }

        public Album UpdateItem(Album album)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("title", SqlDbType.VarChar, 50) {Value = album.Title},
                new SqlParameter("artist", SqlDbType.VarChar, 50) {Value = album.Artist},
                new SqlParameter("id", SqlDbType.Int, 50) {Value = album.AlbumId}
            };
            var sqlUpdateColTitle = $"UPDATE albums SET title = \"{parameter[0].Value}\", artist = \"{parameter[1].Value}\" WHERE id = {parameter[2].Value}";
            RepoInterface<Album> repoInterface = this;
            repoInterface.QueryDB(sqlUpdateColTitle);
            return album;
        }

        public IEnumerable<Album> GetAllItems()
        {
            var sqlSelectAllAlbums = "SELECT * FROM albums ORDER BY id";
            RepoInterface<Album> repoInterface = this;
            var rawCols = repoInterface.QueryDB(sqlSelectAllAlbums);
            var alList = new List<Album>();
            foreach (DataRow dataRow in rawCols.Rows)
            {
                var rawCol = dataRow.ItemArray;
                var alId = Convert.ToInt32(rawCol[0]);
                var songList = _songRepo.GetSongsFromAl(alId);
                var title = rawCol[1].ToString();
                var artist = rawCol[2].ToString();
                var al = new Album(songList, alId, title, artist);
                alList.Add(al);
            }
            return alList;
        }
    }
}