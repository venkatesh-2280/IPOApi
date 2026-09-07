using IPOApi.Models;
using MySql.Data.MySqlClient;
using System.Data;
using static IPOApi.Models.IssueSetupModel;
using static IPOApi.Models.UtilityModel;
using static System.Net.WebRequestMethods;

namespace IPOApi.STADataAccess
{
    public class IssueSetupData
    { 
        DataTable result = new DataTable();  
        DataSet ds = new DataSet();
        List<IDbDataParameter>? parameters;
        CommonHeader objlog = new CommonHeader();
        string constring1 = "";
        public DataSet getofferType(IssueSetupModel inscObj, string constring)
        {
            try
            { 
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("p_master_code", inscObj.mastercode, DbType.String));
                parameters.Add(dbManager.CreateParameter("p_depend_value", inscObj.dependvalue, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_masterlist", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                // Log error if any
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:sp_get_masterlist" + " Error Message:" + ex.Message);
            }

            return ds; // Return the DataSet with results
        }

        public DataSet Get_Offerlist(string in_user_code, string in_role_code, headerValue headerval, string constring)
        {
            DBManager dbManager = new DBManager(constring);
            parameters = new List<IDbDataParameter>();
             
            parameters.Add(dbManager.CreateParameter("in_user_code", in_user_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_role_code", in_role_code, DbType.String));
            DataSet ds = dbManager.execStoredProcedure(
                "pr_ipo_get_offerlist_new",
                CommandType.StoredProcedure,
                parameters.ToArray()
            );
            return ds;
        }

        public DataSet Get_OfferFetch(string client_code, string offer_code, headerValue headerval, string constring)
        {
            DBManager dbManager = new DBManager(constring);
            parameters = new List<IDbDataParameter>();

            parameters.Add(dbManager.CreateParameter("in_offer_code", offer_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_client_code", client_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
            parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));

            DataSet ds = dbManager.execStoredProcedure(
                "pr_ipo_get_offerFetch",
                CommandType.StoredProcedure,
                parameters.ToArray()
            );

            return ds;
        }

        public DataTable setoffer_header(OfferHeaderModel offerheader, headerValue headerval, string constring)
        {
            DataTable result = new DataTable();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>(); 
                parameters.Add(dbManager.CreateParameter("in_action", offerheader.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_header_gid", Convert.ToInt16(offerheader.offer_header_gid), DbType.Int32, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_offer_code", offerheader.offer_code, DbType.String, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_offer_type", offerheader.offer_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_listing", offerheader.offer_listing_no, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_isin", offerheader.offer_isin, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_status", offerheader.offer_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_empdiscount", offerheader.offer_empdiscount, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_remarks", offerheader.offer_remarks, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_client_code", offerheader.client_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_active_status", offerheader.active_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", offerheader.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", offerheader.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ipo_status", offerheader.ipo_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_boa_status", offerheader.boa_checked_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));
                DataSet ds = dbManager.execStoredProcedure("pr_ipo_set_offerheader", CommandType.StoredProcedure,parameters.ToArray());
                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP: pr_ipo_set_offerheader | Error: " + ex.Message);
            }

            return result;
        }

        public DataTable Set_OfferDetail(OfferDetailModel offerdetail, headerValue headerval, string constring)
        {
            DataTable result = new DataTable();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_action", offerdetail.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_detail_gid", offerdetail.offer_detail_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_offer_precapital", offerdetail.offer_precapital, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_issuesize", offerdetail.offer_issuesize, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_offer_postcapital", offerdetail.offer_postcapital, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_lotsize", offerdetail.offer_lotsize, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_offer_facevalue", offerdetail.offer_facevalue, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_premiun", offerdetail.offer_premiun, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_pricetype", offerdetail.offer_pricetype, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_fixedprice", offerdetail.offer_fixedprice, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_maximumprice", offerdetail.offer_maximumprice, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_minimumprice", offerdetail.offer_minimumprice, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_cutoffprice", offerdetail.offer_cutoffprice, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_offer_code", offerdetail.offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_client_code", offerdetail.client_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ratio_numerator", offerdetail.ratio_numerator, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_ratio_denominator", offerdetail.ratio_denominator, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_record_cutoff_date", offerdetail.record_cutoff_date, DbType.Date));
                parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));
                DataSet ds = dbManager.execStoredProcedure(
                    "pr_ipo_set_offerdetail",
                    CommandType.StoredProcedure,
                    parameters.ToArray()
                );

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP: pr_ipo_set_offerdetail | Error: " + ex.Message);
            }

            return result;
        }

        public DataTable Set_OfferBankers(OfferBankerModel offerbanker, headerValue headerval, string constring)
        {
            DataTable result = new DataTable();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_action", offerbanker.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_gid", offerbanker.banker_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_banker_type", offerbanker.banker_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_name", offerbanker.banker_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_address", offerbanker.banker_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_city", offerbanker.banker_city, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_state", offerbanker.banker_state, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_pincode", offerbanker.banker_pincode, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_holdname", offerbanker.banker_holdname, DbType.String));// add
                parameters.Add(dbManager.CreateParameter("in_banker_accountno", offerbanker.banker_accountno, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_banker_ifsc", offerbanker.banker_ifsc, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_code", offerbanker.offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_client_code", offerbanker.client_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));
                DataSet ds = dbManager.execStoredProcedure(
                    "pr_ipo_set_offerbanker",
                    CommandType.StoredProcedure,
                    parameters.ToArray()
                );

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP: pr_ipo_set_offerbanker | Error: " + ex.Message);
            }

            return result;
        }

        public DataTable SetOfferStack(OfferStackModel stack, headerValue headerval, string constring)
        {
            DataTable result = new DataTable();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_action", stack.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_gid", stack.stack_gid, DbType.Int32, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_stack_code", stack.stack_code, DbType.String, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_stack_type", stack.stack_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_name", stack.stack_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_address", stack.stack_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_city", stack.stack_city, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_state", stack.stack_state, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_pincode", stack.stack_pincode, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_contact", stack.stack_contact, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_designation", stack.stack_designation, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_email", stack.stack_email, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_stack_mobile", stack.stack_mobile, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_code", stack.offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_client_code", stack.client_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_active_status", stack.active_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", stack.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));

                DataSet ds = dbManager.execStoredProcedure(
                    "pr_ipo_set_offerstack",
                    CommandType.StoredProcedure,
                    parameters.ToArray()
                );

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP: pr_ipo_set_offerstack | Error: " + ex.Message);
            }

            return result;
        }

        public DataSet GetStackFetch(string action,string client_code, string offer_code,string stack_code,headerValue headerval, string constring)
        {
            DBManager dbManager = new DBManager(constring);
            parameters = new List<IDbDataParameter>();

            parameters.Add(dbManager.CreateParameter("in_action", action, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_client_code", client_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_offer_code", offer_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_stack_code", stack_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
            parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));

            DataSet ds = dbManager.execStoredProcedure(
                "pr_ipo_get_offerstack_fetch",
                CommandType.StoredProcedure,
                parameters.ToArray()
            );

            return ds;
        }

        public DataTable SetOffermiles(MilestoneModel milestone, headerValue headerval, string constring)
        {
            DataTable result = new DataTable();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_action", milestone.action, DbType.String));

                parameters.Add(dbManager.CreateParameter("in_milestone_gid", milestone.milestone_gid, DbType.Int32, ParameterDirection.InputOutput));

                parameters.Add(dbManager.CreateParameter("in_offer_openingdate", milestone.offer_openingdate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_closingdate", milestone.offer_closingdate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_allotmentdate", milestone.offer_allotmentdate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_approvaldate", milestone.offer_approvaldate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_listingdate", milestone.offer_listingdate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_nsdldate", milestone.offer_nsdldate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_cdsldate", milestone.offer_cdsldate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_refunddate", milestone.offer_refunddate, DbType.DateTime));
                parameters.Add(dbManager.CreateParameter("in_offer_code", milestone.offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_client_code", milestone.client_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_active_status", milestone.active_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));
                DataSet ds = dbManager.execStoredProcedure("pr_ipo_set_offermilestones",CommandType.StoredProcedure, parameters.ToArray());

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP: pr_ipo_set_offermilestones | Error: " + ex.Message);
            }

            return result;
        }

        public DataSet GetOffermiles(string client_code, string offer_code, headerValue headerval, string constring)
        {
            DBManager dbManager = new DBManager(constring);
            parameters = new List<IDbDataParameter>();

            parameters.Add(dbManager.CreateParameter("in_client_code", client_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("in_offer_code", offer_code, DbType.String)); 
            parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
            parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
            parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));

            DataSet ds = dbManager.execStoredProcedure(
                "pr_ipo_get_offermilestones",
                CommandType.StoredProcedure,
                parameters.ToArray()
            );

            return ds;
        }

        public DataTable SetOfferCategory(CategoryModel model, headerValue headerval, string constring)
        {
            DataTable result = new DataTable();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_action", model.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_json", model.json_data, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_offer_code", model.offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_client_code", model.client_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", model.user_code, DbType.String));

                parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", 0, DbType.Int32, ParameterDirection.Output));

                DataSet ds = dbManager.execStoredProcedure(
                    "pr_ipo_set_offercategory",
                    CommandType.StoredProcedure,
                    parameters.ToArray()
                );

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP: pr_ipo_set_offercategory | Error: " + ex.Message);
            }

            return result;
        }

        public DataSet GetOfferCategory(string offer_code, string client_code, headerValue headerval, string constring)
        {
            DataSet result = new DataSet();

            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();

                parameters.Add(dbManager.CreateParameter("in_offer_code", offer_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_client_code", client_code, DbType.String));

                DataSet ds = dbManager.execStoredProcedure(
                    "pr_ipo_get_offercategory",
                    CommandType.StoredProcedure,
                    parameters.ToArray()
                );

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds;
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP: pr_ipo_get_offercategory | Error: " + ex.Message);
            }

            return result;
        }

        public DataSet GetAuditTrailData(string ipo_code, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_ipo_code", ipo_code, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_ipo_get_auditlist", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_ipo_get_auditlist Error Message: " + ex.Message);
            }
            return ds;
        }

        // GetifscdetailsData

        public DataSet GetifscdetailsData(string ifsc_search, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_ifsc_search", ifsc_search, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_tifscdetails", CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_tifscdetails Error Message: " + ex.Message);
            }
            return ds;
        }

    }
}
