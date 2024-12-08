using HashNetFramework;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using SendGrid.SmtpApi;
using Spire.Pdf;
using Spire.Pdf.AutomaticFields;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WinSCP;

namespace DusColl
{



    public class PDFEdit
    {


        public static PdfPageTemplateElement CreateFooterTemplate(Spire.Pdf.PdfDocument doc, PdfMargins margins)
        {
            //get page size
            SizeF pageSize = doc.PageSettings.Size;

            //create a PdfPageTemplateElement object which works as footer space
            PdfPageTemplateElement footerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Bottom);
            footerSpace.Foreground = false;

            //declare two float variables
            float x = margins.Left;
            float y = 0;

            //draw line in footer space
            PdfPen pen = new PdfPen(PdfBrushes.Gray, 1);
            footerSpace.Graphics.DrawLine(pen, x, y, pageSize.Width - x, y);

            //draw text in footer space
            y = y + 5;
            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Impact", 10f), true);
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
            String footerText = "E-iceblue Technology Co., Ltd.\nTel:028-81705109\nWebsite:http://www.e-iceblue.com";
            footerSpace.Graphics.DrawString(footerText, font, PdfBrushes.Gray, x, y, format);

            //draw dynamic field in footer space
            PdfPageNumberField number = new PdfPageNumberField();
            PdfPageCountField count = new PdfPageCountField();
            PdfCompositeField compositeField = new PdfCompositeField(font, PdfBrushes.Gray, "Page {0} of {1}", number, count);
            compositeField.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Top);
            SizeF size = font.MeasureString(compositeField.Text);
            compositeField.Bounds = new RectangleF(pageSize.Width - x, y, size.Width, size.Height);
            compositeField.Draw(footerSpace.Graphics);

            //return footerSpace
            return footerSpace;
        }


        public  static PdfPageTemplateElement CreateHeaderTemplate(Spire.Pdf.PdfDocument doc, PdfMargins margins)
        {
            //get page size
            SizeF pageSize = doc.PageSettings.Size;

            //create a PdfPageTemplateElement object as header space
            PdfPageTemplateElement headerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Top);
            headerSpace.Foreground = false;

            //declare two float variables
            float x = margins.Left;
            float y = 0;

            //draw image in header space 
            //Spire.Pdf.Graphics.PdfImage headerImage = PdfSharp.Pdf.Advanced.PdfImage.FromFile("logo.png");
            //float width = headerImage.Width / 3;
            //float height = headerImage.Height / 3;
            //headerSpace.Graphics.DrawImage(headerImage, x, margins.Top - height - 2, width, height);

            //draw line in header space
            PdfPen pen = new PdfPen(PdfBrushes.Gray, 1);
            headerSpace.Graphics.DrawLine(pen, x, y + margins.Top - 2, pageSize.Width - x, y + margins.Top - 2);

            //draw text in header space
            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 25f, FontStyle.Bold));
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
            String headerText = "HEADER TEXT";
            SizeF size = font.MeasureString(headerText, format);
            headerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Gray, pageSize.Width - x - size.Width - 2, margins.Top - (size.Height + 5), format);

            //return headerSpace
            return headerSpace;
        }

        /// <summary>
        /// Find the text and replace in PDF
        /// </summary>
        /// <param name="sourceFile">The source PDF file where text to be searched</param>
        /// <param name="descFile">The new destination PDF file which will be saved with replaced text</param>
        /// <param name="textToBeSearched">The text to be searched in the PDF</param>
        /// <param name="textToBeReplaced">The text to be replaced with</param>
        public void ReplaceTextInPDF(string sourceFile, string descFile, string textToBeSearched, string textToBeReplaced)
        {
            ReplaceText(textToBeSearched, textToBeReplaced, descFile, sourceFile);
        }
        private void ReplaceText(string textToBeSearched, string textToAdd, string outputFilePath, string inputFilePath)
        {
            try
            {
                using (Stream inputPdfStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputPdfStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                using (Stream outputPdfStream2 = new FileStream(outputFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    //Opens the unmodified PDF for reading
                    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(inputPdfStream);
                    //Creates a stamper to put an image on the original pdf
                    PdfStamper stamper = new PdfStamper(reader, outputPdfStream); //{ FormFlattening = true, FreeTextFlattening = true };
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        var tt = new MyLocationTextExtractionStrategy(textToBeSearched,CompareOptions.IgnoreCase);
                        var ex = PdfTextExtractor.GetTextFromPage(reader, i, tt); // ex will be holding the text, although we just need the rectangles [RectAndText class objects]
                        foreach (var p in tt.myPoints)
                        {
                            //Creates an image that is the size i need to hide the text i'm interested in removing
                            Bitmap transparentBitmap = new Bitmap((int)p.Rect.Width, (int)p.Rect.Height);
                            transparentBitmap.MakeTransparent();
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(transparentBitmap, new BaseColor(255, 255, 255));
                            //Sets the position that the image needs to be placed (ie the location of the text to be removed)
                            image.SetAbsolutePosition(p.Rect.Left, (p.Rect.Top - 8));
                            //Adds the image to the output pdf
                            stamper.GetOverContent(i).AddImage(image, true); // i stands for the page no.

                            PdfContentByte cb = stamper.GetOverContent(i);

                            // select the font properties
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetColorFill(BaseColor.BLACK);
                            cb.SetFontAndSize(bf, 7);

                            // write the text in the pdf content
                            cb.BeginText();
                            // put the alignment and coordinates here
                            cb.ShowTextAligned(1, textToAdd, p.Rect.Left + 10, p.Rect.Top - 6, 0);
                            cb.EndText();
                        }
                    }
                    //Creates the first copy of the outputted pdf
                    stamper.Close();

                }
            }
            catch (Exception ex)
            {
            }
        }
        public class RectAndText
        {
            public iTextSharp.text.Rectangle Rect;
            public String Text;
            public RectAndText(iTextSharp.text.Rectangle rect, String text)
            {
                this.Rect = rect;
                this.Text = text;
            }
        }
        public class MyLocationTextExtractionStrategy : LocationTextExtractionStrategy
        {
            //Hold each coordinate
            public List<RectAndText> myPoints = new List<RectAndText>();

            //The string that we're searching for
            public String TextToSearchFor { get; set; }

            //How to compare strings
            public System.Globalization.CompareOptions CompareOptions { get; set; }

            public MyLocationTextExtractionStrategy(String textToSearchFor, System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.None)
            {
                this.TextToSearchFor = textToSearchFor;
                this.CompareOptions = compareOptions;
            }

            //Automatically called for each chunk of text in the PDF
            public override void RenderText(TextRenderInfo renderInfo)
            {

                base.RenderText(renderInfo);
                //See if the current chunk contains the text
                var startPosition = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(renderInfo.GetText(), this.TextToSearchFor, this.CompareOptions);

                //If not found bail
                if (startPosition < 0)
                {
                    return;
                }

                //Grab the individual characters
                var chars = renderInfo.GetCharacterRenderInfos().Skip(startPosition).Take(this.TextToSearchFor.Length).ToList();

                //Grab the first and last character
                var firstChar = chars.First();
                var lastChar = chars.Last();


                //Get the bounding box for the chunk of text
                var bottomLeft = firstChar.GetDescentLine().GetStartPoint();
                var topRight = lastChar.GetAscentLine().GetEndPoint();

                //Create a rectangle from it
                var rect = new iTextSharp.text.Rectangle(bottomLeft[Vector.I1], bottomLeft[Vector.I2], topRight[Vector.I1], topRight[Vector.I2]);

                //Add this to our main collection
                this.myPoints.Add(new RectAndText(rect, this.TextToSearchFor));
            }

        }
    }



    public class coba
    {
        public Session SesWINCP { get; set; }
    }

    public static class MessageEmail
    {
        public static int Errorint { get; set; }
        public async static Task<int> sendEmail(int EmailTyped, string toEmailAddress, string valueExtend = "", string DearEmail = "", string namaPT = "")
        {

            bool needcc = false;
            var xmstpapiJson = "";

            try
            {

                string htmlString = "";
                string subject = "";

                vmCommonddl Commonddl = new vmCommonddl();
                List<cConfig> config = await Commonddl.dbGetConfig("SMTP");

                config = config.Where(x => x.Name == "SMTP").ToList();

                int portmailserver = Int32.Parse(config[0].Code.ToString());
                string Hostserver = HasKeyProtect.Decryption(config[1].Code);
                string formemailserver = ""; //HasKeyProtect.Decryption(config[2].Code);
                string formemailpassserver = ""; //HasKeyProtect.Decryption(config[3].Code);
                string reportemail = ""; //HasKeyProtect.Decryption(config[4].Code);
                string authenuser = HasKeyProtect.Decryption(config[2].Code);
                string authenpass = HasKeyProtect.Decryption(config[3].Code);


                if (EmailTyped == (int)EmailType.ResetPassword)
                {
                    subject = "PT SDB : Perubahan kata Sandi - verifikasi Kode";
                    htmlString = "Untuk mengganti kata sandi anda, silahkan gunakan kode verifikasi berikut : <br />" +
                                 "<b><h2>Kode verifikasi : " + valueExtend + "</h2> </b>" +
                                 "<br /><br /><br /><br /><br />" +
                                 "Terima Kasih <br />" +
                                 "Admin SDB";

                }
                else if (EmailTyped == (int)EmailType.userwronglogin)
                {
                    subject = "PT SDB : Perubahan kata Sandi - verifikasi Kode";
                    htmlString = "Untuk membuka user pengguna anda yang terkunci, silahkan gunakan kode verifikasi berikut : <br />" +
                                 "<b><h2>Kode verifikasi : " + valueExtend + "</h2> </b>" +
                                 "<br /><br /><br /><br /><br />" +
                                 "Terima Kasih <br />" +
                                 "Admin SDB";

                }

                else if (EmailTyped == (int)EmailType.userchangeforceglogin)
                {
                    subject = "PT SDB : Perubahan kata Sandi - verifikasi Kode";
                    htmlString = "Untuk pengamanan user anda, Kata sandi anda harus dirubah, silahkan gunakan kode verifikasi berikut : <br />" +
                                 "<b><h2>Kode verifikasi : " + valueExtend + "</h2> </b>" +
                                 "<br /><br /><br /><br /><br />" +
                                 "Terima Kasih <br />" +
                                 "Admin SDB";

                }
                else if (EmailTyped == (int)EmailType.OrderNotaris)
                {
                    needcc = true;
                    subject = "PT SDB : Order Notaris - Permohonan Nomor akta";
                    htmlString = "Salam Hormat " + DearEmail + ", <br /> <br /> " +
                                 "Anda mendapatkan order sebanyak  : " + valueExtend + " Kontrak <br />" +
                                 "mohon untuk segera diproses" +
                                 "<br /><br /><br /><br /><br />" +
                                 "Terima Kasih <br />" +
                                 "Admin SDB";
                }
                else if (EmailTyped == (int)EmailType.AktaNotaris)
                {
                    toEmailAddress = reportemail;
                    subject = "PT SDB : Generate NoAkta Notaris - Permohonan Nomor akta";
                    htmlString = "Salam Hormat Team SDB, <br /> <br /> " +
                                 "Sebanyak  : " + valueExtend + " Kontrak Berhasil Digenerate NoAkta oleh notaris " + DearEmail + "<br/>" +
                                 "mohon untuk segera diproses" +
                                 "<br /><br /><br /><br /><br />" +
                                 "Terima Kasih <br />" +
                                 "Admin SDB";
                }
                else if (EmailTyped == (int)EmailType.OtpOrderFidusia)
                {
                    subject = "PT SDB : Pengajuan Pendaftaran Fidusia - verifikasi Kode";
                    htmlString = "berikut adalah kode verifikasi untuk konfirmasi Pengajuan Pendaftaran Fidusia: <br />" +
                                 "<b><h2>Kode verifikasi : " + valueExtend + "</h2> </b>" +
                                 "<br /><br /><br /><br /><br />" +
                                 "Terima Kasih <br />" +
                                 "Admin SDB";

                }
                else if (EmailTyped == (int)EmailType.useraktivasi)
                {
                    subject = namaPT + " : Aktivasi - ID Pengguna";
                    htmlString = "Anda berhasil melakukan Registrasi ID Pengguna, untuk melakukan aktivasi silahkan klik link beirkut : <br />" +
                                 "<b><h2>" + valueExtend + "</h2> </b>" +
                                 "<br /><br /><br /><br /><br />" +
                                 "Terima Kasih <br />" +
                                 namaPT;

                }




                MailMessage message = new MailMessage();
                message.From = new MailAddress(authenuser);
                message.To.Add(toEmailAddress);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlString;

                //Hostserver : smtp.gmail.com
                //portmailserver : 587
                //authenuser :fdcmnotifalert@gmail.com
                //authenpass : notifalert

                var client = new SmtpClient(Hostserver, portmailserver)
                {
                    Credentials = new NetworkCredential(authenuser, authenpass),
                    EnableSsl = true

                };
                client.Send(message);

                Errorint = 1;

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                OwinLibrary.CreateLog(toEmailAddress, "LogErrorFDCM.txt");
                OwinLibrary.CreateLog(xmstpapiJson, "LogErrorFDCM.txt");
                Errorint = (int)ProccessOutput.FilterValidEmailChangePassword;
                OwinLibrary.CreateLog(msg, "LogErrorFDCM.txt");
            }

            return Errorint;
        }
    }




    public static class DttpPdf
    {


        public static byte[] exportpdf(DataTable dtEmployee, string caption = "")
        {

            // creating document object  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            Document doc = new Document(rec, 10, 10, 10, 10);
            //doc.SetPageSize(iTextSharp.text.PageSize.A4); patroit
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate()); //lanscape
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            //Creating paragraph for header  
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 12, 1, iTextSharp.text.BaseColor.BLACK);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_LEFT;
            prgHeading.Add(new Chunk(caption.ToUpper(), fntHead));
            doc.Add(prgHeading);

            //Adding paragraph for report generated by  
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 9, 2, iTextSharp.text.BaseColor.BLACK);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            //prgGeneratedBY.Add(new Chunk("Report Generated by : ASPArticles", fntAuthor));  
            //prgGeneratedBY.Add(new Chunk("\nGenerated Date : " + DateTime.Now.ToShortDateString(), fntAuthor));  
            doc.Add(prgGeneratedBY);

            //Adding a line  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(p);

            //Adding line break  
            //doc.Add(new Chunk("\n", fntHead));

            //Adding  PdfPTable  
            PdfPTable table = new PdfPTable(dtEmployee.Columns.Count) { HorizontalAlignment = Element.ALIGN_CENTER, WidthPercentage = 100 };
            table.SetWidths(new float[] { 1f, 5f, 5f, 3f, 8f, 4f, 4f, 4f, 4f });
            for (int i = 0; i < dtEmployee.Columns.Count; i++)
            {
                string cellText = HttpUtility.HtmlDecode(dtEmployee.Columns[i].ColumnName);
                PdfPCell cell = new PdfPCell();
                cell.Phrase = new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#C8C8C8"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.PaddingBottom = 10;
                table.AddCell(cell);
            }

            //writing table Data  
            PdfPCell cellrow = new PdfPCell();
            double tot1 = 0;
            double tot2 = 0;
            iTextSharp.text.Font fntrow = new iTextSharp.text.Font(bfntHead, 10, 1, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000")));
            for (int i = 0; i < dtEmployee.Rows.Count; i++)
            {
                for (int j = 0; j < dtEmployee.Columns.Count; j++)
                {
                    cellrow.HorizontalAlignment = Element.ALIGN_LEFT;
                    if (dtEmployee.Columns[j].DataType.ToString().ToLower().Contains("decimal"))
                    {
                        cellrow.Phrase = new Phrase(double.Parse(dtEmployee.Rows[i][j].ToString()).ToString("N0"), fntrow);
                        cellrow.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }
                    else if (dtEmployee.Columns[j].DataType.ToString().ToLower().Contains("date") && dtEmployee.Rows[i][j].ToString()!="")
                    {
                        cellrow.Phrase = new Phrase(DateTime.Parse(dtEmployee.Rows[i][j].ToString()).ToString("dd/MM/yyyy"), fntrow);
                    }
                    else
                    {
                        cellrow.Phrase = new Phrase(dtEmployee.Rows[i][j].ToString(), fntrow);
                    }
                    table.AddCell(cellrow);
                }
            }

            //tot1 = Convert.ToDouble(dtEmployee.Compute("SUM([Nilai HT])", string.Empty));
            //cellrow.Phrase = new Phrase("Total", fntrow);
            //cellrow.Colspan = 6;
            //table.AddCell(cellrow);

            doc.Add(table);
            doc.Close();

            byte[] result = ms.ToArray();
            return result;
        }

    }


}

