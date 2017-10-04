using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace ES.Utils
{
    public class Auth
    {
        public static bool VerifySignHttp(HttpContext httpContext, ref XmlDocument xmlDoc)
        {
            try
            {
                return true;
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось проверить подпись сообщения", causeEx);
            }
        }

        public static void MakeSignHttp(HttpContext httpContext, ref XmlDocument xmlDoc)
        {
            try
            {
                httpContext.Response.AddHeader("eKassir-Signature", "SIGN");
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось поставить подпись сообщения", causeEx);
            }
        }

        public static bool VerifySign(string systemId, string msg, string sign)
        {
            try
            {
                return true;
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось проверить подпись сообщения", causeEx);
            }
        }

        public static string MakeSign(string systemId, string msg)
        {
            try
            {
                return "SIGN";
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось поставить подпись сообщения", causeEx);
            }
        }
    }
}