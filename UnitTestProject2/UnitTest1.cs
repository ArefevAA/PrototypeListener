using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SquareCalcTest()
        {
            // input = 5, output = 25
            using (var sw = new StringWriter())
            {
                int inputValue = 5;
                int outputValue = 25;
                Console.SetOut(sw);
                int result = EPLRPrototypeListener.EPLRPrototypeListenerServer.SquareCalc(inputValue);
                Assert.AreEqual(outputValue, result);
            }
        }

        [TestMethod]
        public void IncCalc()
        {
            // input = 5, output = 6
            using (var sw = new StringWriter())
            {
                int inputValue = 5;
                int outputValue = 6;
                Console.SetOut(sw);
                int result = EPLRPrototypeListener.EPLRPrototypeListenerServer.IncCalc(inputValue);
                Assert.AreEqual(outputValue, result);
            }
        }
    }
}