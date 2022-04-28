using System;
using System.IO;

namespace BackendTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Enter search query - format <fileName.extension> <columnIndex> <searchKey>");
            var userInput = Console.ReadLine();

            IsValid(userInput?.Trim()); //it will throw execption if the format is incorrect

            var splitUserInput = userInput.Split(" "); 

            
            var path = Directory.GetCurrentDirectory() + $"\\{splitUserInput[0]}";

            try
            {
                var isFound = false; //flag to check if we found the value

                //reading file from path
                using var reader = new StreamReader(path);

                while (!reader.EndOfStream)
                {
                    //Getting line from file
                    var line = reader.ReadLine();

                    //csv can be split by ','
                    var values = line.Split(',');


                    if(values[Convert.ToInt32(splitUserInput[1])] == splitUserInput[2])
                    {
                        isFound = true;
                        Console.WriteLine("\n FoundData: \n\t" + line);
                    }

                }

                if (!isFound)
                    Console.WriteLine("\n data not found");

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void IsValid(string userInput)
        {
            var correctFormat = "<fileName.extension> <columnIndex> <searchKey>";
            if (string.IsNullOrEmpty(userInput))
                throw new Exception($"Less argument \n correct format {correctFormat}");

            var splitUserInput = userInput.Split(" ");

            if (splitUserInput.Length < 0 || splitUserInput.Length > 3)
                throw new Exception($"Incorrect input \n correct format {correctFormat}");

            if(!int.TryParse(splitUserInput[1],out var result))
            {
                throw new Exception($"Incorrect column index \n correct format {correctFormat}");
            }
        }
    }
}
