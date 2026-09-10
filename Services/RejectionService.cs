using IPOApi.Models;
using IPOApi.STADataAccess;
using System.Data;
//using static IPOApi.Models.UserManagementModel;

namespace IPOApi.Services
{
    public class RejectionService
    {
        public static DataTable GetRejService(string offer_code, bool runRule, string constring)
        {
            DataTable ds = new DataTable();
            try
            {
                RejectionData objDS = new RejectionData();
                ds = objDS.GetRejData(offer_code, runRule, constring);
            }
            catch (Exception e)
            { }
            return ds;
        }

        public static DataTable GetRejdetailService(string offer_code, string bank_code, string constring)
        {
            DataTable ds = new DataTable();
            try
            {
                RejectionData objDS = new RejectionData();
                ds = objDS.GetRejdetailData(offer_code, bank_code, constring);
            }
            catch (Exception e)
            { }
            return ds;
        }

        // runRejectionService
        public static DataTable runRejectionService(string offer_code, string constring)
        {
            DataTable ds = new DataTable();
            try
            {
                RejectionData objDS = new RejectionData();
                ds = objDS.runRejectionData(offer_code, constring);
            }
            catch (Exception e)
            { }
            return ds;
        }

        public static DataSet GetAddRejService(string ipo_code,
                                          string appl_no,
                                          string order_no,
                                          string pan_no,
                                          string flag, string constring)
        {
            DataSet ds = new DataSet();

            try
            {
                RejectionData objData = new RejectionData();
                ds = objData.GetAddRejData(ipo_code,
            appl_no,
            order_no,
            pan_no,
            flag,
            constring);
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public static DataSet saveaddrejdetails(RejectionModel insOb, headerValue header_value, string constring)
        {
            DataSet ds = new DataSet();

            try
            {
                RejectionData objData = new RejectionData();
                ds = objData.saveaddrejdetail(insOb, header_value, constring);
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public static DataSet Getrulecode(string constring, string ipo_code)
        {
            DataSet ds = new DataSet();
            try
            {
                RejectionData objData = new RejectionData();
                ds = objData.Getrulecode(constring,ipo_code);
            }
            catch (Exception)
            {
            }
            return ds;
        }

        public static DataTable InsertRightsEntitlement(
    string offer_code,
    string constring)
        {
            try
            {
                RejectionData objDS =
                    new RejectionData();

                return objDS.InsertRightsEntitlement(
                    offer_code,
                    constring
                );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetRightsEntitlement(
    string offer_code,
    string constring)
        {
            try
            {
                RejectionData objDS =
                    new RejectionData();

                return objDS.GetRightsEntitlement(
                    offer_code,
                    constring
                );
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
