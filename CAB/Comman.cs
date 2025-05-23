namespace CAB
{
    public class Comman
    {



        //Exception ex = ...

        public static int ExceptionHandle(string message)
        {
            string filePath = @"\Error.txt";
            string docPath = "wwwroot/Error";
         Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var _count = File.ReadAllText(docPath + "/Error.txt");
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Error.txt")))
            {
               
                outputFile.WriteLine(_count);
                outputFile.WriteLine("-----------------------------------------------------------------------------");
                outputFile.WriteLine("Date : " + DateTime.Now.ToString());
                
                outputFile.WriteLine(message);
            }
            return 0;

        }
    }
}
