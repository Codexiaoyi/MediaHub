using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaHub.Common.Helper;
using MediaHub.IRepository;
using MediaHub.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.DotNet.PlatformAbstractions;

namespace MediaHub.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        /// <summary>
        /// 获取所有文件信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("files")]
        public async Task<ActionResult> Get()
        {
            var result = await _fileRepository.QueryAllFileAsync();
            return Ok(new { result, success = true });
        }

        [HttpGet("download")]
        public async Task<FileResult> Download(string id)
        {
            //Guid guid = new Guid(Request.QueryString.Value.Remove(0, 3));
            Guid guid = new Guid(id);
            var file = await _fileRepository.QueryByIdAsync(guid);
            var filePath = file.FilePath;
            var stream = System.IO.File.OpenRead(filePath);
            var provider = new FileExtensionContentTypeProvider();
            var contentType = provider.Mappings[file.ExtensionName];
            return File(stream, contentType, file.FileName);
        }

        /// <summary>
        /// 根据ID删除文件
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete()
        {
            Guid guid = new Guid(Request.QueryString.Value.Remove(0, 3));
            var file = await _fileRepository.QueryByIdAsync(guid);
            var filePath = file.FilePath;
            await FileHelper.DeleteFileAsync(filePath);//删除相应文件
            //删除数据库中文件实体
            var result = await _fileRepository.DeleteAsync(file);
            if (result > 0)
            {
                return Ok(new { success = true });
            }
            else
            {
                return Ok(new { success = false });
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<ActionResult> Post()
        {
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    string filePath = Path.Combine(ApplicationEnvironment.ApplicationBasePath, DateTime.Now.Ticks.ToString(), file.FileName);
                    var saveFile = new FileModel
                    {
                        FileName = file.FileName,
                        FilePath = filePath,
                        ExtensionName = fileExtension,
                        FileSize = file.Length
                    };
                    await FileHelper.CreateFileAsync(file, filePath);
                    await _fileRepository.AddAsync(saveFile);
                }
            }
            return Ok(new { count = files.Count, size = files.Sum(f => f.Length), success = true });
        }
    }
}