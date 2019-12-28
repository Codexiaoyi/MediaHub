using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async static Task<bool> MergeFileAsync(string lastModified, string finalPath)
        {
            bool ok = false;
            try
            {
                var temporary = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/", lastModified);//临时文件夹
                var files = Directory.GetFiles(temporary);//获得下面的所有文件
                using (var fs = new FileStream(finalPath, FileMode.Create))
                {
                    foreach (var part in files.OrderBy(x => x.Length).ThenBy(x => x))//排序，保证从0-N
                    {
                        var bytes = await File.ReadAllBytesAsync(part);
                        await fs.WriteAsync(bytes, 0, bytes.Length);
                        File.Delete(part);
                    }
                }
                Directory.Delete(temporary);
                ok = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ok;
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
