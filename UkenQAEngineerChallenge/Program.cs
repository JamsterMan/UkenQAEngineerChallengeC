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
            FileNumber fileNum;

            foreach (var file in files)
            {
                using (StreamReader sr = File.OpenText(file))
                {
                    fileNumbers = new List<FileNumber>();
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
                    }
                }
                fileNum = FindLeastSeenNumber();
                Console.WriteLine("File: " + Path.GetFileName(file) + ", Number: " + fileNum.GetNumber() + ", Repeated: " + fileNum.GetOccuarnces() + " times");
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();//keep the console open
        }

        /*
         * Finds and returns the number in the file that shows up the least
         */
        private FileNumber FindLeastSeenNumber()
        {
            //sorts by occurances then by number if needed
            fileNumbers.Sort(delegate (FileNumber x, FileNumber y)
            {
                if (x.GetOccuarnces() > y.GetOccuarnces())
                    return 1;
                else if (x.GetOccuarnces() < y.GetOccuarnces())
                    return -1;
                else
                {
                    if (x.GetNumber() > y.GetNumber())
                        return 1;
                    else
                        return -1;
                }
            });
            return fileNumbers[0];
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

        public int GetNumber()
        {
            return number;
        }

        public int GetOccuarnces()
        {
            return numOfOccurances;
        }

        public override String ToString()
        {
            return number + " appears " + numOfOccurances + " Times";
        }
    }

}
