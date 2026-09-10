using IPOApi.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using System.Security.AccessControl;
//using static IPOApi.Models.UserManagementModel;

namespace IPOApi.STADataAccess
{
    public class RejectionData
    {
        DataSet ds = new DataSet();
        List<IDbDataParameter>? parameters;
        CommonHeader objlog = new CommonHeader();
        string constring1 = "";
        DataTable result = new DataTable();


        public DataTable GetRejData(string offer_code, bool runRule, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reference_no", offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_runRule", runRule, DbType.Boolean));
                ds = dbManager.execStoredProcedure("pr_ipo_get_rejection_count", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_rejection" + "Error Message:" + ex.Message);
                return result;
            }
        }

        public DataTable GetRejdetailData(string offer_code, string rule_code, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reference_no", offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_rule_code", rule_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_get_reject_recon_detail", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reject_recon_detail" + "Error Message:" + ex.Message);
                return result;
            }
        }

        // runRejectionData

        public DataTable runRejectionData(string offer_code, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_ipo_code", offer_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_run_boa", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_boa" + "Error Message:" + ex.Message);
                return result;
            }
        }

        public DataSet GetAddRejData(string ipo_code,
                                string appl_no,
                                string order_no,
                                string pan_no,
                                string flag, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_ipo_code", ipo_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_appl_no", appl_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_order_no", order_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_pan_no", pan_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_flag", flag, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_ipo_get_additonrejection_details", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_additonrejection_details Error Message: " + ex.Message);
            }
            return ds;
        }

        public DataSet saveaddrejdetail(RejectionModel insObj, headerValue header_value, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>(); // if no params, leave empty
                parameters.Add(dbManager.CreateParameter("in_ipo_code", insObj.ipo_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_rule_code", insObj.rule_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_applno", insObj.applno, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_orderno", insObj.orderno, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_panno", insObj.panno, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_quantity", insObj.qty, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_price", insObj.shares, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_amount", insObj.amt, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_add", insObj.addremarks, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reject", insObj.rejremarks, DbType.String)); 
                parameters.Add(dbManager.CreateParameter("in_user_code", header_value.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_audit_flag", insObj.audit_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_flag", insObj.flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.Int32, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("pr_ipo_set_additionrejection_single", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_tclientdetails Error Message: " + ex.Message);
            }

            return ds;
        }

        public DataSet Getrulecode(string constring1, string ipo_code)
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring1);
                parameters = new List<IDbDataParameter>(); // if no params, leave empty
                parameters.Add(dbManager.CreateParameter("in_ipo_code", ipo_code, DbType.String));                                         // parameters.Add(dbManager.CreateParameter("in_rule_code", rulecode, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_ipo_get_rulemaster", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_rulemaster Error Message: " + ex.Message);
            }
            return ds;
        }

        public DataTable InsertRightsEntitlement(
    string offer_code,
    string constring)
        {
            DataTable result = new DataTable();

            try
            {
                DBManager dbManager = new DBManager(constring);

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_offer_code",offer_code,DbType.String));
                parameters.Add(dbManager.CreateParameter("in_depos_type", "",DbType.String));

                DataSet ds = dbManager.execStoredProcedure(
                    "pr_ins_rightsentitlement",
                    CommandType.StoredProcedure,
                    parameters.ToArray()
                );

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0];
                }

                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();

                objlog.logger(
                    "SP:pr_ins_rightsentitlement Error Message:"
                    + ex.Message
                );

                throw;
            }
        }

        public DataSet GetRightsEntitlement(
    string offer_code,
    string constring)
        {
            try
            {
                DBManager dbManager =
                    new DBManager(constring);

                parameters =
                    new List<IDbDataParameter>();

                parameters.Add(
                    dbManager.CreateParameter(
                        "in_offer_code",
                        offer_code,
                        DbType.String
                    )
                );

                DataSet ds =
                    dbManager.execStoredProcedure(
                        "pr_get_rightsentitlement",
                        CommandType.StoredProcedure,
                        parameters.ToArray()
                    );

                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds;
                }

                return new DataSet();
            }
            catch (Exception ex)
            {
                CommonHeader objlog =
                    new CommonHeader();

                objlog.logger(
                    "SP:pr_get_rightsentitlement Error Message:"
                    + ex.Message
                );

                throw;
            }
        }
    }
}
