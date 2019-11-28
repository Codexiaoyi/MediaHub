using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaHub.IRepository;
using MediaHub.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace MediaHub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;
        

        public ValuesController(IHostingEnvironment environment)
        {
            _environment = environment;
            
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult> Get()
        {
            return Ok(new { a = 1 });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(new { a = 1 });
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            var files = Request.Form.Files;
            //foreach (var file in files)
            //{
            //    if (file.Length > 0)
            //    {
            //        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            //        string filePath = _environment.WebRootPath + "/UploadFile/" + DateTime.Now + "." + fileExtension;
            //        var saveFile = new FileModel
            //        {
            //            FileName = file.FileName,
            //            FilePath = filePath,
            //            ExtensionName = fileExtension,
            //            FileSize = file.Length
            //        };
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await file.CopyToAsync(stream);
            //        }
            //        await _fileRepository.AddAsync(saveFile);
            //    }
            //}
            return Ok(new { count = files.Count, size = files.Sum(f => f.Length) });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
