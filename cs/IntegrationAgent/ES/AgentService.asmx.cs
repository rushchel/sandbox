using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NLog;
using ES.V3;
using ES.Utils;

namespace ES
{
    /// <summary>
    /// Summary description for AgentService
    /// </summary>
    [WebService(Namespace = XmlNS)]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AgentService : System.Web.Services.WebService
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public const string XmlNS = "http://otpbank.ru/ocl/ia/";

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
        public string GetType(ChildClass inp)
        {
            return inp.GetType().ToString();
        }

        [WebMethod]
        public Response GetDirectory(Request request)
        {
            GetDirectoryRequest rq = (GetDirectoryRequest)request;
            GetDirectoryResponse resp = new GetDirectoryResponse();
            return resp;
        }
    }

    //[System.Xml.Serialization.XmlInclude(typeof(ChildClass))]
    public abstract class BaseClass
    {
        public string guid;
        public string maker;
    }

    //[Serializable]
    public class ChildClass : BaseClass
    {
        public string model;
    }
}
