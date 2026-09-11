using IPOApi.Models;
using IPOApi.STADataAccess;
using System.Data;
using static IPOApi.Models.BOAModel;

namespace IPOApi.Services
{
    public class RunEntitlementService
    {
        public static DataSet Export_ri_allotment_bo(string offer_code, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                RunEntitlementData objDS = new RunEntitlementData();
                ds = objDS.Export_ri_allotment_bo(offer_code, constring);
            }
            catch (Exception e)
            { }
            return ds;
        }

    }
}
