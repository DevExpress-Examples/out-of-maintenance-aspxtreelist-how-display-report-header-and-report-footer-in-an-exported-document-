using DevExpress.XtraPrinting;
using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace _ASPxTreeList {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void Button_Click(object sender, EventArgs e) {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = Exporter;
            using (MemoryStream ms = new MemoryStream()) {

                int height = 20;
                link.CreateReportHeaderArea += (s, args) => {
                    args.Graph.DrawString("Report Header Text", Color.Empty, new RectangleF(Point.Empty, new SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Top);
                    args.Graph.DrawRect(new RectangleF(new Point(0, height), new SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Top, Color.Empty, Color.Empty);
                };

                link.CreateReportFooterArea += (s, args) => {
                    args.Graph.DrawRect(new RectangleF(Point.Empty, new SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Bottom, Color.Empty, Color.Empty);
                    args.Graph.DrawString("Report Footer Text", Color.Empty, new RectangleF(new Point(0, height), new SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Bottom);
                };

                link.CreateDocument(false);
                link.ExportToPdf(ms);
                WriteDataToResponse(ms.ToArray(), ExportType.PDF);
            }
        }

        public static void WriteDataToResponse(byte[] data, ExportType type) {
            if (data != null && data.Length > 0 && type != ExportType.none) {
                String fileEnding = String.Empty;
                String fileContent = String.Empty;
                switch (type) {
                    case ExportType.XLS:
                        fileContent = "application/ms-excel";
                        fileEnding = "xls";
                        break;
                    case ExportType.PDF:
                        fileContent = "application/pdf";
                        fileEnding = "pdf";
                        break;
                    case ExportType.CSV:
                        fileContent = "text/plain";
                        fileEnding = "csv";
                        break;
                    case ExportType.RTF:
                        fileContent = "text/enriched";
                        fileEnding = "rtf";
                        break;
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AppendHeader("Content-Type", fileContent);
                HttpContext.Current.Response.AppendHeader("Content-Transfer-Encoding", "binary");
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "inline; filename=Export." + fileEnding);
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.End();
            }
        }
    }

    public enum ExportType {
        none = 0,
        XLS = 1,
        PDF = 2,
        CSV = 4,
        RTF = 8
    }
}