using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using System.Xml;
using System.Xml.Schema;

namespace ES.Utils
{
    public class Schema
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string pathToXsd = Environment.CurrentDirectory + "\\Protocol\\EkassirV3.xsd";

        public const string xmlNS = "http://otpbank.ru/ocl/ia";
        public const string xmlTargetNS = "http://ekassir.com/eKassir/PaySystem/Server/eKassirV3Protocol";
        public const string xsdW3CNS = "http://www.w3.org/2001/XMLSchema";
        public const string xsdW3CInstanceNS = "http://www.w3.org/2001/XMLSchema-instance";

        public static void Validate(ref XmlDocument xmlDoc)
        {
            try
            {
                xmlDoc.Schemas.Add(xmlTargetNS, pathToXsd);
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                xmlDoc.Validate(eventHandler);
                logger.Trace("Валидация XML успешна");
            }
            catch (Exception causeEx)
            {
                throw new Exception("Валидация XML не прошла", causeEx);
            }
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    throw e.Exception;
                case XmlSeverityType.Warning:
                    logger.Warn(e.Message);
                    break;
            }
        }
    }
}