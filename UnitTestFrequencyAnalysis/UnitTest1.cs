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
        public const string IndexOutOfRangeExceptionMessage = "Please input text file name";
        public const string ExceptionMessage = "Please input .txt file"; 

        [TestMethod]
        public void TestFrequencyAnalysisMethod()
        {
            // arrange
            Dictionary<char, int> expected = new Dictionary<char, int>();
            expected.Add('t', 4);
            expected.Add('s', 3);
            expected.Add('i', 2);
            expected.Add('e', 2);
            expected.Add('x', 1);
            expected.Add('h', 1);
            expected.Add('a', 1);
            expected.Add('T', 1);

            //act
            string txtResult = "Thisisatesttext";
            Dictionary<char, int> actual = FrequencyAnalysis.FrequencyAnalysis.FrequencyAnalyse(txtResult);
            //assert
            CollectionAssert.AreEquivalent(expected.ToList(), actual.ToList());
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());

            //arrange
            txtResult = txtResult.ToLower();
            expected = txtResult.GroupBy(c => c).ToDictionary(grp => grp.Key, grp => grp.Count());
            expected = expected.OrderByDescending(x => x.Value).ThenByDescending(x => x.Key).Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);

            //act
            actual = FrequencyAnalysis.FrequencyAnalysis.FrequencyAnalyse(txtResult);
            //assert
            CollectionAssert.AreEquivalent(expected.ToList(), actual.ToList());
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());

        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "[textfile is not in directory]")]
        public void TestReadTextFile()
        {
            //arrange
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo('Y', ConsoleKey.Y, false, false, false);
            // act 
            string txtResult = FrequencyAnalysis.FrequencyAnalysis.ReadTextFile("Sample3.txt", keyPress);
            // assert is handled by the ExpectedException 
        }

        [TestMethod]
        public void TestReadTextFile2()
        {
            //arrange
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo('Y', ConsoleKey.Y, false, false, false);
            string expected = "The three did feed the deerThe quick brown fox jumped over the lazy dog";
            // act
            string txtResult = FrequencyAnalysis.FrequencyAnalysis.ReadTextFile("Sample.txt", keyPress);
            // assert 
            Assert.IsTrue(expected.SequenceEqual(txtResult));

        }

        [TestMethod]
        public void TestCheckFileNameFormat()
        {
            //arrange
            string[] args = { };

            try
            {
                // act
                FrequencyAnalysis.FrequencyAnalysis.CheckFileNameFormat(args);
            }
            catch (IndexOutOfRangeException)
            {
                // assert  
                StringAssert.Contains("Please input text file name", IndexOutOfRangeExceptionMessage);
                return;
            }
            //assert
            Assert.Fail("No exception was thrown.");  
        }

        [TestMethod]
        public void TestCheckFileNameFormat2()
        {
            //arrange
            string[] args = {"test.dll"};

            try
            {
                // act
                FrequencyAnalysis.FrequencyAnalysis.CheckFileNameFormat(args);
            }
            catch (Exception)
            {
                // assert  
                StringAssert.Contains("Please input .txt file", ExceptionMessage);
                return;
            }
            //assert
            Assert.Fail("No exception was thrown.");
        }


    }
}
