using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ES;

namespace ESTest
{
    [TestClass]
    public class AgentServiceTest
    {
        private static AgentService agent = new AgentService();

        private static string dataDir = Environment.CurrentDirectory + "\\Data\\";

        /*[TestMethod]
        public void DoValidation_WithValidXml()
        {
            XmlDocument inputDocument = new XmlDocument();
            inputDocument.Load(dataDir + "Valid.xml");
            string expected = "Success";
            string actual = agent.DoValidation(inputDocument);
            Assert.AreEqual(expected, actual, true, "Test DoValidation_WithValidXml failed");
        }

        [TestMethod]
        public void DoValidation_WithNoValidXml()
        {
            XmlDocument inputDocument = new XmlDocument();
            inputDocument.Load(dataDir + "NoValid.xml");
            string expected = "Success";
            string actual = agent.DoValidation(inputDocument);
            Assert.AreNotEqual(expected, actual, true, "Test DoValidation_WithNoValidXml failed");
        }

        [TestMethod]
        public void GetRequestType_WithValidXml()
        {
            XmlDocument inputDocument = new XmlDocument();
            inputDocument.Load(dataDir + "Valid.xml");
            string expected = "AddPaymentTemplateRequest";
            string actual = agent.GetRequestType(inputDocument);
            Assert.AreEqual(expected, actual, true, "Test GetRequestType_WithValidXml failed");
        }*/
    }
}
