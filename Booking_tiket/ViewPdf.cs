using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;
using System.IO;

namespace Booking_tiket
{
    public partial class ViewPdf : Form
    {
        public ViewPdf(byte[] pdfData)
        {
            InitializeComponent();
            LoadPdf(pdfData);
        }
        public void LoadPdf(byte[] pdfData) 
        {
            using (var stream = new MemoryStream(pdfData))
            {
                var pdfDocument = PdfDocument.Load(stream);
                pdfRenderer1.Document = pdfDocument;
            }
        }
    }
}
