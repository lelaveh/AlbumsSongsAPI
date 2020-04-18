using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Interfaces;
using Domain;

namespace DAL
{
    public class AlbumRepo : Repo<Album>, ARInterface
    {
        public AlbumRepo(SRInterface songRepo)
        {
            _songRepo = (SongRepo) songRepo;
        } 
        private SongRepo _songRepo;
        public override Album GetItemById(int id)
        {
            SqlParameter parameter = new SqlParameter("id", SqlDbType.Int, 50) {Value = id};
            var sqlSelectAlbum = $"SELECT * FROM albums WHERE id = {parameter.Value}";
            var res = QueryDB(sqlSelectAlbum);
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

        public override Album SaveItem(Album album)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("title", SqlDbType.VarChar, 50) {Value = album.Title},
                new SqlParameter("artist", SqlDbType.VarChar, 50) {Value = album.Artist}
            };
            var sqlInsertAlbum= $"INSERT INTO albums (title, artist) VALUES (\"{parameter[0].Value}\", \"{parameter[1].Value}\" )";
            int id = InsertIntoDB(sqlInsertAlbum);
            album.AlbumId = id;
            
            if (album.SongList != null)
                foreach (Song song in album.SongList)
                {
                    song.AlbumId = album.AlbumId;
                    _songRepo.SaveItem(song);
                }
            return album;
        }

        public override int DeleteItem(int id)
        {
            SqlParameter parameter = new SqlParameter("id", SqlDbType.Int, 50) {Value = id};
            var sqlDeleteAlbum = $"DELETE FROM albums WHERE id = {parameter.Value}";
            var sqlDeleteCards = $"DELETE FROM songs WHERE album_id = {parameter.Value}";
            QueryDB(sqlDeleteAlbum);
            QueryDB(sqlDeleteCards);
            return id;
        }

        public override Album UpdateItem(Album album)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("title", SqlDbType.VarChar, 50) {Value = album.Title},
                new SqlParameter("artist", SqlDbType.VarChar, 50) {Value = album.Artist},
                new SqlParameter("id", SqlDbType.Int, 50) {Value = album.AlbumId}
            };
            var sqlUpdateColTitle = $"UPDATE albums SET title = \"{parameter[0].Value}\", artist = \"{parameter[1].Value}\" WHERE id = {parameter[2].Value}";
            QueryDB(sqlUpdateColTitle);
            return album;
        }

        public override IEnumerable<Album> GetAllItems()
        {
            var sqlSelectAllAlbums = "SELECT * FROM albums ORDER BY id";
            var rawCols = QueryDB(sqlSelectAllAlbums);
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