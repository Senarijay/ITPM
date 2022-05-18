using iTextSharp.text;
using iTextSharp.text.pdf;
using ITPM.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITPM.Report
{
    public class FeedReport
    {
        private IWebHostEnvironment _hostEnvironment;
        public FeedReport(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        #region Declaration
        int _maxcol = 8;
        Document _document;
        Font _fontstyle;
        PdfPCell _pdfcell;
        PdfPTable _pdfPTable = new PdfPTable(8);
        MemoryStream _memoryStream = new MemoryStream();

        List<FeedbackClass> _ob = new List<FeedbackClass>();


        #endregion

        public byte[] Report(List<FeedbackClass> ob)
        {
            _ob = ob;
            _document = new Document();
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(5f, 5f, 20f, 5f);
            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontstyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter docwrite = PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            float[] sizes = new float[_maxcol];

            for (var i = 0; i < _maxcol; i++)
            {

                if (i == 0) sizes[i] = 20;
                else sizes[i] = 100;
            }

            _pdfPTable.SetWidths(sizes);
            //this.ReportHeader();
            this.Emptyrow(2);
            this.ReportBody();
            _pdfPTable.HeaderRows = 2;
            _document.Add(_pdfPTable);
            _document.Close();

            return _memoryStream.ToArray();

        }

        private void ReportBody()
        {
            var fontstylebold = FontFactory.GetFont("Tahoma", 9f, 1);
            _fontstyle = FontFactory.GetFont("Tahoma", 9f, 0);

            #region Detail Table Header
            _pdfcell = new PdfPCell(new Phrase("FeedID", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfcell = new PdfPCell(new Phrase("First Name", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfcell = new PdfPCell(new Phrase("Last Name", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfcell = new PdfPCell(new Phrase("Feedback Type", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfcell = new PdfPCell(new Phrase("Feedback Comment", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfcell = new PdfPCell(new Phrase("St_01", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfcell = new PdfPCell(new Phrase("St_02", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfcell = new PdfPCell(new Phrase("Rate", fontstylebold));
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfcell.BackgroundColor = BaseColor.Gray;
            _pdfPTable.AddCell(_pdfcell);

            _pdfPTable.CompleteRow();
            #endregion

            #region Detail table body

            foreach (var fed in _ob)
            {

                _pdfcell = new PdfPCell(new Phrase(fed.FeedID.ToString(), _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfcell = new PdfPCell(new Phrase(fed.FName, _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfcell = new PdfPCell(new Phrase(fed.LName, _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfcell = new PdfPCell(new Phrase(fed.FeedType, _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfcell = new PdfPCell(new Phrase(fed.FeedDes, _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfcell = new PdfPCell(new Phrase(fed.St_01.ToString(), _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfcell = new PdfPCell(new Phrase(fed.St_02.ToString(), _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfcell = new PdfPCell(new Phrase(fed.Rate.ToString(), _fontstyle));
                _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfcell.BackgroundColor = BaseColor.White;
                _pdfPTable.AddCell(_pdfcell);

                _pdfPTable.CompleteRow();

            }

            #endregion
        }

        private void Emptyrow(int ncount)
        {
            for (int i = 1; i <= ncount; i++)
            {
                _pdfcell = new PdfPCell(new Phrase("", _fontstyle));
                _pdfcell.Colspan = _maxcol;
                _pdfcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfcell.Border = 0;
                _pdfcell.ExtraParagraphSpace = 10;
                _pdfPTable.AddCell(_pdfcell);
                _pdfPTable.CompleteRow();
            }
        }

        //private void ReportHeader()
        //{
        //    _pdfcell = new PdfPCell(this.Addlogo());
        //    _pdfcell.Colspan = 1;
        //    _pdfcell.Border = 0;
        //    _pdfPTable.AddCell(_pdfcell);

        //    _pdfcell = new PdfPCell(this.setpagetitle());
        //    _pdfcell.Colspan = _maxcol - 1;
        //    _pdfcell.Border = 0;
        //    _pdfPTable.AddCell(_pdfcell);

        //    _pdfPTable.CompleteRow();


        //}

        private PdfPTable setpagetitle()
        {
            int maxcol = 3;
            PdfPTable pdfPTable = new PdfPTable(maxcol);
            _fontstyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _pdfcell = new PdfPCell(new Phrase("Report", _fontstyle));
            _pdfcell.Colspan = maxcol;
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.Border = 0;
            _pdfcell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfcell);
            pdfPTable.CompleteRow();

            _fontstyle = FontFactory.GetFont("Tahoma", 14f, 1);
            _pdfcell = new PdfPCell(new Phrase("Feedback Report", _fontstyle));
            _pdfcell.Colspan = maxcol;
            _pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfcell.Border = 0;
            _pdfcell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfcell);
            pdfPTable.CompleteRow();

            return pdfPTable;

        }

        //private PdfPTable Addlogo()
        //{
        //    int maxcolumn = 1;
        //    PdfPTable pdfTable = new PdfPTable(maxcolumn);
        //    string path = _hostEnvironment.WebRootPath + "/Images/Customer";
        //    string imgcombine = Path.Combine(path, "logo.png");

        //    Image img = Image.GetInstance(imgcombine);
        //    _pdfcell = new PdfPCell(img);
        //    _pdfcell.Colspan = maxcolumn;
        //    _pdfcell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    _pdfcell.Border = 0;
        //    _pdfcell.ExtraParagraphSpace = 0;
        //    pdfTable.AddCell(_pdfcell);

        //    pdfTable.CompleteRow();
        //    return pdfTable;

        //}
    }
}
