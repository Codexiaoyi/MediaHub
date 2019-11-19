using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.Common.Helper
{
    public class FileHelper
    {
        public async static Task CreateFileAsync(IFormFile file, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="srcPath"></param>
        public async static Task DeleteFileAsync(string srcPath)
        {
            await Task.Run(() =>
            {
                try
                {
                    File.Delete(srcPath);      //删除指定文件
                }
                catch { }
            });
        }
    }
}
