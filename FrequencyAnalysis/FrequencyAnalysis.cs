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
                    //calling CheckFileNameFormat to get filename
                    string filename = CheckFileNameFormat(args);

                    //get case sensitive option from user 
                    ConsoleKeyInfo keyPress = InputMenu();

                    // call method ReadTextFile to read text from text file by passing filename 
                    string txtResult = ReadTextFile(filename, keyPress);
                   
                    // use Regular Expression to remove all whitespace
                    txtResult = Regex.Replace(txtResult, @"\s", "");

                    //call FrequencyAnalyse method to sort and count character in textfile by passing text variable
                    Dictionary<char, int> SortedDict = FrequencyAnalyse(txtResult);

                    //calling DisplayOutput method to write output to console
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
                Console.WriteLine("Please input text file name");
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n"+e.Message);
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }

        // Summary:
        //     Read all lines from input text file and return a text in one line
        //
        // Parameters:
        //   txtFilename: 
        //      input filename:
        //   keyPress: 
        //      The System.ConsoleKeyInfo is the console key that was pressed which including the character
        //
        // Returns:
        //     a string that contains all lines in textfile including whitespace, newline, carriage return and tab
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
                // each element of the array is one line of the file, and then closes the file.
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
            catch (FileNotFoundException)
            { 
                // if file not found throw exception
                throw new FileNotFoundException("[textfile is not in directory]");
            }        
        }

        // Summary:
        //     count number of frequency of character orrcurences contains in input string in descending order
        //     then by character and select top 10 pairs
        //
        // Parameters:
        //   txtResult:
        //     A string that contains all characters except all whitespace from file. 
        //
        // Returns:
        //     Sorted Dictionary that contains top 10 most frequently occurring characters in descending order
        //
        // Exceptions:
        //   System.OverflowException:
        //     File is not text file or file is too large
        public static Dictionary<char, int> FrequencyAnalyse(string txtResult)
        {
            try
            {
                //use LINQ to group and count chararcter and store in Dictionary
                Dictionary<char, int> counts = txtResult.GroupBy(c => c).ToDictionary(grp => grp.Key, grp => grp.Count());

                //sort dictionary by frequency of occurence in descending order then by ascii code character and select first 10 pairs 
                var sortedDict = (from entry in counts orderby entry.Value descending, entry.Key descending select entry).Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);
                
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
        //     A string that contains all characters and no whitespace from file 
        //   SortedDict:
        //     Dictionary contains the top 10 most frequently occurring characters  with the number of occurrences of each.
        public static void DisplayOutput(string txtResult, Dictionary<char, int> sortedDict)
        {
            //write total number of characters to console
                Console.WriteLine("\nTotal characters: " + txtResult.Length);

                foreach (var item in sortedDict)
                {
                    //write each charater and the number of occurence to console
                    Console.WriteLine(" " + item.Key + " (" + item.Value + ")");

                }
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
        }

        // Summary:
        //     display output to console to ask user for case sensitive options
        //     the app will keep repeated to ask user until user press Y or N key
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
                
                //get the next key presses by the user
                keyPress = Console.ReadKey();

            } while (keyPress.Key != ConsoleKey.Y && keyPress.Key != ConsoleKey.N);

            return keyPress;
        }

        // Summary:
        //     Check if the input filename end with .txt
        //
        // Parameters:
        //   args:
        //     commandline arguments.
        //
        // Returns:
        //      String filename  
        //
        // Exceptions:
        //   System.IndexOutOfRangeException:
        //      no file name is given via command line
        //   System.Exception:
        //      file name is not end with .txt
        public static string CheckFileNameFormat(string[] args)
        {
            try
            {
                //take first element from commandline input and removes all leading and trailing white-space characters from this string
                string filename = args[0].Trim();

                //if the end of filename input is not matches with .txt will thorw exception
                if (!filename.EndsWith(".txt")) {
                    throw new Exception("Please input .txt file");
                
                }

                //return filename if filename is end with .txt
                return filename;
            }
            catch (IndexOutOfRangeException)
            { 
                //if user not input filename
                throw new IndexOutOfRangeException("Please input text file name");
            }
        
        }
    }
}
