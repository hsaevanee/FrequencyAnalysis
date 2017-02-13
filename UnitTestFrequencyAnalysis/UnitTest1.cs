using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrequencyAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace UnitTestFrequencyAnalysis
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(OverflowException), "File is not text file or file is too large")]
        public void TestFrequencyAnalysisMethod()
        {
            //ConsoleKeyInfo keyPress = FrequencyAnalysis.FrequencyAnalysis.InputMenu();
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo('Y', ConsoleKey.Y, false, false, false);
             
            string txtResult = FrequencyAnalysis.FrequencyAnalysis.ReadTextFile("Sample3.txt", keyPress);
            //counts = counts.OrderByDescending(x => x.Value).ThenBy(x=>x.Key).Take(10).ToDictionary(x => x.Key, x => x.Value);

            // use Regular Expression to remove all whitespace
            txtResult = Regex.Replace(txtResult, @"\s", "");
             
            //Dictionary<char, int> expected = txtResult.GroupBy(c => c).ToDictionary(grp => grp.Key, grp => grp.Count());

            //expected = (from entry in expected orderby entry.Value descending, Convert.ToByte(entry.Key) descending select entry).Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);

            Dictionary<char, int> actual = FrequencyAnalysis.FrequencyAnalysis.FrequencyAnalyse(txtResult);

            //CollectionAssert.AreEquivalent(expected.ToList(), actual.ToList());

        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "[textfile is not in directory]")]
        public void TestReadTextFile()
        {
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo('Y', ConsoleKey.Y, false, false, false);

            string txtResult = FrequencyAnalysis.FrequencyAnalysis.ReadTextFile("Sample3.txt", keyPress);

        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "[textfile is not in directory]")]
        public void TestReadTextFile()
        {
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo('Y', ConsoleKey.Y, false, false, false);

            string txtResult = FrequencyAnalysis.FrequencyAnalysis.ReadTextFile("Sample3.txt", keyPress);

        }
    }
}
