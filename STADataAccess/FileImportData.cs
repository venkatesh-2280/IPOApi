using IPOApi.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Data;
using static System.Net.WebRequestMethods;
using static IPOApi.Models.UtilityModel;

namespace IPOApi.STADataAccess
{
    public class FileImportData
    {
        DataSet ds = new DataSet();
        List<IDbDataParameter>? parameters;
        CommonHeader objlog = new CommonHeader();
        string constring1 = "";        
        DataTable result = new DataTable();

        public DataSet getBank(string constring)
        {

            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                ds = dbManager.execStoredProcedurelist("pr_get_all_banknames", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                // Log error if any
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_all_banknames" + " Error Message:" + ex.Message);
            }

            return ds; // Return the DataSet with results
        }

        public DataSet getJobinfo(FileInfoRequest fileinfo, string constring)
        {
            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_ipo_code", fileinfo.ipo_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_dataset_code", fileinfo.dataset_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_ipo_get_datasetjob", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_datasetjob" + " Error Message:" + ex.Message);
            }

            return ds;

        }

        // getdatasetPipelineService
        public DataSet getdatasetPipelineData(string Pipeline_code, string constring)
        {
            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_pipeline_code", Pipeline_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_ipo_get_datasetpipeline", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_datasetpipeline" + " Error Message:" + ex.Message);
            }
            return ds;
        }

        public DataSet getdatasetTotalData(string ipocodes, string constring)
        {
            try
            {
                constring1 = constring;
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reference_no", ipocodes, DbType.String));
                ds = dbManager.execStoredProcedure("pr_get_benpos_total", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_benpos_total" + " Error Message:" + ex.Message);
            }
            return ds;
        }

    }
}
