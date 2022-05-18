using ITPM.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ITPM.Report
{
    public class CoachDetailsReport
    {
        private IWebHostEnvironment _hostEnvironment;
        public CoachDetailsReport(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }




        #region Declaration
        int _maxColumn = 7;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(7);
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();



        List<CoachClass> _getcoachdetails = new List<CoachClass>();
        #endregion



        public byte[] Report(List<CoachClass> getcoachdetails)
        {
            _getcoachdetails = getcoachdetails;



            _document = new Document();
            _document.SetPageSize(PageSize.A3);
            _document.SetMargins(5f, 5f, 20f, 5f);



            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;



            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);



            PdfWriter docWrite = PdfWriter.GetInstance(_document, _memoryStream);



            _document.Open();



            float[] sizes = new float[_maxColumn];
            for (var i = 0; i < _maxColumn; i++)
            {
                if (i == 0) sizes[i] = 20;
                else sizes[i] = 100;
            }



            _pdfTable.SetWidths(sizes);



            //this.ReportHeader();
            this.EmptyRow(2);
            this.ReportBody();



            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);



            _document.Close();



            return _memoryStream.ToArray();
        }



        //private void ReportHeader()
        //{
        //    _pdfCell = new PdfPCell(this.AddLogo());
        //    _pdfCell.Colspan = 1;
        //    _pdfCell.Border = 0;
        //    _pdfTable.AddCell(_pdfCell);



        //    _pdfCell = new PdfPCell(this.SetPageTitle());
        //    _pdfCell.Colspan = _maxColumn - 1;
        //    _pdfCell.Border = 0;
        //    _pdfTable.AddCell(_pdfCell);



        //    _pdfTable.CompleteRow();
        //}



        //private PdfPTable AddLogo()
        //{
        //    int maxColumn = 1;
        //    PdfPTable pdfPTable = new PdfPTable(maxColumn);



        //    string path = _hostEnvironment.WebRootPath + "/Images/Delivery";



        //    string imgCombine = Path.Combine(path, "logo.png");
        //    Image img = Image.GetInstance(imgCombine);



        //    _pdfCell = new PdfPCell(img);
        //    _pdfCell.Colspan = maxColumn;
        //    _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    _pdfCell.Border = 0;
        //    _pdfCell.ExtraParagraphSpace = 0;
        //    pdfPTable.AddCell(_pdfCell);



        //    pdfPTable.CompleteRow();



        //    return pdfPTable;



        //}



        private PdfPTable SetPageTitle()
        {
            int maxColumn = 3;
            PdfPTable pdfPTable = new PdfPTable(maxColumn);



            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _pdfCell = new PdfPCell(new Phrase("Coach Details", _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();



            _fontStyle = FontFactory.GetFont("Tahoma", 14f, 1);
            _pdfCell = new PdfPCell(new Phrase("Report", _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();



            return pdfPTable;



        }



        private void EmptyRow(int nCount)
        {
            for (int i = 1; i <= nCount; i++)
            {
                _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
                _pdfCell.Colspan = _maxColumn;
                _pdfCell.Border = 0;
                _pdfCell.ExtraParagraphSpace = 10;
                _pdfTable.AddCell(_pdfCell);
                _pdfTable.CompleteRow();
            }
        }



        private void ReportBody()
        {
            var fontStyleBold = FontFactory.GetFont("Tahoma", 9f, 1);
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);



            #region Detail Table Header



            _pdfCell = new PdfPCell(new Phrase("CoachID ", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);



            //_pdfCell = new PdfPCell(new Phrase("OrderID", fontStyleBold));
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfCell.BackgroundColor = BaseColor.Gray;
            //_pdfTable.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("Cname", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("CAge", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("contactnumber", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("Email ", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);



            _pdfCell = new PdfPCell(new Phrase("Description", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Rate", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);



            //_pdfCell = new PdfPCell(new Phrase("OStatus", fontStyleBold));
            //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfCell.BackgroundColor = BaseColor.Gray;
            //_pdfTable.AddCell(_pdfCell);



            _pdfTable.CompleteRow();
            #endregion



            #region Detail table body



            foreach (var getcoachdetails in _getcoachdetails)
            {
                _pdfCell = new PdfPCell(new Phrase(getcoachdetails.CoachID.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);



                //_pdfCell = new PdfPCell(new Phrase(getcoachdetails.Order_ID.ToString(), _fontStyle));
                //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //_pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //_pdfCell.BackgroundColor = BaseColor.White;
                //_pdfTable.AddCell(_pdfCell);



                _pdfCell = new PdfPCell(new Phrase(getcoachdetails.Cname, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);



                _pdfCell = new PdfPCell(new Phrase(getcoachdetails.CAge.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);



                _pdfCell = new PdfPCell(new Phrase(getcoachdetails.contactnumber.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);



                _pdfCell = new PdfPCell(new Phrase(getcoachdetails.Email, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);



                _pdfCell = new PdfPCell(new Phrase(getcoachdetails.Description.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(getcoachdetails.Rate.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);



                //_pdfCell = new PdfPCell(new Phrase(getcoachdetails.OStatus, _fontStyle));
                //_pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //_pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //_pdfCell.BackgroundColor = BaseColor.White;
                //_pdfTable.AddCell(_pdfCell);




                _pdfTable.CompleteRow();
            }



            #endregion


        }
    }
}
