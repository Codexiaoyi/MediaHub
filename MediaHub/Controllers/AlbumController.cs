using AutoMapper;
using MediaHub.Common.Helper;
using MediaHub.IRepository;
using MediaHub.Model;
using MediaHub.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaHub.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取相册集
        /// </summary>
        /// <param name="userId">用户的Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Album>>> GetAlbums(Guid userId)
        {
            var albums = await _albumRepository.QueryAlbumsByUserIdAsync(userId);
            if (albums == null || albums.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(albums);
            }
        }

        /// <summary>
        /// 获取相册封面
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [HttpGet("coverimage")]
        //[Authorize]
        public async Task<ActionResult> GetCoverImage(Guid albumId)
        {
            var album = await _albumRepository.QueryByIdAsync(albumId);
            using (var sw = new FileStream(album.CoverUrl, FileMode.Open))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, "image/png");
            }
        }

        /// <summary>
        /// 添加相册
        /// </summary>
        /// <param name="albumViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AlbumViewModel>> AddAlbum([FromForm] AlbumViewModel albumViewModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            //获取图片即封面
            var image = albumViewModel.CoverImage;
            var album = _mapper.Map<Album>(albumViewModel);
            var relativePath = Path.Combine(album.UserId.ToString(), album.Id.ToString(), "CoverImage");//相对地址
            await FileHelper.CreateFileAsync(image, relativePath);//封面图片保存
            album.CoverUrl = Path.Combine(FileHelper.BaseFilePath, relativePath, image.FileName);//最终地址保存到数据库
            var result = await _albumRepository.AddAsync(album);
            if (result > 0)
            {
                return Ok(albumViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 删除相册
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteAlbum(Guid albumId)
        {
            var album = await _albumRepository.QueryByIdAsync(albumId);
            if (album != null)
            {
                var result = await _albumRepository.DeleteAsync(album);
                if (result > 0)
                {
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
