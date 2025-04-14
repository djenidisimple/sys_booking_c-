using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;
using iTextSharp.text;
using System.IO;

namespace Bookong_tiket
{
    class GenererQrCode
    {
        public Image GeneQrCode(string content, int taille = 30) 
        {
            var option = new QrCodeEncodingOptions
            {
                Width = taille,
                Height = taille,
                Margin = 1,
                DisableECI = true,
                CharacterSet = "UTF-8"
            };
            var writter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = option
            };

            System.Drawing.Bitmap qrBitmap = writter.Write(content);

            using (MemoryStream ms = new MemoryStream()) 
            {
                qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Image.GetInstance(ms.ToArray());
            }
        }
    }
}
