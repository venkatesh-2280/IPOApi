using IPOApi.Models;
using System.Data;

namespace IPOApi.STADataAccess
{
    public class EmailData
    {
        DataSet ds = new DataSet();
        List<IDbDataParameter>? parameters;
        CommonHeader objlog = new CommonHeader();
        string constring1 = "";
        DataTable result = new DataTable();

        public DataTable SendIpoEmailsData(string offer_code, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reference_no", offer_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_get_email_list", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_email_list" + "Error Message:" + ex.Message);
                return result;
            }
        }

        public void UpdateEmailStatus(string offerCode,int gid,string status,string errorMessage,int entlid, string in_action, string constring)
            {
                try
                {
                    DBManager dbManager = new DBManager(constring);

                    parameters = new List<IDbDataParameter>();

                    parameters.Add(dbManager.CreateParameter("in_refernce_no", offerCode, DbType.String));
                    parameters.Add(dbManager.CreateParameter("in_gid", gid, DbType.Int32));
                    parameters.Add(dbManager.CreateParameter("in_status", status, DbType.String));
                    parameters.Add(dbManager.CreateParameter("in_error", errorMessage, DbType.String));
                     parameters.Add(dbManager.CreateParameter("in_entitlement_id", entlid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_action", in_action, DbType.String));
                dbManager.execStoredProcedure("pr_update_ipo_email_log",CommandType.StoredProcedure,parameters.ToArray());
                }
                catch (Exception ex)
                {
                    CommonHeader objlog = new CommonHeader();
                    objlog.logger("SP:pr_update_ipo_email_log Error:" + ex.Message);
                }
        }

        //getemailListData
        public DataSet getemailListData(string offer_code, string in_action,string constring)
            {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_refernce_no", offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action", in_action, DbType.String));
                ds = dbManager.execStoredProcedure("pr_ipo_get_emaillist", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_emaillist" + "Error Message:" + ex.Message);
                return ds;
            }
        }

        //GetbidfilecountsummaryData
        public DataSet GetbidfilecountsummaryData(string ipo_code, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_ipo_code", ipo_code, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_ipo_get_bidfile_count", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_bidfile_count Error Message: " + ex.Message);
            }
            return ds;
        }

    }
}
