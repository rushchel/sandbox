using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MakeWS
{
    /// <summary>
    /// Сводное описание для WebService
    /// </summary>
    [WebService(Namespace = "http://home.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Request))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Response))]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Привет всем!";
        }

        [WebMethod]
        public int GetSum(int a, int b) => a + b;

        [WebMethod]
        public Response GetData(Request request)
        {
            Response resp = new Response
            {
                ResponseVar = request.RequestVar
            };
            return resp;
        }
    }

    public class Request
    {
        [System.Xml.Serialization.XmlElementAttribute("req", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        private string requestVar;

        [System.Xml.Serialization.XmlElementAttribute("req", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RequestVar { get => requestVar; set => requestVar = value; }
    }

    public class Response
    {
        [System.Xml.Serialization.XmlElementAttribute("resp", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        private string responseVar;

        [System.Xml.Serialization.XmlElementAttribute("resp", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ResponseVar { get => responseVar; set => responseVar = value; }

        public Response()
        {
        }
    }
}
