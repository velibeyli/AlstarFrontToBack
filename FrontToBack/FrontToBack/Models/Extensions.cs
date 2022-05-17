using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FrontToBack.Models
{
    public static class Extensions
    {
        public static bool CheckImageType(HttpPostedFileBase Image)
        {
            return Image.ContentType == "image/jpg" || Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/gif";
        }

        public static bool CheckImageSize(HttpPostedFileBase Image, int mb)
        {
            return Image.ContentLength / 1024 / 1024 < mb;
        }

        public static string SaveImage(string folder, HttpPostedFileBase Image)
        {
            string fileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + Path.GetFileName(Image.FileName);

            string path = Path.Combine(folder, fileName);

            Image.SaveAs(path);
            return path;
        }
        public static bool DeleteImage(string folder, string fileName)
        {
            string pathToImage = Path.Combine(folder,fileName);

            if (File.Exists(pathToImage))
            {
                File.Delete(pathToImage);

                return true;
            }

            return false;
        }
    }
}