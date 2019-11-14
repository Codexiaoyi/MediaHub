using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaHub.IRepository;
using MediaHub.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
                    string filePath = Path.Combine(ApplicationEnvironment.ApplicationBasePath,"UpLoad", file.FileName);
                    var saveFile = new FileModel
                    {
                        FileName = file.FileName,
                        FilePath = filePath,
                        ExtensionName = fileExtension,
                        FileSize = file.Length
                    };
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _fileRepository.AddAsync(saveFile);
                }
            }
            return Ok(new { count = files.Count, size = files.Sum(f => f.Length) });
        }
    }
}