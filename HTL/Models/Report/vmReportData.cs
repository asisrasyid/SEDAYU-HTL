using HashNetFramework;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DusColl
{
    [Serializable]
    public class vmReportData
    {
        public cFilterContract DetailFilter { get; set; }

        public async Task<Byte[]> dbDownloadLamporanDataHT(string template, string usertipe, string TitileReport, string SelectBranchDesc, string SelectRegion, string SelectDivisiDesc, string fromdate, string todate, string SelectStatusDesc, DataTable dt, String RequestAppPath = "")
        {
            await Task.Delay(0);
            XmlDocument xml = new XmlDocument();
            string xmlString = RequestAppPath + "External\\TemplateReport\\ReportDataHTL.xml";
            //if (usertipe == "FDCM")
            //{
            //    xmlString = RequestAppPath + "External\\TemplateReport\\ReportDataHTLHO.xml";
            //}
            if (template != "")
            {
                xmlString = RequestAppPath + "External\\TemplateReport\\" + template;
            }
            xml.Load(xmlString);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
            XmlElement root = xml.DocumentElement;

            DataTable dtlist = dt;
            XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

            foreach (DataRow rw in dtlist.Rows)
            {
                XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                xmlAttrRow.Value = "0";
                XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                xmlRecordNo.Attributes.Append(xmlAttrRow);
                foreach (DataColumn col in dtlist.Columns)
                {
                    XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                    XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                    string attrval = col.ColumnName.Split('_')[1].ToString();
                    xmlAttrNumber.Value = attrval;
                    XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                    xmlRecordNoceldata.InnerText = rw[col].ToString();
                    xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                    xmlRecordNo.AppendChild(xmlRecordNocel);
                    xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                }
                nodesing.AppendChild(xmlRecordNo);
            }

            byte[] bindata = Encoding.ASCII.GetBytes(xml.OuterXml);
            return bindata;
        }

        public async Task<Byte[]> dbDownloadLampiranINV(DataTable dt, String RequestAppPath = "")
        {
            await Task.Delay(0);
            XmlDocument xml = new XmlDocument();
            string xmlString = RequestAppPath + "External\\TemplateReport\\ReportDataHTLINV.xml";

            xml.Load(xmlString);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
            XmlElement root = xml.DocumentElement;

            DataTable dtlist = dt;
            XmlNode nodesing = root.SelectSingleNode("/*//ss:Table", nsmgr);

            foreach (DataRow rw in dtlist.Rows)
            {
                XmlAttribute xmlAttrRow = xml.CreateAttribute("ss", "AutoFitHeight", "urn:schemas-microsoft-com:office:spreadsheet");
                xmlAttrRow.Value = "0";
                XmlNode xmlRecordNo = xml.CreateNode(XmlNodeType.Element, "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                xmlRecordNo.Attributes.Append(xmlAttrRow);
                foreach (DataColumn col in dtlist.Columns)
                {
                    XmlNode xmlRecordNocel = xml.CreateNode(XmlNodeType.Element, "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                    XmlAttribute xmlAttrNumber = xml.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                    string attrval = col.ColumnName.Split('_')[1].ToString();
                    xmlAttrNumber.Value = attrval;
                    XmlNode xmlRecordNoceldata = xml.CreateNode(XmlNodeType.Element, "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                    xmlRecordNoceldata.InnerText = rw[col].ToString();
                    xmlRecordNoceldata.Attributes.Append(xmlAttrNumber);
                    xmlRecordNo.AppendChild(xmlRecordNocel);
                    xmlRecordNocel.AppendChild(xmlRecordNoceldata);
                }
                nodesing.AppendChild(xmlRecordNo);
            }

            byte[] bindata = Encoding.ASCII.GetBytes(xml.OuterXml);
            return bindata;
        }
    }
}