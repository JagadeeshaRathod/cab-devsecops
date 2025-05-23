using CAB.Models;

namespace CAB.Data.Repo
{
    public class ProcessDataRepo
    {

        private readonly ApplicationDbContext _context = null, _context1 = null;

        public ProcessDataRepo(ApplicationDbContext context, ApplicationDbContext context1)
        {
            _context = context;
            _context1 = context1;
        }

        #region
        ///To Get The Process Data Cnf Create  {public void ProcessDataCnfCreate(ExcelData model)}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Get The Process Data Cnf Create
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

        //public void ProcessDataCnfCreate(CabData model)
        //{
        //    var newProcessData = new Cab_model()
        //    {
        //        ArmyNo = model.ArmyNo,
        //        Name = model.Name,
        //        Complaint_Type = model.Complaint_Type,
        //        Rank = model.Rank,
        //        Subject = model.Subject,
        //        Regt = model.Regt,
        //        Case_Status = model.Case_Status,
        //        Dt_Of_Complaint = model.Dt_Of_Complaint ,       
        //        Recd_Date_Cab = model.Recd_Date_Cab,
        //        ComplaintNo = model.ComplaintNo,

        //        //Checked = true,

        //    };




        //    Cab_model pd = _context1.ProcessData.FirstOrDefault(P => P.ArmyNo == newProcessData.ArmyNo);

        //    if (pd != null)
        //    {
        //        _context.SaveChanges();
        //    }
        //    else
        //    {
        //        _context.ProcessData.Add(newProcessData);
        //    }
        //    _context.SaveChanges();

       

        //}
        ///end
        #endregion


        #region
        ///To Get All {public List<ProcessData> GetAll()}
        ///Coder Name :- Ajay Singh
        ///Purpose :-To Get ALL
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
        //public List<Cab_model> GetAll()
        //{
        //    var Temp = _context.tbl_CabData.ToList();
        //    return Temp;
        //}
        ///end
        #endregion

    }
}
