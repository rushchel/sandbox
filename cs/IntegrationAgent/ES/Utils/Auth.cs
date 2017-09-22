using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ES.Utils
{
    public class Auth
    {
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