using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace ES.Utils
{
    public class OraDB
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string connectString = GetConnectString("IA");
        private static OracleConnection oraConnection = new OracleConnection(connectString);

        public static string GetConnectString(string connectName)
        {
            try
            {
                logger.Trace("Извлечение строки подключения {0}", connectName);
                return ConfigurationManager.ConnectionStrings[connectName].ConnectionString;
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось получить строку подключения к БД", causeEx);
            }
        }

        public static void OpenConnect()
        {
            try
            {
                oraConnection.Open();
                logger.Trace("Открыто соединение с БД");
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось открыть соединение с БД", causeEx);
            }
        }

        public static void CloseConnect()
        {
            try
            {
                oraConnection.Close();
                logger.Trace("Закрыто соединения с БД");
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось закрыть соединение с БД", causeEx);
            }
        }

        public static string DoTestQuery()
        {
            try
            {
                OpenConnect();
                OracleCommand cmd = new OracleCommand("select 'Success' from dual", oraConnection);
                string res = cmd.ExecuteScalar().ToString();
                CloseConnect();
                logger.Trace("Выполнен тестовый запрос в БД");
                return res;
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось выполнить запрос в БД", causeEx);
            }
        }
    }
}