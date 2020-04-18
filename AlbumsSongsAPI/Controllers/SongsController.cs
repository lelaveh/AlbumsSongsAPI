using System.Collections.Generic;
using BLL;
using BLL.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace AlbumsSongsAPI.Controllers
{
    /// <summary>
    /// api/songs 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        public SongsController(SBLInterface songBll)
        {
            _songBll = (SongBLL) songBll;
        }

        private SongBLL _songBll;
        
        /// <summary>
        /// GET api/songs список всех песен 
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Song>> Get()
        {
            return new ActionResult<IEnumerable<Song>>(_songBll.GetAllItems());
        }

        /// <summary>
        /// GET api/songs/[id] получение песни по id 
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public ActionResult<Song> Get(int id)
        {
            return _songBll.GetItemById(id);
        }

        
        /// <summary>
        /// POST api/songs сохранение песни 
        /// </summary>
        /// <param name="song"></param>
        [HttpPost]
        public ActionResult<Song> Post([FromBody] Song song)
        {
            //Console.WriteLine(song);
            var savedSong = _songBll.CreateNewItem(song);
            return savedSong;
        }

        
        /// <summary>
        /// PUT api/songs/[id] обновление песни по id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="song"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Song song)
        {
            song.SongId = id;
            _songBll.UpdateItem(song);
        }

        /// <summary>
        /// DELETE api/songs/[id] удаление песни по id 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _songBll.DeleteItemById(id);
        }
        
        
        /// <summary>
        /// GET api/songs/search/?title=[title]  поиск песен по названию 
        /// </summary>
        /// <param name="title"></param>
        [HttpGet("search")]
        public ActionResult<IEnumerable<Song>> Get([FromQuery(Name="title")] string title)
        {
            return new ActionResult<IEnumerable<Song>>(_songBll.GetItemsWithTitle(title));
        }
        
    }
}