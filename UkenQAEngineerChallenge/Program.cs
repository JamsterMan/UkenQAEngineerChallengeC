using System;
using System.Collections.Generic;
using System.IO;

namespace UkenQAEngineerChallenge
{
    class Program
    {
        private List<FileNumber> fileNumbers = new List<FileNumber>();

        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.FileNumberCounter();
        }
        
        private void FileNumberCounter()
        {
            ReadFile();
        }


        private void ReadFile()
        {
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path+@"\src", "*.txt");

            foreach (var file in files)
            {
                using (StreamReader sr = File.OpenText(file))
                {
                    fileNumbers = new List<FileNumber>();
                    FileNumber fileNum;
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        try
                        {
                            int num = int.Parse(s);
                            if(fileNumbers.Exists(x => x.IsNumber(num)))//checks if num is already in the list
                            {
                                fileNum = fileNumbers.Find(x => x.IsNumber(num));
                                fileNum.IncreaseOccurances();
                            }
                            else//number is not yet in the list
                            {
                                fileNum = new FileNumber(num);
                                fileNumbers.Add(fileNum);
                            }
                            //Console.WriteLine(fileNum.ToString());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Unable to parse " + s);
                        }
                        
                        //Console.WriteLine(s);
                    }
                }
                //
            }

            Console.WriteLine("Press any button to exit");
            Console.ReadLine();//keep the console open
        }
    }

    class FileNumber
    {
        readonly int number;
        int numOfOccurances;

        public FileNumber(int num)
        {
            number = num;
            numOfOccurances = 1;
        }

        public bool IsNumber(int num)
        {
            return number == num;
        }

        public void IncreaseOccurances()
        {
            numOfOccurances++;
        }

        public override String ToString()
        {
            return number + " appears " + numOfOccurances + " Times";
        }
    }

}
