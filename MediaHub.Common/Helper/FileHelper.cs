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
        public static string BaseFilePath = Directory.GetCurrentDirectory() + "/wwwroot/";

        public static Dictionary<string, string> ContentTypeDic = new Dictionary<string, string>()
        {
            {"jpg","image/jpeg"},
            {"jpeg","image/jpeg"},
            {"jpe","image/jpeg"},
            {"png","image/png"},
            {"gif","image/gif"},
            {"ico","image/x-ico"},
            {"tif","image/tiff"},
            {"tiff","image/tiff"},
            {"fax","image/fax"},
            {"wbmp","image//vnd.wap.wbmp"},
            {"rp","image/vnd.rn-realpix"}
        };

        public async static Task CreateFileAsync(IFormFile file, string relativePath)
        {
            var filePath = Path.Combine(BaseFilePath, relativePath);//创建最终地址
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            using (var stream = new FileStream(filePath + @"\" + file.FileName, FileMode.Create, FileAccess.ReadWrite))
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
            {
                //根据路径字符串判断是文件还是文件夹
                FileAttributes fileAttributes = File.GetAttributes(srcPath);
                await Task.Run(() =>
                {
                    try
                    {
                        if (fileAttributes == FileAttributes.Directory)
                        {
                            //文件夹删除
                            Directory.Delete(srcPath, true);
                        }
                        else
                        {
                            //文件删除
                            File.Delete(srcPath);
                        }
                    }
                    catch { }
                });
            }
        }

        /// <summary>
        /// 获取文件内容类型
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>类型</returns>
        public static string GetContentType(string fileName)
        {
            var typeSplit = fileName.Split('.');
            var fileExtention = typeSplit[typeSplit.Length - 1].ToLower();
            if (!ContentTypeDic.ContainsKey(fileExtention))
            {
                return null;
            }
            else
            {
                return ContentTypeDic[fileExtention];
            }
        }
    }
}
