using DocumentFormat.OpenXml.Drawing.Diagrams;
using IPOApi.Models;
using System.Data;


namespace IPOApi.STADataAccess
{
    public class RunEntitlementData
    {
        DataSet ds = new DataSet();
        List<IDbDataParameter>? parameters;
        CommonHeader objlog = new CommonHeader();
        string constring1 = "";
        DataTable result = new DataTable();

        public DataSet Export_ri_allotment_bo(string offer_code, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_offercode", offer_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_get_tentitlementAllotment", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_finalAllotment" + "Error Message:" + ex.Message);
                return null;
            }
        }

    }
}
