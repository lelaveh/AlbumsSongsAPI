using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Interfaces;
using Domain;

namespace DAL
{
    public class SongRepo : SRInterface
    {        
        public Song SaveItem(Song song)
        {
            SqlParameter[] parameters =
            {    
                new SqlParameter("@title", SqlDbType.VarChar, 50) { Value = song.Title },
                new SqlParameter("@descr", SqlDbType.VarChar, 50) { Value = song.Description },
                new SqlParameter("@album_id", SqlDbType.Int, 50) { Value = song.AlbumId},
            };
            var sqlInsertString = $"INSERT INTO songs (title, descr, album_id) VALUES (\"{parameters[0].Value}\" , \"{parameters[1].Value}\", \"{parameters[2].Value}\")";
            SRInterface srInterface = this;
            int id = srInterface.InsertIntoDB(sqlInsertString);
            song.SongId = id;
            return song;
        }

        public int DeleteItem(int id)
        {
            SqlParameter parameter = new SqlParameter("id", SqlDbType.Int, 50) {Value = id};
            var sqlDeleteString = $"DELETE FROM songs WHERE id = {parameter.Value}";
            SRInterface srInterface = this;
            srInterface.QueryDB(sqlDeleteString);
            return id;
        }

        public Song UpdateItem(Song song)
        {
            SqlParameter[] parameters =
            {    
                new SqlParameter("@title", SqlDbType.VarChar, 50) { Value = song.Title },
                new SqlParameter("@descr", SqlDbType.VarChar, 50) { Value = song.Description },
                new SqlParameter("@album_id", SqlDbType.Int, 50) { Value = song.AlbumId},
                new SqlParameter("@song_id", SqlDbType.Int, 50) { Value = song.SongId},
            };
            var sqlUpdate = "UPDATE songs " +
                            $"SET title=\"{parameters[0].Value}\", " +
                            $"descr=\"{parameters[1].Value}\"," +
                            $"album_id={parameters[2].Value} " +
                            $"WHERE songs.id = {parameters[3].Value}";
            SRInterface srInterface = this;
            srInterface.QueryDB(sqlUpdate);
            return song;
        }

        public Song GetItemById(int id)
        {
            SqlParameter sqlParameter = new SqlParameter("id", SqlDbType.Int, 50) {Value = id};
            var sqlDeleteString = $"SELECT * FROM songs WHERE id = {sqlParameter.Value}"; 
            SRInterface srInterface = this;
            var res = srInterface.QueryDB(sqlDeleteString); 
            if (res.Rows.Count > 0)
            {
                var data = res.Rows[0].ItemArray;
                var song = new Song(Convert.ToInt32(data[0]), (string) data[1],
                    (string) data[2], Convert.ToInt32(data[3]));
                return song;    
            }
            return null;
        }
        
        public IEnumerable<Song> GetAllItems()
        {
            var sqlSelectAllSongs = "SELECT * FROM songs ORDER BY songs.id";
            SRInterface srInterface = this;
            var rawList = srInterface.QueryDB(sqlSelectAllSongs);
            var resList = new List<Song>();
            foreach (DataRow row in rawList.Rows)
            {
                var song = new Song(Convert.ToInt32(row[0]), (string) row[1], 
                    (string) row[2], Convert.ToInt32(row[3]));
                resList.Add(song);
            }
            
            return resList;
        }

        public List<Song> GetSongsFromAl(int albumId)
        {
            SqlParameter parameter = new SqlParameter("albumId", SqlDbType.Int, 50) {Value = albumId}; 
            string sqlSelectSongs = $"SELECT * FROM songs WHERE album_id = {parameter.Value} ORDER BY id";
            SRInterface srInterface = this;
            var rawSongs = srInterface.QueryDB(sqlSelectSongs).Rows;
            List<Song>songList = new List<Song>();
            for(int i = 0; i < rawSongs.Count; i++)
            {
                var rawSong = rawSongs[i].ItemArray;
                var song = new Song(Convert.ToInt32(rawSong[0]), rawSong[1].ToString(), 
                    rawSong[2].ToString(), Convert.ToInt32(rawSong[3]));
                songList.Add(song);
            }

            return songList;
        }

        public IEnumerable<Song> GetSongsWithTitle(string title)
        {
            SqlParameter parameter = new SqlParameter("title", SqlDbType.VarChar, 50) {Value = title};
            string sqlSelectSongsWithString = $"SELECT * FROM songs WHERE title = \"{parameter.Value}\" ORDER BY id";
            SRInterface srInterface = this;
            var rawRes = srInterface.QueryDB(sqlSelectSongsWithString).Rows;
            List<Song> songList = new List<Song>();
            foreach (DataRow dataRow in rawRes)
            {
                var rawSong = dataRow.ItemArray;
                var song = new Song(Convert.ToInt32(rawSong[0]), rawSong[1].ToString(),
                    rawSong[2].ToString(), Convert.ToInt32(rawSong[3]));
                songList.Add(song);
            }

            return songList;
        }
    }
}