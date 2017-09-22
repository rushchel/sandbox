using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NLog;
using ES.V3;
using ES.Utils;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace ES
{
    /// <summary>
    /// Summary description for AgentService
    /// </summary>
    [WebService(Namespace = Schema.xmlNS)]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AgentService : System.Web.Services.WebService
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        [WebMethod]
        public string TestConnectDb()
        {
            try
            {
                return OraDB.DoTestQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Произошла ошибка!");
                return ex.Message;
            }
        }

        [WebMethod]
        public string DoValidation(XmlDocument request)
        {
            try
            {
                XmlDocument inp = request;
                Schema.Validate(inp);
                return "Success";
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Произошла ошибка!");
                return ex.Message;
            }
        }

        [WebMethod]
        public string GetRequestType(XmlDocument request)
        {
            try
            {
                XmlDocument inp = request;
                Schema.Validate(inp);
                string result = inp.DocumentElement.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Произошла ошибка!");
                return ex.Message;
            }
        }
    }
}
