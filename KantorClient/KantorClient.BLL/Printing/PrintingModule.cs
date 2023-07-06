using KantorClient.BLL.Models;
using PdfSharp;
using PdfSharp.Pdf;
using System.Data;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Excel = Microsoft.Office.Interop.Excel;

namespace KantorClient.BLL.Printing
{
    public static class PrintingModule
    {
        public static void ExportToExcel(List<TransactionReportModel> models, string path)
        {
            var dataTable = new DataTable("Transakcje");
            dataTable.Columns.Add("Typ Transakcji");
            dataTable.Columns.Add("Waluta");
            dataTable.Columns.Add("Kurs wymiany");
            dataTable.Columns.Add("Ilość");
            dataTable.Columns.Add("Kwota wymiany");
            dataTable.Columns.Add("Data transakcji");
            dataTable.Columns.Add("Kantor");
            dataTable.Columns.Add("Kasjer");
            dataTable.Columns.Add("Edytowany?");


            foreach (var model in models)
            {
                dataTable.Rows.Add(model.TransactionType == Model.Consts.TransactionType.Sell ? "SPRZEDAŻ" : "KUPNO", model.Currency.Symbol, model.Rate,
                                   model.Quantity, model.FinalValue, model.TransactionDate,
                                   model.KantorName, model.UserName, model.Parent == null ? "Tak" : "Nie");
            }

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
            Excel._Worksheet xlWorksheet = excelWorkBook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //Add a new worksheet to workbook with the Datatable name  
            Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
            excelWorkSheet.Name = dataTable.TableName;

            // add all the columns  
            for (int i = 1; i < dataTable.Columns.Count + 1; i++)
            {
                excelWorkSheet.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
            }

            // add all the rows  
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                for (int k = 0; k < dataTable.Columns.Count; k++)
                {
                    excelWorkSheet.Cells[j + 2, k + 1] = dataTable.Rows[j].ItemArray[k].ToString();
                }
            }

            try
            {
                excelWorkBook.SaveAs(path);
            }
            catch { }
            excelApp.Visible = true;
            //excelWorkBook.Close();
            //excelApp.Quit();
        }

        //public static PdfDocument PrintTransactions(List<TransactionReportModel> models)
        //{
        //    var pdfDocument = new PdfDocument();

        //    //margin
        //    PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
        //    PdfMargins margin = new PdfMargins();
        //    margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
        //    margin.Bottom = margin.Top;
        //    margin.Left = unitCvtr.ConvertUnits(3.17f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
        //    margin.Right = margin.Left;

        //    // Create one page
        //    PdfPageBase page = pdfDocument.Pages.Add(PdfPageSize.A4, margin);

        //    float y = 10;

        //    //title
        //    PdfBrush brush1 = PdfBrushes.Black;
        //    PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
        //    PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
        //    page.Canvas.DrawString("Part List", font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);
        //    y = y + font1.MeasureString("Part List", format1).Height;
        //    y = y + 5;

        //    //create data table
        //    PdfTable table = new PdfTable();
        //    table.Style.CellPadding = 1;
        //    table.Style.BorderPen = new PdfPen(brush1, 0.75f);
        //    table.Style.DefaultStyle.BackgroundBrush = PdfBrushes.SkyBlue;
        //    table.Style.DefaultStyle.Font = new PdfTrueTypeFont(new Font("Arial", 10f), true);
        //    table.Style.HeaderSource = PdfHeaderSource.ColumnCaptions;
        //    table.Style.HeaderStyle.BackgroundBrush = PdfBrushes.CadetBlue;
        //    table.Style.HeaderStyle.Font = new PdfTrueTypeFont(new Font("Arial", 11f, FontStyle.Bold), true);
        //    table.Style.HeaderStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
        //    table.Style.ShowHeader = true;
        //    table.Style.RepeatHeader = true;

        //    table.DataSourceType = PdfTableDataSourceType.TableDirect;
        //    var dataTable = new DataTable();
        //    dataTable.Columns.Add("Typ Transakcji");
        //    dataTable.Columns.Add("Waluta");
        //    dataTable.Columns.Add("Kurs wymiany");
        //    dataTable.Columns.Add("Ilość");
        //    dataTable.Columns.Add("Kwota wymiany");
        //    dataTable.Columns.Add("Data transakcji");
        //    dataTable.Columns.Add("Kantor");
        //    dataTable.Columns.Add("Kasjer");
        //    dataTable.Columns.Add("Edytowany?");


        //    foreach(var model in models)
        //    {
        //        dataTable.Rows.Add(model.TransactionType, model.Currency.Symbol, model.Rate,
        //                           model.Quantity, model.FinalValue, model.TransactionDate,
        //                           model.KantorName, model.UserName, model.Edited == true ? "Tak" : "Nie");
        //    }
        //    table.DataSource = dataTable;
        //    float width
        //        = page.Canvas.ClientSize.Width
        //            - (table.Columns.Count + 1) * table.Style.BorderPen.Width;

        //    for (int i = 0; i < table.Columns.Count; i++)
        //    {
        //        if (i == 1)
        //        {
        //            table.Columns[i].Width = width * 0.4f * width;
        //            table.Columns[i].StringFormat
        //                = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
        //        }
        //        else
        //        {
        //            table.Columns[i].Width = width * 0.12f * width;
        //            table.Columns[i].StringFormat
        //                = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
        //        }
        //    }

        //    table.BeginRowLayout += new BeginRowLayoutEventHandler(table_BeginRowLayout);

        //    PdfTableLayoutFormat tableLayout = new PdfTableLayoutFormat();
        //    tableLayout.Break = PdfLayoutBreakType.FitElement;
        //    tableLayout.Layout = PdfLayoutType.Paginate;
        //    PdfLayoutResult result = table.Draw(page, new PointF(0, y), tableLayout);
        //    y = result.Bounds.Bottom + 5;

        //    PdfBrush brush2 = PdfBrushes.Gray;
        //    PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 9f));
        //    result.Page.Canvas.DrawString(String.Format("* All {0} parts in the list", table.Rows.Count),
        //        font2, brush2, 5, y);

        //    return pdfDocument;
        //}

        public static void PrintTest()
        {

            //var html = @"
            //<h1>HI..! Welcome to the PDF Tutorial!</h1>
            //<p> This is 1st Page </p>
            //<div style = 'page-break-after: always;' ></div>
            //<h2> This is 2nd Page after page break!</h2>
            //<div style = 'page-break-after: always;' ></div>
            //<p> This is 3rd Page</p>
            //<div style = 'page-break-after: always;' ></div>
            //<link href=""https://fonts.googleapis.com/css?family=Libre Barcode 128""rel = ""stylesheet"" ><p style = ""font-family: 'Libre Barcode 128', serif; font-size:30px;""> Hello Google Fonts</p>";

            //PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
            //pdf.Save("document.pdf");
        }
        //static void table_BeginRowLayout(object sender, BeginRowLayoutEventArgs args)
        //{
        //    if (args.RowIndex < 0)
        //    {
        //        //header
        //        return;
        //    }
        //    if (args.RowIndex % 3 == 0)
        //    {
        //        args.CellStyle.BackgroundBrush = PdfBrushes.LightYellow;
        //    }
        //    else
        //    {
        //        args.CellStyle.BackgroundBrush = PdfBrushes.SkyBlue;
        //    }
        //}
    }
}
