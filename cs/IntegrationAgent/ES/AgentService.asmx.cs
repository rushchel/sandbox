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

        private XmlDocument GetInternalErrorResponse()
        {
            return new XmlDocument();
        }
        private XmlDocument VerifySignFail()
        {
            return new XmlDocument();
        }
        private Sys GetSystem()
        {
            try
            {
                long requestSystemID = Convert.ToInt64(Context.Request.Headers.Get("eKassir-PointID"));
                if (requestSystemID == 0)
                {
                    throw new Exception("Не указан http-заголовок eKassir-PointID");
                }
                return OraDB.GetSystem(requestSystemID);
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось получить информацию", causeEx);
            }
        }

        public void LogRequest()
        {
            var request = Context.Request;
            var inputStream = request.InputStream;
            inputStream.Position = 0;
            using (var reader = new StreamReader(inputStream))
            {
                string headers = request.Headers.ToString();
                string body = reader.ReadToEnd();
                string rawRequest = string.Format("{0}{1}{1}{2}", headers, Environment.NewLine, body);
                logger.Info(rawRequest);
            }
        }

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
        public XmlDocument DoExchange(XmlDocument request)
        {
            XmlDocument response = new XmlDocument();
            Sys system = GetSystem();
            try
            {
                if (Auth.VerifySignHttp(Context, ref request))
                {
                    response = OraDB.CallStoredProc(system.SystemId, ref request);
                }
                else
                {
                    response = VerifySignFail();
                    Context.Response.StatusCode = 401;
                    Context.Response.StatusDescription = "Unauthorized";
                }
            }
            catch (Exception ex)
            {
                response = GetInternalErrorResponse();
                logger.Error(ex, "Произошла ошибка!");
            }
            finally
            {
                Auth.MakeSignHttp(Context, ref response);
            }
            return response;
        }
    }
}
