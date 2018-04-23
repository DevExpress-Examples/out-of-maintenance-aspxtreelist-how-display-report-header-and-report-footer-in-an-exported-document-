Imports Microsoft.VisualBasic
Imports DevExpress.XtraPrinting
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Web

Namespace _ASPxTreeList
    Partial Public Class [Default]
        Inherits System.Web.UI.Page
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Protected Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim link As New PrintableComponentLink(New PrintingSystem())
            link.Component = Exporter
            Using ms As New MemoryStream()

                Dim height As Integer = 20
                AddHandler link.CreateReportHeaderArea, Function(s, args) AnonymousMethod1(s, args, height)
                AddHandler link.CreateReportFooterArea, Function(s, args) AnonymousMethod2(s, args, height)

                link.CreateDocument(False)
                link.ExportToPdf(ms)
                WriteDataToResponse(ms.ToArray(), ExportType.PDF)
            End Using
        End Sub

        Private Function AnonymousMethod1(ByVal s As Object, ByVal args As CreateAreaEventArgs, ByVal height As Integer) As Boolean
            args.Graph.DrawString("Report Header Text", Color.Empty, New RectangleF(Point.Empty, New SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Top)
            args.Graph.DrawRect(New RectangleF(New Point(0, height), New SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Top, Color.Empty, Color.Empty)
            Return True
        End Function

        Private Function AnonymousMethod2(ByVal s As Object, ByVal args As CreateAreaEventArgs, ByVal height As Integer) As Boolean
            args.Graph.DrawRect(New RectangleF(Point.Empty, New SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Bottom, Color.Empty, Color.Empty)
            args.Graph.DrawString("Report Footer Text", Color.Empty, New RectangleF(New Point(0, height), New SizeF(args.Graph.ClientPageSize.Width, height)), BorderSide.Bottom)
            Return True
        End Function

        Public Shared Sub WriteDataToResponse(ByVal data() As Byte, ByVal type As ExportType)
            If data IsNot Nothing AndAlso data.Length > 0 AndAlso type <> ExportType.none Then
                Dim fileEnding As String = String.Empty
                Dim fileContent As String = String.Empty
                Select Case type
                    Case ExportType.XLS
                        fileContent = "application/ms-excel"
                        fileEnding = "xls"
                    Case ExportType.PDF
                        fileContent = "application/pdf"
                        fileEnding = "pdf"
                    Case ExportType.CSV
                        fileContent = "text/plain"
                        fileEnding = "csv"
                    Case ExportType.RTF
                        fileContent = "text/enriched"
                        fileEnding = "rtf"
                End Select
                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.Buffer = False
                HttpContext.Current.Response.ClearHeaders()
                HttpContext.Current.Response.AppendHeader("Content-Type", fileContent)
                HttpContext.Current.Response.AppendHeader("Content-Transfer-Encoding", "binary")
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "inline; filename=Export." & fileEnding)
                HttpContext.Current.Response.BinaryWrite(data)
                HttpContext.Current.Response.End()
            End If
        End Sub
    End Class

    Public Enum ExportType
        none = 0
        XLS = 1
        PDF = 2
        CSV = 4
        RTF = 8
    End Enum
End Namespace