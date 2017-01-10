using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using SherWoodFeePortal.DTO;

namespace SherWoodFeePortal.BLL
{
    public class Utility
    {
        public static string GetSafestring(object obj)
        {
            if (obj != null)
            {
                return Convert.ToString(obj);
            }
            else
            {
                return "";
            }
        }

        public static decimal GetSafeint(object obj) 
        { 
            if (obj != null) 
            { 
                decimal num = 0; 
                num = Math.Round((decimal)obj, 2, MidpointRounding.AwayFromZero); 
                decimal answer = num; 
                return answer; 
            } 
            else 
            { 
                return 0; 
            } 
        }

        public static void  SendEmail(PaymentBasket objPayBasket, string strMsg)
        {
            bool isSent = false;
            string UserId;
            string Password;
            string ConfigEmailAddressfrom;
            string ConfigEmailAddressto;
           
            try
            {                        
                UserId = ConfigurationManager.AppSettings["mailuserid"];
                Password = ConfigurationManager.AppSettings["mailuserpassword"];

                ConfigEmailAddressfrom = ConfigurationManager.AppSettings["emailfrom"];
                ConfigEmailAddressto =  ConfigurationManager.AppSettings["emailto"];
                ConfigEmailAddressto = ConfigEmailAddressto + "," + objPayBasket.fatheremail;

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(UserId, Password);
                //MailMessage mm = new MailMessage("neeraj.login@gmail.com", "neg@amadeus.co.in,ask@amadeus.co.in", ConfigurationManager.AppSettings["subject"].ToString() + " " + strMsg, MailTemplate);
                MailMessage mm = new MailMessage(ConfigEmailAddressfrom, ConfigEmailAddressto, ConfigurationManager.AppSettings["subject"].ToString() + " - " + strMsg, objPayBasket.mailtemplate);

                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
           
                try
                {
                    client.Send(mm);
                }
                catch (Exception Ex)
                {}
            }
            catch (Exception Ex1)
            {}
        }

        public static string NumberToText(int number, bool isUK)
        {
            if (number == 0) return "Zero";
            string and = isUK ? "and " : ""; // deals with UK or US numbering
            if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
            "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
            "Six Hundred " + and + "Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Million ", "Billion " };
            num[0] = number % 1000;           // units
            num[1] = number / 1000;
            num[2] = number / 1000000;
            num[1] = num[1] - 1000 * num[2];  // thousands
            num[3] = number / 1000000000;     // billions
            num[2] = num[2] - 1000 * num[3];  // millions
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10;              // ones
                t = num[i] / 10;
                h = num[i] / 100;             // hundreds
                t = t - 10 * h;               // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append(and);
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
    }
}