using HashNetFramework;
using Newtonsoft.Json;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DusColl.Controllers
{
    public class FidusiaController : Controller
    {

        public ActionResult clnPendaftaranAHU()
        {
            return Json(new
            {
                moderror = "",
                url = "",
                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Fidusia/_uiGridAHURegis.cshtml", null),
                result = "",
            });
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SetRegisAHU()
        {

            int result = 0;
            try
            {
                string SertiAHU = HttpContext.Request.Params["SertiAHU"];
                cDocuments Param = JsonConvert.DeserializeObject<cDocuments>(SertiAHU);

                HttpPostedFileBase filbs = Request.Files[0];
                byte[] FileByte = null;
                BinaryReader reader = new BinaryReader(filbs.InputStream);
                FileByte = reader.ReadBytes((int)filbs.ContentLength);
                string filename = filbs.FileName;

                string pathFolder = "D:\\FileMTF\\Sertifikat\\";
                //string sourcefile = Param.DOCUMENT_TYPE + "_" + Param.PATH_DESTINATION_FILENAME.Replace("_ECP", "");
                if ((Param.DOCUMENT_TYPE.ToLower().ToString() == "bukti objek"))
                {
                    FileByte = ConvertHTMLToPDFObjectSaveToBYTE(FileByte);

                    
                    //Document pdfDoc = new Document(iTextSharp.text.PageSize.A2, 10f, 10f, 10f, 0f);
                    //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    //PdfWriter.GetInstance(pdfDoc, filbs.InputStream);
                    //pdfDoc.Open();
                    //htmlparser.Parse(sr);
                    //pdfDoc.Close();

                    pathFolder = "D:\\FileMTF\\BuktiObjek\\";
                }

                if (Param.ISECP == true)
                {
                    //prepare to encrypt
                    string KECEP = "dodol";
                    string KECEPDB = KECEP;
                    KECEP = HasKeyProtect.Encryption(KECEP);
                    byte[] filebyteECP = HasKeyProtect.SetFileByteEncrypt(FileByte, KECEP);
                    FileByte = filebyteECP;
                    //convert byte to base//
                    //string base64String = Convert.ToBase64String(filebyteECP, 0, filebyteECP.Length);
                }
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                System.IO.File.WriteAllBytes(pathFolder + filename + ".pdf", FileByte);

                result = 1;
            }
            catch (Exception xc)
            {
                result = 0;
            }

            return Json(new
            {
                moderror = "",
                url = "",
                view = "",
                resulted = result,
            });

        }
        public ActionResult clnGetPendingDataAHU(string type)
        {

            var resultJSON = "";
            var resultJSON1 = "";
            string uril = HasKeyProtect.DecryptionPass(OwinLibrary.GetUrlAPI());
            using (HttpClient httpClient = new HttpClient())
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uril);
                var login = new Dictionary<string, string>
                            {
                               {"grant_type", "password"},
                               {"UserName", "Csoz+BPpSiQx4ratHtk3ULZGgg97IiTqqjjyv0YBeZQ="},
                            };
                var response = client.PostAsync("Token", new FormUrlEncodedContent(login)).Result;
                if (response.IsSuccessStatusCode)
                {
                    resultJSON = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.AccessToken);
                    var param = new Dictionary<string, string>
                            {
                               {"type", type},
                            };
                    string cmdtextapi = "api/FDCM/GetPendingFileAHU";
                    var responsed = client.PostAsync(cmdtextapi, new FormUrlEncodedContent(param)).Result;
                    if (responsed.IsSuccessStatusCode)
                    {
                        resultJSON = responsed.Content.ReadAsStringAsync().Result;
                    }
                }
            }


            return Json(new
            {
                moderror = "",
                url = "https://fidusia.ahu.go.id/app/nextPageDaftar.php",
                view = CustomEngineView.RenderRazorViewToString(ControllerContext, "/Views/Fidusia/_uiGridAHURegis.cshtml", null),
                result = resultJSON
            });
        }


        public static byte[] ConvertHTMLToPDFObjectSaveToBYTE(byte[] filetxt)
        {

            var fileContents = Encoding.Default.GetString(filetxt);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(fileContents);

            var headers = doc.DocumentNode.SelectNodes("//tr/th");
            DataTable table = new DataTable();

            //cGetSertificateAHU dataAHU = new cGetSertificateAHU();
            //dataAHU = CollectAHU[0];

            string html = "";
            var i = 0;
            foreach (HtmlAgilityPack.HtmlNode header in headers)
            {
                if (i < 7)
                {
                    table.Columns.Add(header.InnerText); // create columns from th
                }
                i = i + 1;
            }
            // select rows with td elements 
            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
            {
                table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());
            }


            var headers1 = doc.DocumentNode.InnerText.ToLower().IndexOf("nomor sertifikat");
            string captionheader = doc.DocumentNode.InnerText.Substring(0, headers1);

            var headers2 = doc.DocumentNode.InnerText.ToLower().IndexOf("kategori");
            string captionheader1 = doc.DocumentNode.InnerText.Substring(headers1, headers2 - headers1);
            DataTable table1 = new DataTable();

            Byte[] res = null;
            //  string filename = "";
            using (MemoryStream ms = new MemoryStream())
            {
                PdfDocument documentpdf = new PdfDocument();

                double pageSize = 35;
                double pageSizeNext = 42;
                double totalpage = Math.Ceiling((table.Rows.Count / pageSize));
                int pageSizebefore = int.Parse(pageSize.ToString());
                double page = 0;
                double j = 0;
                // set detail laporan //
                for (var k = 0; k < totalpage; k++)
                {

                    if (k == 1)
                    {
                        pageSize = pageSizeNext;
                        totalpage = Math.Ceiling(double.Parse(table.Rows.Count.ToString()) / pageSize);

                    }

                    int pageSizeint = int.Parse(pageSize.ToString());
                    j = (k) * pageSizeint;
                    DataTable dtdata = table.AsEnumerable().Skip((((k + 1) - 1) * pageSizeint) - (pageSizeint - pageSizebefore)).Take(pageSizeint).CopyToDataTable();


                    string htmldetail = "";
                    string htmldetailGroup = "";
                    string caption = "";
                    int captionorder = 0;


                    foreach (DataRow rw in dtdata.Rows)
                    {

                        captionorder = captionorder + 1;

                        if (dtdata.Columns[1].ColumnName.ToLower() == "merk")
                        {

                            if (captionorder == 1)
                            {
                                html = "<Table style='border: 1px solid black;width:100%;box-sizing:border-box;'>";
                                caption = "Kategori Obyek";
                            }
                            else if (captionorder == 2)
                            {
                                caption = "Merek";
                            }
                            else if (captionorder == 3)
                            {
                                caption = "Tipe";
                            }
                            else if (captionorder == 4)
                            {
                                caption = "No. Rangka";
                            }
                            else if (captionorder == 5)
                            {
                                caption = "No. Mesin";
                            }
                            else if (captionorder == 6)
                            {
                                caption = "Bukti Obyek";
                            }
                            else if (captionorder == 7)
                            {
                                caption = "Nilai Obyek";

                            }

                            htmldetail = htmldetail + "<tr style='max-width: 300px;text-align:left;font-weight:bold;'><td>" + caption + "</td><td width='20px'>:</td><td>" + rw["Merk"].ToString() + "</td></tr>";

                            if (captionorder == 7)
                            {
                                htmldetailGroup = htmldetailGroup + html + htmldetail + "</table>";
                                htmldetail = "";
                                captionorder = 0;
                            }
                        }
                        else
                        if (dtdata.Columns[1].ColumnName.ToLower() == "keterangan")
                        {
                            if (captionorder == 1)
                            {
                                html = "<Table style='border: 1px solid black;width:100%;box-sizing:border-box;'>";
                                caption = "Kategori Obyek";
                            }
                            else if (captionorder == 2)
                            {
                                caption = "Keterangan";
                            }
                            else if (captionorder == 3)
                            {
                                caption = "Bukti Obyek";
                            }
                            else if (captionorder == 4)
                            {
                                caption = "Nilai Obyek";

                            }

                            htmldetail = htmldetail + "<tr style='max-width: 300px;text-align:left;font-weight:bold;'><td>" + caption + "</td><td width='20px'>:</td><td>" + rw["Keterangan"].ToString() + "</td></tr>";

                            if (captionorder == 4)
                            {
                                htmldetailGroup = htmldetailGroup + html + htmldetail + "</table>";
                                htmldetail = "";
                                captionorder = 0;
                            }
                        }
                    }

                    if (k == 0)
                    {
                        html = "<center><strong><h3>" + captionheader + "</h3><h5>" + captionheader1 + "</h5></strong></center>" + htmldetailGroup;

                    }
                    else
                    {
                        html = htmldetailGroup;
                    }

                    PdfDocument pdf =  PdfGenerator.GeneratePdf(html, new PdfGenerateConfig()
                    {
                        PageSize = PageSize.A4,
                        PageOrientation = PageOrientation.Portrait,
                        MarginBottom = 50,
                        MarginTop = 50,
                        MarginLeft = 25,
                        MarginRight = 25,

                    });

                    PdfDocument pdf1ForImportdetail = ImportPdfDocument(pdf);
                    documentpdf.Pages.Add(pdf1ForImportdetail.Pages[0]);
                    

                    page = page + 1;

                }

                //filename = dataAHU.ContractNo + '_' + dataAHU.CertificateNo + '_' + dataAHU.DebiturNAme + ".pdf";

                documentpdf.Save(ms);
                res = ms.ToArray();

                //dataAHU.FileName = filename;
                //dataAHU.FileByte = res;
                //dataAHU.ContentLength= res.Length;
                //dataAHU.ContentType = "application/pdf";
            }

            //CollectAHU.Add(dataAHU);
            return res;
        }

        public static PdfDocument ImportPdfDocument(PdfDocument pdf1)
        {
            using (var stream = new MemoryStream())
            {
                pdf1.Save(stream, false);
                stream.Position = 0;
                var result = PdfReader.Open(stream, PdfDocumentOpenMode.Import);
                return result;
            }
        }

    }
}
