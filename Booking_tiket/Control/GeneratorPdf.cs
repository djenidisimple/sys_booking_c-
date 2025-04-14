using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookong_tiket;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;


namespace Booking_tiket.Controler
{
    class GeneratorPdf
    {
        public void GeneratorTicket(string path, List<Billet> ticket) 
        {
            int i = 0;
                using (Document doc = new Document(PageSize.A4, 10, 10, 10, 10)) 
                {
                    using (FileStream stream = new FileStream(path, FileMode.Create)) 
                    {
                        PdfWriter writer = PdfWriter.GetInstance(doc, stream);
                        doc.Open();

                        while (i < ticket.Count)
                        {
                    

                            PdfPTable table = new PdfPTable(2);
                            table.WidthPercentage = 100;

                            PdfPCell cellLeft = new PdfPCell();
                            cellLeft.Border = PdfPCell.NO_BORDER;
                            cellLeft.AddElement(new Paragraph(new Paragraph("BILLET DE TRAIN", Constant.policeTitre)));

                            cellLeft.AddElement(new Paragraph("Départ : " + ticket[i].depart + "\nArrivée : " + ticket[i].arriver + "", Constant.policeInfos));
                            cellLeft.AddElement(new Paragraph("Date : " + ticket[i].dateDepart.ToString("dd/MM/yyyy HH:mm") + "", Constant.policeInfos));
                            cellLeft.AddElement(new Paragraph("Train : " + ticket[i].nameTrain + "", Constant.policeInfos));

                            PdfPCell cellRight = new PdfPCell();
                            cellRight.Border = PdfPCell.NO_BORDER;

                            //------------
                            //Barcode here

                            Barcode128 code = new Barcode128
                            {
                                CodeType = Barcode128.CODE128,
                                Code = ticket[i].CodeBarres
                            };

                            Image codeImage = code.CreateImageWithBarcode(writer.DirectContent, null, null);
                            codeImage.ScaleAbsolute(120, 40);
                            cellRight.AddElement(codeImage);
                            //------------

                            GenererQrCode qr = new GenererQrCode();
                            cellRight.AddElement(qr.GeneQrCode(ticket[i].CodeBarres));

                            cellRight.AddElement(new Paragraph("Place : " + ticket[i].seatNumber, Constant.policePetit));

                            table.AddCell(cellLeft);
                            table.AddCell(cellRight);
                            doc.Add(table);

                            doc.Add(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, 1)));

                            PdfPTable tableBase = new PdfPTable(3);
                            tableBase.AddCell(new PdfPCell(new Phrase("Passager: \n" + ticket[i].passager + "", Constant.policePetit)));
                            tableBase.AddCell(new PdfPCell(new Phrase("Prix: \n" + ticket[i].price + "Ar", Constant.policePetit)));
                            tableBase.AddCell(new PdfPCell(new Phrase("Reference: \n" + ticket[i].reference + "", Constant.policePetit)));

                            doc.Add(tableBase);

                            i++;
                            if (i <= ticket.Count)
                                doc.NewPage();
                        }
                        doc.Close();
                }
            }
        }
    }
}
