using CAB.Data;
using CAB.Data.Repo;
using CAB.Models;
using EFCore.BulkExtensions;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Data;
using System.Text.Json;

namespace CAB.Controllers
{
    public class ProcessDataController : Controller
    {
        private readonly ProcessDataRepo _ProcessDataRepo = null;
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment webHostEnvironment;
        private DateTime Start;
        private TimeSpan TimeSpan;
        public ProcessDataController(ProcessDataRepo ProcessDataRepo, IWebHostEnvironment _webHostEnvironment, ApplicationDbContext context)
        {
            _ProcessDataRepo = ProcessDataRepo;
            webHostEnvironment = _webHostEnvironment;
            _context = context;
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
        ///To Process the Excel Data{public ActionResult ProcessDataCnf(string filename, int Id)}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Process the Excel Data
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 21/09/2022
        ///Remarks :- 
        ///Reviewed by : M Sanal Kumar
        ///Reviewed Date :-- 20 Oct 22
        ///Tested By :- 
        ///Tested Date :-
        ///Start
        [HttpGet]
        public ActionResult ProcessDataCnf(string filename, int Id)
        {
            try
            {
                string msg1 = "";
                // string path = Path.Combine(this.webHostEnvironment.WebRootPath, "Image");
                var fileName = Path.Combine(this.webHostEnvironment.WebRootPath, "Excel" + "\\") + filename;
                FileStream stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                string ftype = Path.GetExtension(filename);
                IExcelDataReader excelReader;
                if (ftype == ".xls")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                stream.Close();
                List<CabData> Excelist = new List<CabData>();
                var dt = result.Tables[0];
                int count_insert = 0;
                int totalrec = dt.Rows.Count;
                foreach (DataRow data in dt.Rows)
                {
                    if (data[4].ToString() != "")
                    {
                        try
                        {

                            Excelist.Add(new CabData
                            {


                                //Complaint_Type = string.IsNullOrEmpty(data[0].ToString()) ? "" : data[0].ToString(),
                                ArmyNo = string.IsNullOrEmpty(data[0].ToString()) ? "" : data[0].ToString(),
                                Rank = string.IsNullOrEmpty(data[1].ToString()) ? "" : data[1].ToString(),
                                Name = string.IsNullOrEmpty(data[2].ToString()) ? "" : data[2].ToString(),
                                ComplaintNo = string.IsNullOrEmpty(data[3].ToString()) ? "" : data[3].ToString(),
                                // Subject = string.IsNullOrEmpty(data[4].ToString()) ? "" : data[4].ToString(),
                                Dt_Of_Complaint = string.IsNullOrEmpty(data[4].ToString()) ? new DateTime() : Convert.ToDateTime(data[4].ToString()),
                                Regt = string.IsNullOrEmpty(data[5].ToString()) ? "" : data[5].ToString(),

                                // Recd_Date_Cab = string.IsNullOrEmpty(data[5].ToString()) ? new DateTime() : Convert.ToDateTime(data[5].ToString()),
                                InfoDate = string.IsNullOrEmpty(data[6].ToString()) ? new DateTime() : Convert.ToDateTime(data[6].ToString()),
                                Case_Status = string.IsNullOrEmpty(data[7].ToString()) ? "" : data[7].ToString(),
                                //


                            }); ; ;
                            count_insert = count_insert + 1;
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }

                ExcelDataViewModel viewModel = new ExcelDataViewModel();

                //viewModel.ExcelDatas = Excelist;
                Start = DateTime.Now;
                //ExcelData pd = _context.ExcelData.FirstOrDefault(P =>P.ArmyNo == viewModel.ExcelDatas.);
                deleteall();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    //insert list data using BulkInsert
                    try
                    {

                        _context.BulkInsert(Excelist);


                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                deletefile(Id);
                TimeSpan = DateTime.Now - Start;
                string ss = "Total Record : " + totalrec + " :::::: Total Record Uploaded :" + count_insert;
                TempData["ss"] = ss;
                return RedirectToAction("uploadFile", "ImportData");
            }
            catch (Exception ex)
            {

                Comman.ExceptionHandle(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        #endregion

        #region
        ///To Process the Confirm Excel Data{public ActionResult ProcessDataCnfCreate(ExcelDataViewModel excelDataViewModel)}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Process the Confirm Excel Data
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
        /// </summary>
        /// <param name="excelDataViewModel"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult ProcessDataCnfCreate(ExcelDataViewModel excelDataViewModel)
        //{
        //    foreach (var v in excelDataViewModel.tbl_CabData)
        //    {
        //        _ProcessDataRepo.ProcessDataCnfCreate(v);
        //    }

        //    return View();
        //}
        #endregion

        ///Coder Name :- kapoor
        ///Purpose :-To Process the delete all rec
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 14/10/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start

        public void deleteall()
        {
            string name = string.Format("[{0}]", "tbl_CabData");
            string cmd = $"TRUNCATE TABLE {name}";
            _context.Database.ExecuteSqlRaw(cmd);
            // return cmd;
            //return cmd;
        }

        ///Coder Name :- kapoor
        ///Purpose :-To Process the delete Excel rec
        ///Authority & Reference :- 
        ///Kind Of Request :- 
        ///Version :- Ver 1.0.0
        ///Dated :- 14/10/2022
        ///Remarks :- 
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        ///Start
        public void deletefile(int Id)
        {

            _context.Database.ExecuteSqlRaw("DELETE FROM tbl_ImportDtls where Import_Id=" + Id + "");
        }

        ///Coder Name :- Sanal
        ///Purpose :-To Process check authorised ip for upload excel
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


    }


}

