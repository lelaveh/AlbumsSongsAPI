using System.Collections.Generic;
using BLL;
using BLL.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace AlbumsSongsAPI.Controllers
{
    /// <summary>
    /// контроллер api/albums
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        public AlbumsController(ABLInterface albumBll)
        {
            _albumBll = albumBll;
        }
        private ABLInterface _albumBll;
        
        /// <summary>
        /// get api/albums/[id] получение альбома по Id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public ActionResult<Album> Get(int id)
        {
            return _albumBll.GetItemById(id);
        }

        
        /// <summary>
        /// get api/albums получение списка альбомов
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Album>> Get()
        {
            return new ActionResult<IEnumerable<Album>>(_albumBll.GetAllItems());
        }
        
        /// <summary>
        /// post api/albums сохранение альбома
        /// </summary>
        /// <param name="album"></param>
        [HttpPost]
        public ActionResult<Album> Post([FromBody] Album album)
        {
            var savedAlbum = _albumBll.CreateNewItem(album);
            return savedAlbum;
        }
        
        /// <summary>
        /// put api/albums/[id] обновление альбома по id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Album album)
        {
            album.AlbumId = id;
            _albumBll.UpdateItem(album);
        }

        /// <summary>
        /// delete api/albums/[id] удаление альбома по id 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _albumBll.DeleteItemById(id);
        }
    }
}