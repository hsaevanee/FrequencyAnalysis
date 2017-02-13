using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrequencyAnalysis
{
    public class FrequencyAnalysis
    {
        public static void Main(string[] args)
        {

            try {
                    string filename = CheckFileNameFormat(args);

                    ConsoleKeyInfo keyPress = InputMenu();

                    // call method ReadTextFile to read text from text file by passing filename 
                    string txtResult = ReadTextFile(filename, keyPress);
                    //counts = counts.OrderByDescending(x => x.Value).ThenBy(x=>x.Key).Take(10).ToDictionary(x => x.Key, x => x.Value);

                    // use Regular Expression to remove all whitespace
                    txtResult = Regex.Replace(txtResult, @"\s", "");

                    //call FrequencyAnalyse method to sort and count character in textfile by passing text variable
                    Dictionary<char, int> SortedDict = FrequencyAnalyse(txtResult);

                    DisplayOutput(txtResult, SortedDict);
                    
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("\n[Data File Missing] {0}", e.FileName);
                // Keep the console window open.
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
            catch (IndexOutOfRangeException)
            {
                //throw new IndexOutOfRangeException("Please insert text file");
                Console.WriteLine("Please insert text file");
                // Keep the console window open.
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n"+e.Message);
                // Keep the console window open.
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }

        // Summary:
        //     Read all lines from input text file and return a text in one line
        //
        // Parameters:
        //   txtFilename: input filename:
        //   keyPress: The System.ConsoleKeyInfo is the console key that was pressed which including the character
        //
        // Returns:
        //     a string that contains all lines in textfile including whitespace and tab
        //
        // Exceptions:
        //   System.FileNotFoundException:
        //     File is not found.
        public static string ReadTextFile(string txtFilename, ConsoleKeyInfo keyPress)
        {
            try
            {
                //combine filename into the current working directory of the application.
                var path = Path.Combine(Directory.GetCurrentDirectory(), txtFilename);

                // Read each line of the file into a string array. 
                // each element of the array is one line of the file.
                string[] lines = System.IO.File.ReadAllLines(path);
                string txtResult = "";

                //check if the console key y is pressed for case sensitive.
                if (keyPress.Key == ConsoleKey.Y)
                {

                    //Join each element of lines array in one line of the string
                    txtResult = string.Join(txtResult, lines);
                }
                else
                {
                    //Join each element of lines array in one line of the string
                    txtResult = string.Join(txtResult, lines);
                    //convert all text characters to lower cases
                    txtResult = txtResult.ToLower();
                }

                return txtResult;

            }
            catch (FileNotFoundException e)
            { 
                // if file not found throw exception
                throw new FileNotFoundException("[textfile is not in directory]");
            }        
        }

        // Summary:
        //     Converts the specified string representation of a logical value to its Boolean
        //     equivalent, using the specified culture-specific formatting information.
        //
        // Parameters:
        //   value:
        //     A string that contains the value of either System.Boolean.TrueString or System.Boolean.FalseString.
        //
        //   provider:
        //     An object that supplies culture-specific formatting information. This parameter
        //     is ignored.
        //
        // Returns:
        //     Sorted Dictionary that contains top tens the most frequency characters in descending order
        //
        public static Dictionary<char, int> FrequencyAnalyse(string txtResult)
        {
            try
            {
                //use LINQ to group and count chararcter and store in Dictionary
                Dictionary<char, int> counts = txtResult.GroupBy(c => c).ToDictionary(grp => grp.Key, grp => grp.Count());

                //sort dictionary by frequency of occurence in descending order and select first 10 pairs 
                var tempDict = (from entry in counts orderby entry.Value descending select entry).Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);
                // sort again by frequency of occurence and ascii code character
                var sortedDict = (from entry in tempDict orderby entry.Value descending, Convert.ToByte(entry.Key) descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
                //return sorted dictionary
                return sortedDict;
            }            
            catch (OverflowException)
            { 
                // if file not found throw exception
                throw new OverflowException("File is not text file or file is too large");
            }  
        }
        // Summary:
        //     display output to console
        //
        // Parameters:
        //   txtResut:
        //     A string that contains the value of either System.Boolean.TrueString or System.Boolean.FalseString.
        //
        //   provider:
        //     An object that supplies culture-specific formatting information. This parameter
        //     is ignored.
        //
        // Returns:
        //     Sorted Dictionary that contains top tens the most frequency characters in descending order
        //
        public static void DisplayOutput(string txtResult, Dictionary<char, int> sortedDict)
        {
                //write totla number of text to console
                Console.WriteLine("\nTotal characters: " + txtResult.Length);

                foreach (var item in sortedDict)
                {
                    //write each charater and the number of occurence to console
                    Console.WriteLine(" " + item.Key + " (" + item.Value + ")");

                }
                // Keep the console window open.
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
        }

        // Summary:
        //     display output to console to ask user for case sensitive options
        //     the user will b
        //
        // Returns:
        //      System.ConsoleKeyInfo object that was pressed 
        //
        public static ConsoleKeyInfo InputMenu()
        {
            ConsoleKeyInfo keyPress;
            
            do
            {
                Console.Write("\nCase sensitive (Y/N): ");
                 
                keyPress = Console.ReadKey(true);

            } while (keyPress.Key != ConsoleKey.Y && keyPress.Key != ConsoleKey.N);

            return keyPress;
        }


        public static string CheckFileNameFormat(string[] args)
        {
            try
            {
                string filename = args[0].Trim();

                if (!filename.EndsWith(".txt")) {
                    throw new Exception("Please input .txt file");
                
                }

                return filename;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Please insert text file");
            }
        
        }
    }
}
