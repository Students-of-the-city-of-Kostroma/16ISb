using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        { 
            //arrange
            string str = "5+5";
            double expected = 10;
            //act
            int actual = /*метод из кода*/(str);
            //assert
            Assert.AreEqual(actual, expected);
        }
         public void TestMethod1()
        { 
            //arrange
            string str = "5*5";
            double expected = 25;
            //act
            int actual = /*метод из кода*/(str);
            //assert
            Assert.AreEqual(actual, expected);
        }
         public void TestMethod1()
        { 
            //arrange
            string str = "5/5";
            double expected = 1;
            //act
            int actual = /*метод из кода*/(str);
            //assert
            Assert.AreEqual(actual, expected);
        }
         public void TestMethod1()
        { 
            //arrange
            string str = "5-5";
            double expected = 0;
            //act
            int actual = /*метод из кода*/(str);
            //assert
            Assert.AreEqual(actual, expected);
        }
    }
}
