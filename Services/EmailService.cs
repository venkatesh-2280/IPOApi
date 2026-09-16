using IPOApi.STADataAccess;
using MimeKit;
using System.Data;
using MailKit.Net.Smtp;
using System.Net.Mail;

namespace IPOApi.Services
{
    public class EmailService
    {
        private readonly EmailData _emailData;
        private readonly IConfiguration _config;

        public EmailService(EmailData emailData, IConfiguration config)
        {
            _emailData = emailData;
            _config = config;
        }
        public DataTable SendIpoEmailsService(string offer_code, string constring)
        {
            DataTable ds = new DataTable();
            try
            {
                EmailData objDS = new EmailData();
                ds = objDS.SendIpoEmailsData(offer_code, constring);
            }
            catch (Exception e)
            { }
            return ds;
        }

        public async Task<string> ProcessBulkEmails(string offerCode, string constring)
        {
            DataTable dt = _emailData.SendIpoEmailsData(offerCode,constring);

            foreach (DataRow row in dt.Rows)
            {
                int gid = Convert.ToInt32(row["email_log_gid"]);
                string email = row["email_id"].ToString();
                try
                {
                    await SendMail(row);
                    _emailData.UpdateEmailStatus(offerCode,gid, "Y", "",0,"",constring);
                }
                catch (Exception ex)
                {
                    _emailData.UpdateEmailStatus( offerCode, gid,  "N", ex.Message,0,"",constring);
                }
            }
            return "Emails Processed Successfully";
        }

        private async Task SendMail(DataRow row)
        {
            string smtpUser = "noreplystaportal@gnsaindia.com";
            string smtpPass = "egut evok eymk ugab";

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("IPO Registrar", smtpUser));

            message.To.Add(
                new MailboxAddress(
                    row["investor_name"].ToString(),
                    row["email_id"].ToString()
                ));

            message.Subject = "IPO Allotment Intimation";

            // HTML BODY
            string body = GenerateHtmlBody(row);

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();

            await client.ConnectAsync(
                "smtp.gmail.com",
                587,
                false
            );

            await client.AuthenticateAsync(
                smtpUser,
                smtpPass
            );

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
        private string GenerateHtmlBody(DataRow row) {
            decimal sharedPrice = Convert.ToDecimal(row["shared_applied_price"]);
            int allottedQty = Convert.ToInt32(row["alloted_quantity"]);
            decimal amount_adjusted = sharedPrice * allottedQty;
            string status = row["allotment_status_flag"].ToString() == "Y" ? "Successful Allotment - " + allottedQty + "Shares" : "Regret - Un-successful allotment due to over-subscription"; 
            return $@" <html> <body style='font-family: Arial, Helvetica, sans-serif; font-size:14px; color:#000000;'> 
                                <p> Dear Investor, </p> 
                                <p> Greetings!! </p> 
                                <p> This is with reference to the application made by you in the public issue of <b>{row["client_name"]}</b> (""Company"" or ""Issuer""). Please find the allotment cum unblocking details as given below: </p> 
                                <table cellpadding='0' cellspacing='0' border='1' width='100%' style='border-collapse:collapse; border:1px solid #000;'> 
                                <!-- MAIN HEADER --> 
                                <tr style='background-color:#0057A8; color:white; font-weight:bold; text-align:center;'> 
                                <td colspan='4' style='padding:8px;'> {row["client_name"]} </td> 
                                </tr> 
                                <!-- SUB HEADER --> 
                                <tr style='background-color:#0C7DC0; color:white; font-weight:bold;'> 
                                <td colspan='4' style='padding:6px;'> Allotment advice cum Unblocking Intimation </td> 
                                </tr> 
                                <!-- ROW 1 --> 
                                <tr> 
                                <td style='padding:6px; width:22%;'> Bid cum Application Form No. </td> 
                                <td style='padding:6px; width:28%;'> {row["application_no"]} </td> 
                                <td style='padding:6px; width:22%;'> RTA Reference No. </td> 
                                <td style='padding:6px; width:28%;'> {row["reference_no"]} </td> 
                                </tr> 
                                <!-- ROW 2 --> 
                                <tr> 
                                <td style='padding:6px;'> DP ID Client ID </td> 
                                <td style='padding:6px;'> {row["dp_id"]} - {row["client_id"]} </td> 
                                <td style='padding:6px;'> Issue Price per Security (Rs.) </td> 
                                <td style='padding:6px;'> - </td> 
                                </tr> 
                                <!-- ROW 3 --> 
                                <tr> 
                                <td style='padding:6px;'> Sole/ First Applicant Name </td> 
                                <td colspan='3' style='padding:6px;'> {row["investor_name"]} </td> 
                                </tr> 
                                <!-- INVESTMENT HEADER --> 
                                <tr style='background-color:#0C7DC0; color:white; font-weight:bold; text-align:center;'> 
                                <td colspan='2' style='padding:6px;'> Investment Particulars </td> 
                                <td colspan='2' style='padding:6px;'> Allotment Particulars </td> 
                                </tr> 
                                <!-- INVESTMENT ROW 1 --> 
                                <tr> 
                                <td style='padding:6px;'> Shares Applied For </td> 
                                <td style='padding:6px;'> {row["quantity"]} </td> 
                                <td style='padding:6px;'> Shares Allotted </td> 
                                <td style='padding:6px;'> {row["alloted_quantity"]} </td> 
                                </tr> 
                                <!-- INVESTMENT ROW 2 --> 
                                <tr> 
                                <td style='padding:6px;'> Amount Invested (Rs.) </td> 
                                <td style='padding:6px;'> {row["total_amount"]} </td> 
                                <td style='padding:6px;'> Amount adjusted towards allotment (Rs.) </td> 
                                <td style='padding:6px;'> {amount_adjusted} </td> 
                                </tr> 
                                <!-- INVESTMENT ROW 3 --> 
                                <tr> 
                                <td style='padding:6px;'> Mode of Investment </td> 
                                <td style='padding:6px;'> UPI </td> 
                                <td style='padding:6px;'> Amount Unblocked, If any (Rs.) </td> 
                                <td style='padding:6px;'> - </td> 
                                </tr> 
                                <!-- BALANCE HEADER --> 
                                <tr style='background-color:#0C7DC0; color:white; font-weight:bold;'> 
                                <td colspan='4' style='padding:6px;'> Balance Amount, if any post allotment, being unblocked by your SCSB (ASBA Banker) / UPI </td> 
                                </tr> 
                                <!-- DATE ROW --> 
                                <tr> 
                                <td style='padding:6px;'> Date of Unblock </td> 
                                <td style='padding:6px;'> - </td> 
                                <td style='padding:6px;'> Expected Date of Listing </td> 
                                <td style='padding:6px;'> - </td> 
                                </tr> 
                                <!-- STATUS ROW --> 
                                <tr> 
                                <td style='padding:6px;'> Status </td> 
                                <td colspan='3' style='padding:6px; font-weight:bold;'> {status} </td> 
                                </tr> 
                                </table> 
                                <br/> 
                                <p style='font-size:13px;'> Kindly verify the above. In case the securities have not been credited to your beneficiary A/C (DPID and Client ID) or amount to be unblocked, if any, has not been unblocked in your bank account, please contact us/mail to us per the details given below. </p> 
                                <br/> 
                                <p> <b>This is a system generated email. Please do not reply.</b> </p> 
                                </body> 
                                </html>"; 
        }

        // getemailListService
        public DataSet getemailListService(string offer_code, string in_action, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                EmailData objDS = new EmailData();
                ds = objDS.getemailListData(offer_code, in_action, constring);
                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                int gid = Convert.ToInt32(row["email_log_gid"]);
                                int entlid = Convert.ToInt32(row["entitlement_id"]);

                                // Call UpdateEmailStatus
                                objDS.UpdateEmailStatus(
                                    offer_code,
                                    gid,
                                    "Y",
                                    "",
                                   entlid,
                                    in_action,
                                    constring);
                            }
                        }
            }
            catch (Exception e)
            { }
            return ds;
        }

        //GetbidfilecountsummaryService
        public DataSet GetbidfilecountsummaryService(string ipo_code, string constring)
        {
            DataSet ds = new DataSet();
            try
            {
                EmailData objDS = new EmailData();
                ds = objDS.GetbidfilecountsummaryData(ipo_code, constring);
            }
            catch (Exception e)
            { }
            return ds;
        }

    }
}
