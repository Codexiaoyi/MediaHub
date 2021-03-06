﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediaHub.Common.Helper;
using MediaHub.IRepository;
using MediaHub.Model;
using MediaHub.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        private readonly IFileChunkRepository _fileChunkRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public FileController(IFileChunkRepository fileChunkRepository, IFileRepository fileRepository, IMapper mapper)
        {
            _fileChunkRepository = fileChunkRepository;
            _fileRepository = fileRepository;
            _mapper = mapper;
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
        public async Task<ActionResult> Delete(string id)
        {
            Guid guid = new Guid(id);
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
            var data = Request.Form.Files["data"];
            string lastModified = Request.Form["lastModified"].ToString();
            var total = Request.Form["total"];
            var fileName = Request.Form["fileName"];
            var index = Request.Form["index"];
            var fileSize = long.Parse(Request.Form["fileSize"]);

            string temporaryFile = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/", lastModified);
            try
            {
                if (!Directory.Exists(temporaryFile))
                    Directory.CreateDirectory(temporaryFile);
                string tempPath = Path.Combine(temporaryFile, index.ToString());
                if (!Convert.IsDBNull(data))
                {
                    await FileHelper.CreateFileAsync(data, tempPath);
                }
                bool mergeOk = false;
                if (total == index)
                {
                    var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileName;
                    var finalPath = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/", fileName);//最终的文件名（demo中保存的是它上传时候的文件名，实际操作肯定不能这样）
                    mergeOk = await FileHelper.MergeFileAsync(lastModified, finalPath);
                    if (mergeOk)
                    {
                        var saveFile = new Model.FileModel
                        {
                            FileName = fileName,
                            FilePath = finalPath,
                            ExtensionName = fileExtension,
                            FileSize = fileSize
                        };
                        await _fileRepository.AddAsync(saveFile);
                    }
                }

                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("number", index);
                result.Add("mergeOk", mergeOk);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Directory.Delete(temporaryFile);
                throw ex;
            }
        }

        /// <summary>
        /// 分片上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("upload/chunk")]
        public async Task<ActionResult> UploadChunk([FromForm] FileChunkViewModel fileChunkViewModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var file = fileChunkViewModel.file;//文件
            string temporaryFile = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/", "Chunk" + fileChunkViewModel.Filename);
            if (!Directory.Exists(temporaryFile))
                Directory.CreateDirectory(temporaryFile);
            string tempPath = Path.Combine(temporaryFile, fileChunkViewModel.ChunkNumber.ToString());
            if (!Convert.IsDBNull(file))
            {
                await FileHelper.CreateFileAsync(file, tempPath);
            }

            var fileInfo = _mapper.Map<FileChunk>(fileChunkViewModel);
            int result = await _fileChunkRepository.AddAsync(fileInfo);
            return Ok();
        }

        [HttpPost("upload/merge")]
        public async Task<ActionResult> MergeFile([FromForm] FileModelViewModel fileModelViewModel)
        {
            var lastModified = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/", "Chunk" + fileModelViewModel.FileName);
            var finalPath = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/", fileModelViewModel.FileName);//最终的文件名（demo中保存的是它上传时候的文件名，实际操作肯定不能这样）
            var mergeOk = await FileHelper.MergeFileAsync(lastModified, finalPath);
            if (mergeOk)
            {
                var newFile = _mapper.Map<FileModel>(fileModelViewModel);
                newFile.FilePath = finalPath;
                await _fileRepository.AddAsync(newFile);
            }
            return Ok();
        }
    }
}
