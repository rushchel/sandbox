using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using NLog;
using ES.V3;

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
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetCountRows()
        {
            try
            {
                logger.Trace("Trace message");
                string connectStr = ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString;
                OracleConnection conn = new OracleConnection(connectStr);
                conn.Open();
                OracleCommand cmd = new OracleCommand("select count(*) from ia_cs_log", conn);
                string res = cmd.ExecuteScalar().ToString();
                conn.Close();
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public Response GetDirectory(Request request)
        {
            GetDirectoryRequest rq = (GetDirectoryRequest)request;
            GetDirectoryResponse resp = new GetDirectoryResponse();
            return resp;
        }
    }
}
