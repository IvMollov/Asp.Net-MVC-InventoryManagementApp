using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string CreateBase64Image(this HtmlHelper helper, byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return string.Empty;
            }
            Image streamImage;
            TypeConverter typeConvertor = TypeDescriptor.GetConverter(typeof(Bitmap));
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {

                Bitmap bitmapImg = (Bitmap)typeConvertor.ConvertFrom(fileBytes);

                streamImage = (Image)bitmapImg;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                streamImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}