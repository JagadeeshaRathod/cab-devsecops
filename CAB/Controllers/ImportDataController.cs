using CAB.Data;
using CAB.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CAB.Controllers
{
    /*  [NonController] */   //asp.net Core mvc hide and exclude Web Api Controller Method
    public class ImportDataController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private IWebHostEnvironment webHostEnvironment;

        string ipaddress;
        public ImportDataController(/*UnitDtlsController UnitDtls,*/ IWebHostEnvironment _webHostEnvironment, ApplicationDbContext context/*, UnitDetailRepo UnitDetailRepo*/, IHttpContextAccessor httpContextAccessor)
        {
            webHostEnvironment = _webHostEnvironment;
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        #region
        ///To Get The Index View  {public IActionResult Index()}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Get The Index View 
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 21/09/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start

        public IActionResult Index()
        {
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            //Getauthip();
            //if (ip == ipaddress || ip == "::1")
            //{
                return View();
            //}
            //else
            //{
            //    Comman.ExceptionHandle("Unauthorise Access from : " + ip + DateTime.Today.ToString());
            //    return RedirectToAction("Error", "Home");
            //}
        }
        #endregion


        #region
        ///To Upload The Excel File{public async Task<IActionResult> uploadFile()}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Upload The Excel File
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 21/09/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start
        ///
        //[ApiExplorerSettings(IgnoreApi = true)]                 //asp.net Core mvc hide and exclude Web Api Controller Method
        //[NonAction]
        public async Task<IActionResult> uploadFile()
        {
            try
            {

                var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                 Getauthip();

                if (ip == ipaddress || ip == "::1")
                {
                    ViewBag.GetAll = _context.tbl_ImportDtls.ToList();

                    var ss = ViewBag.GetAll;

                    return View();
                }
                else
                {
                    Comman.ExceptionHandle("Unauthorised Access detected :  " + ip + "   " + DateTime.Now.ToString());
                    return RedirectToAction("Error", "Home");

                }

            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        ///Validation of IP address 
        ///Coder Name :- M Sanal Kumar
        ///Purpose :-Secure upload method
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 27/10/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start

        public void Getauthip()
        {
            UploadBinding uplbind = new UploadBinding();

            ipaddress = _context.tbl_UploadBinding.Select(p => p.Ipaddress).FirstOrDefault();


        }


        #region
        ///To Upload The Excel File{public async Task<IActionResult> uploadFile()}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Upload The Excel File
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 21/09/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start
        //[ApiExplorerSettings(IgnoreApi = true)]                                        //asp.net Core mvc hide and exclude Web Api Controller Method
        //[NonAction]



        [HttpPost]
        public async Task<IActionResult> uploadFile(CreateUpload_dtl upload)
        {
            try
            {

                if (upload == null)
                {
                    return BadRequest();
                }
                string wwwPath = this.webHostEnvironment.WebRootPath;
                string contentPath = this.webHostEnvironment.ContentRootPath;
                string path = Path.Combine(this.webHostEnvironment.WebRootPath, "Excel");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                List<string> uploadedFiles = new List<string>();
                string fileName = Path.GetFileName(upload.postedFiles.FileName);
                string filepath = Path.Combine(path, fileName);
                IExcelDataReader excelReader;

                using (FileStream streams = new FileStream(Path.Combine(path, Path.GetFileName(upload.postedFiles.FileName)), FileMode.Create))
                {
                    upload.postedFiles.CopyTo(streams);
                    uploadedFiles.Add(fileName);
                    streams.Close();

                }
                var uloadDtl = new Upload_dtl()
                {
                    uploaddt = DateTime.Now,
                    FileName = fileName,
                    //Status = "Pending For Processing",
                    UploadedBy = "ASDC",
                };

                string ftype = Path.GetExtension(upload.postedFiles.FileName);
                FileStream stream = System.IO.File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                if (ftype == ".xls")
                {

                    try
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    catch (Exception ex)
                    {
                        string sss = ex.Message;
                        if (sss == "'Invalid file signature") ;
                        stream.Close();
                        System.IO.File.Delete(filepath);
                        TempData["message"] = "Invlid File!";
                        throw;
                    }


                    TempData["message"] = "The Excel is Uploaded Successfully!";
                    _context.tbl_ImportDtls.Add(uloadDtl);
                    await _context.SaveChangesAsync();
                }
                else if (ftype == ".xlsx")
                {
                    try
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    catch (Exception ex)
                    {
                        string sss = ex.Message;
                        if (sss == "'Invalid file signature") ;
                        stream.Close();
                        System.IO.File.Delete(filepath);
                        TempData["message"] = "Invlid File!";
                        throw;
                    }


                    TempData["message"] = "The Excel is Uploaded Successfully!";
                    _context.tbl_ImportDtls.Add(uloadDtl);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    TempData["message"] = "This file format is not supported !";
                }
                return RedirectToAction(nameof(uploadFile));
            }
            catch (Exception ex)
            {
                if (TempData["message"].ToString().Length < 1)
                {
                    TempData["message"] = ex.Message;
                }

                Comman.ExceptionHandle(TempData["message"].ToString());
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion



        #region
        ///To Delete the Record  { public async Task<IActionResult> Delete(int? id)}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Upload The Excel File
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 21/09/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start
        [HttpPost]

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var Upload_dtl = await _context.tbl_ImportDtls.FindAsync(id);
                _context.tbl_ImportDtls.Remove(Upload_dtl);
                await _context.SaveChangesAsync();
                return RedirectToAction("uploadFile");
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion
    }
}
