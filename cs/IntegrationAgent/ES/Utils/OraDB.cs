using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NLog;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Xml;

namespace ES.Utils
{
    public class OraDB
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string connectString = GetConnectString("IA");

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

        public static string DoTestQuery()
        {
            try
            {
                using (var conn = new OracleConnection(connectString))
                using (var cmd = new OracleCommand("select 'Success' from dual", conn))
                {
                    conn.Open();
                    string result = cmd.ExecuteScalar().ToString();
                    conn.Close();
                    return result;
                }
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось выполнить запрос в БД", causeEx);
            }
        }

        public static Sys GetSystem(long id)
        {
            Sys result = new Sys();
            try
            {
                using (var conn = new OracleConnection(connectString))
                using (var cmd = new OracleCommand("select * from ps_system where system_id = :ID", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("ID", OracleDbType.Int64, ParameterDirection.Input).Value = id;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables["Table"].Rows.Count == 0)
                    {
                        throw new Exception("Внешняя система с ID = "+ id.ToString() + " не найдена");
                    }
                    result.SystemId = Convert.ToInt64(ds.Tables["Table"].Rows[0]["SYSTEM_ID"]);
                    result.SysName = Convert.ToString(ds.Tables["Table"].Rows[0]["SYS_NAME"]);
                }
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось получить данные внешней системы из БД", causeEx);
            }
            return result;
        }

        public static void CheckSystemId(long systemId)
        {
        }

        public static XmlDocument CallStoredProc(long systemId, ref XmlDocument request)
        {
            XmlDocument result = new XmlDocument();
            try
            {
                using (var conn = new OracleConnection(connectString))
                using (var cmd = new OracleCommand("PS_PKG.HANDLE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("SYSTEM_ID", OracleDbType.Int64, ParameterDirection.Input).Value = systemId;
                    cmd.Parameters.Add("REQUEST", OracleDbType.Clob, ParameterDirection.Input).Value = request.OuterXml;
                    cmd.Parameters.Add("RESPONSE", OracleDbType.Clob, ParameterDirection.Output);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    result.LoadXml((string)((OracleClob)(cmd.Parameters["RESPONSE"].Value)).Value);
                    conn.Close();
                }
            }
            catch (Exception causeEx)
            {
                throw new Exception("Не удалось вызвать хранимую процедуру", causeEx);
            }
            return result;
        }
    }

    public class Sys
    {
        private long systemId;
        private string sysName;
        private string cacId;

        public Sys(){}
        public Sys(long id)
        {
            SystemId = id;
        }
        public Sys(long id, string name)
        {
            SystemId = id;
            SysName = name;
        }
        public Sys(long id, string name, string cac)
        {
            SystemId = id;
            SysName = name;
            CacId = cac;
        }

        public long SystemId
        {
            get { return systemId; }
            set { systemId = value; }
        }

        public string SysName
        {
            get { return sysName; }
            set { sysName = value; }
        }

        public string CacId
        {
            get { return cacId; }
            set { cacId = value; }
        }
    }
}