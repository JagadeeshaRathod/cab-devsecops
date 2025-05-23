using CAB.Data;
using CAB.Models;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Parser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Timers;
using Color = iText.Kernel.Colors.Color;
using Document = iText.Layout.Document;
using Paragraph = iText.Layout.Element.Paragraph;
using Path = System.IO.Path;
using Rectangle = iText.Kernel.Geom.Rectangle;

namespace CAB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment webHostEnvironment;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private System.Timers.Timer aTimer;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment _webHostEnvironment, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = _webHostEnvironment;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        #region
        ///To Get The Index View  {public IActionResult Index()}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Get The Index View 
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 20/09/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start

        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region
        ///To Get The Search DATA{public IActionResult SearchRecord(string ArmyNo, string Date)}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Get The Search DATA
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 20/09/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start
        [HttpPost]
        public IActionResult SearchRecord(string ArmyNo, string Date)
        {
            try
            {
                var data = _context.tbl_CabData.Where(c => c.ArmyNo == ArmyNo && c.Dt_Of_Complaint == Convert.ToDateTime(Date)).GroupBy(c => c.ArmyNo).Select(x => x.First()).ToList();
                //    var data = _context.tbl_CabData.Where(c => c.ArmyNo == "7534").GroupBy(c => c.ArmyNo).Select(x => x.First()).ToList();

                return View(data);
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region
        ///To Get  last inserted 20 records {public IActionResult Search()}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Get  last inserted 20 records
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 20/09/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start
        public async Task<IActionResult> Search()
        {
            try
            {
                //var data = (from p in _context.tbl_CabData
                //            orderby p.Dt_Of_Complaint descending
                //            select p).Take(20);

                //var data = await _context.tbl_CabData.ToListAsync();
                var data = _context.tbl_CabData.GroupBy(c => c.ArmyNo).Select(x => x.First()).Take(100).ToList();

                return View(data);
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Polices()
        {

            try
            {
                string path = System.IO.Path.Combine(this.webHostEnvironment.WebRootPath, "Pdf");
                List<string> Courses = new List<string>();
                int count = 1;



                foreach (string file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                )
                {

                    // do something
                    Courses.Add(file.Replace(path + "\\", ""));

                    // generate1(file.Replace(path + "\\", "").ToString(),"");
                }
                ViewBag.Courses = Courses;
                return View();
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult ContactUs()
        {

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public void generate(string Path)
        //{
        //    try { 
        //    var filePath1 = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/Pdf/sample - Copy.pdf");
        //    PdfDocument pdf = new PdfDocument(new PdfWriter(filePath1));
        //    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new MyEventHandler());
        //    Document document = new Document(pdf);
        //    Paragraph p = new Paragraph("List of reported UFO sightings in 20th century");

        //    document.Add(p);

        //    document.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        Comman.ExceptionHandle(ex.Message);

        //    }
        //}
        string filepathpdf = "";
        public IActionResult WaterMark2(string id)
        {
            //var stream = new FileStream(@"path\to\file", FileMode.Open);
            //return new FileStreamResult(stream, "application/pdf");
            try
            {
                var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                var filePath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/Pdf/" + id + "");

                filepathpdf = generate2(filePath, ip);

                aTimer = new System.Timers.Timer(60000);
                // Hook up the Elapsed event for the timer.
                aTimer.Elapsed += OnTimer;

                aTimer.Enabled = true;
                return Redirect("../../Download/" + filepathpdf + ".pdf");
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return Json(0);
            }
        }
        public void OnTimer(Object source, ElapsedEventArgs e)
        {

            try
            {
                var filePath1 = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/Download/" + filepathpdf + ".pdf");

                if (System.IO.File.Exists(filePath1))
                {
                    // If file found, delete it    

                    System.IO.File.Delete(filePath1);


                }
            }
            catch (Exception ex)
            {
                Comman.ExceptionHandle(ex.Message);
            }
        }
        /// <summary>
        /// for jquery reutn file
        /// </summary>
        /// <param name="Courses"></param>
        /// <returns></returns>
        //public JsonResult WaterMark(string Courses)
        //{
        //    try { 
        //    var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        //    var filePath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/Pdf/"+ Courses + "");

        //        return Json(generate2(filePath, ip));
        //    }
        //    catch (Exception ex)
        //    {

        //        Comman.ExceptionHandle(ex.Message);
        //        return Json(0);
        //    }
        //}
        public string generate1(string Path, string ip)
        {
            try
            {
                //PdfFont font6 = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);
                //  var filePath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/Pdf/sample - Copy - Copy.pdf");
                Random rnd = new Random();
                string Dfilename = rnd.Next(1, 1000).ToString();
                var filePath1 = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/Download/" + Dfilename + ".pdf");
                PdfDocument pdfDoc =
                    new PdfDocument(new PdfReader(Path), new PdfWriter(filePath1));
                int margin = 72;

                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    PdfPage page = pdfDoc.GetPage(i);
                    // change page size
                    iText.Kernel.Geom.Rectangle mediaBox = page.GetMediaBox();
                    iText.Kernel.Geom.Rectangle newMediaBox = new iText.Kernel.Geom.Rectangle(
                          mediaBox.GetLeft() - margin, mediaBox.GetBottom() - margin,
                mediaBox.GetWidth() + margin * 2, mediaBox.GetHeight() + margin * 2);
                    page.SetMediaBox(newMediaBox);


                    PdfCanvas canvas1 = new PdfCanvas(pdfDoc.GetPage(i));
                    //        canvas.ShowTextAligned(new iText.Layout.Element.Paragraph("CONFIDENTIAL"),
                    //298, 421, pdfDoc.GetPageNumber(page),
                    //TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);

                    canvas1.BeginText().SetFontAndSize(
                    PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA), 45)
                    // .MoveText(208, 421)

                    .MoveText(mediaBox.GetWidth() / 2 - 150, mediaBox.GetHeight() / 2 - 10).SetFillColor(ColorConstants.LIGHT_GRAY)
                    //.SetHorizontalScaling(500)
                    .ShowText(ip)

                    .EndText();

                    //196, 189, 188
                }
                pdfDoc.Close();
                return Dfilename;
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return "";
            }
        }
        public string generate2(string Path, string ip)
        {
            try
            {
                Random rnd = new Random();
                string Dfilename = rnd.Next(1, 1000).ToString();
                var filePath1 = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/Download/" + Dfilename + ".pdf");
                PdfDocument pdfDoc = new PdfDocument(new PdfReader(Path), new PdfWriter(filePath1));
                Document doc = new Document(pdfDoc);
                PdfFont font = PdfFontFactory.CreateFont(FontProgramFactory.CreateFont(StandardFonts.HELVETICA));
                Paragraph paragraph = new Paragraph(ip + " " + DateTime.Now).SetFont(font).SetFontSize(30);

                PdfExtGState gs1 = new PdfExtGState().SetFillOpacity(0.2f);
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    PdfPage pdfPage = pdfDoc.GetPage(i);
                    Rectangle pageSize = pdfPage.GetPageSize();
                    float x = (pageSize.GetLeft() + pageSize.GetRight()) / 2;
                    float y = (pageSize.GetTop() + pageSize.GetBottom()) / 2;
                    PdfCanvas over = new PdfCanvas(pdfPage);
                    over.SaveState();
                    over.SetExtGState(gs1);

                    doc.ShowTextAligned(paragraph, 297, 450, i, TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);

                    over.RestoreState();
                }

                doc.Close();
                return Dfilename;
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return "";
            }
        }


        //Hit Counter 

        [HttpGet]
        public IActionResult GetHitCounterToday()
        {
            Counter counter = new Counter();
            string currentDt = DateTime.Now.Date.ToString("dd MMM yyyy");
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TodayHitCounter.txt");

            try
            {
                string fileContent = System.IO.File.ReadAllText(filePath);
                string[] lines = fileContent.Split(Environment.NewLine);

                if (lines.Length >= 2)
                {
                    string date = lines[0].Trim();
                    string totalHitString = lines[1].Trim();

                    if (date == currentDt)
                    {
                        if (int.TryParse(totalHitString, out int fileContents))
                        {
                            fileContents++;
                            System.IO.File.WriteAllText(filePath, $"{date}{Environment.NewLine}{fileContents}");
                            counter.SubmitBy = fileContents.ToString();
                        }
                    }
                    else
                    {
                        string newDate = currentDt;
                        int freshHitCounter = 1;

                        System.IO.File.WriteAllText(filePath, $"{newDate}{Environment.NewLine}{freshHitCounter}");
                        counter.SubmitBy = freshHitCounter.ToString();
                    }
                }
                else
                {
                    // Create the file if it doesn't exist
                    System.IO.File.WriteAllText(filePath, $"{currentDt}{Environment.NewLine}1");
                    counter.SubmitBy = "1";
                }

                return Ok(counter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetHitCounterMonthly()
        {
            Counter counter = new Counter();
            string currentMonth = DateTime.Now.Date.ToString("MMM");
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MonthlyHitCounter.txt");

            try
            {
                string fileContent = System.IO.File.ReadAllText(filePath);
                string[] lines = fileContent.Split(Environment.NewLine);

                if (lines.Length >= 2)
                {
                    string month = lines[0].Trim();
                    string totalHitString = lines[1].Trim();

                    if (month == currentMonth)
                    {
                        if (int.TryParse(totalHitString, out int fileContents))
                        {
                            fileContents++;
                            System.IO.File.WriteAllText(filePath, $"{month}{Environment.NewLine}{fileContents}");
                            counter.SubmitIp = $"{currentMonth} : {fileContents}";
                        }
                    }
                    else
                    {
                        string newMonth = currentMonth;
                        int freshHitCounter = 1;

                        System.IO.File.WriteAllText(filePath, $"{newMonth}{Environment.NewLine}{freshHitCounter}");
                        counter.SubmitIp = freshHitCounter.ToString();
                    }
                }
                else
                {
                    // Create the file if it doesn't exist
                    System.IO.File.WriteAllText(filePath, $"{currentMonth}{Environment.NewLine}1");
                    counter.SubmitIp = "1";
                }

                return Ok(counter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetHitCounter()
        {
            Counter counter = new Counter();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TotalHitCounter.txt");

            try
            {
                string fileContent = System.IO.File.ReadAllText(filePath);

                if (int.TryParse(fileContent, out int fileContents))
                {
                    fileContents++;
                    System.IO.File.WriteAllText(filePath, fileContents.ToString());
                    counter.SanctionIP = fileContents.ToString();

                    return Ok(counter);
                }
                else
                {
                    // Handle the case where the file content is not a valid integer
                    return BadRequest("Invalid file content. Unable to parse as an integer.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllHitCounterss()
        {
            IActionResult todayResult = GetHitCounterToday();
            IActionResult monthlyResult = GetHitCounterMonthly();
            IActionResult totalResult = GetHitCounter();
            if (todayResult is OkObjectResult todayOk && monthlyResult is OkObjectResult monthlyOk && totalResult is OkObjectResult totalOk)
            {
                Counter todayCounter = todayOk.Value as Counter;
                Counter monthlyCounter = monthlyOk.Value as Counter;
                Counter totalCounter = totalOk.Value as Counter;

                // Construct the response object with desired format
                var response = new[]
                    { new Dictionary<string, int>
            {
            { "Today", int.Parse(todayCounter.SubmitBy ?? "0") },
            { "CurrentMonth", int.Parse(monthlyCounter.SubmitIp?.Split(':')[1]?.Trim() ?? "0") },
            { "Total", int.Parse(totalCounter.SanctionIP ?? "0") }
            } };

                return Ok(response);
            }

            else
            {
                // Handle error cases where one or more counters failed to retrieve data
                return BadRequest("Failed to retrieve one or more hit counters.");
            }
        }

    }
}