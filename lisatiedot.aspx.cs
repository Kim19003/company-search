using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

namespace nettisivut_app
{
    public partial class lisatiedot : System.Web.UI.Page
    {
        public int viisikymmenta = 50;
    protected void Page_Load(object sender, EventArgs e)
        {
            StringWriter writer2 = new StringWriter();
            WebRequest myRequest2 = WebRequest.Create((string)Session["haettuDetails"]);
            WebResponse response2 = myRequest2.GetResponse();
            Stream dataStream2 = response2.GetResponseStream();
            StreamReader reader2 = new StreamReader(dataStream2);
            string responseFromFile3 = reader2.ReadToEnd();
            Session["responseFromFile2"] = responseFromFile3;
            string responseFromFile4 = (string)Session["responseFromFile2"];

            string[] words2 = responseFromFile4.Split('}');
            int i2 = 0;
            while (!words2[i2].Contains("\"street\"") && i2 < words2.Length)
            {
                i2++;
            }
            string[] companyData2 = words2[i2].Split(',');

            //street
            string haettuStreetRaw = companyData2[1];
            string haettuStreet = haettuStreetRaw.Substring(24);
            string haettuStreetMinus = haettuStreet.Remove(haettuStreet.Length - 1);

            //city
            string haettuCityRaw = companyData2[4];
            string haettuCity = haettuCityRaw.Substring(8);
            string haettuCityMinus = haettuCity.Remove(haettuCity.Length - 1);

            //postcode
            string haettuPCRaw = companyData2[2];
            string haettuPC = haettuPCRaw.Substring(12);
            string haettuPCMinus = haettuPC.Remove(haettuPC.Length - 1);

            //country
            string haettuCountryRaw = companyData2[5];
            string haettuCountry = haettuCountryRaw.Substring(11);
            string haettuCountryMinus = haettuCountry.Remove(haettuCountry.Length - 1);

            //phone
            string haettuPhoneRaw = companyData2[7];
            string haettuPhone = haettuPhoneRaw.Substring(8);
            //string haettuPhoneMinus = haettuPhone.Remove(haettuPhone.Length - 1);

            //website
            string haettuWSRaw = companyData2[6];
            string haettuWS = haettuWSRaw.Substring(10);
            //string haettuWSMinus = haettuWS.Remove(haettuWS.Length - 1);


            //Session["haettuStreet"] = companyData2[1];
            if (haettuStreetMinus == "null")
            {
                Session["haettuStreet"] = "-";
            }
            else
            {
                haettuStreetMinus = haettuStreetMinus.Replace("\"", "");
                Session["haettuStreet"] = haettuStreetMinus;
            }

            if (haettuCityMinus == "null")
            {
                Session["haettuCity"] = "-";
            }
                else
                {
                haettuCityMinus = haettuCityMinus.Replace("\"", "");
                Session["haettuCity"] = haettuCityMinus;
                }

            if (haettuPCMinus == "null")
            {
                Session["haettuPC"] = "-";
            }
            else
            {
                haettuPCMinus = haettuPCMinus.Replace("\"", "");
                Session["haettuPC"] = haettuPCMinus;
            }

            if (haettuCountryMinus == "null")
            {
                Session["haettuCountry"] = "-";
            }
            else
            {
                haettuCountryMinus = haettuCountryMinus.Replace("\"", "");
                Session["haettuCountry"] = haettuCountryMinus;
            }

            if (haettuPhone == "null" || haettuPhone == "\"null\"")
            {
                Session["haettuPhone"] = "-";
            }
            else
            {
                haettuPhone = haettuPhone.Replace("\"", "");
                Session["haettuPhone"] = haettuPhone;
            }
            //Session["haettuPhone"] = haettuPhoneMinus;

            if (haettuWS == "null" || haettuWS == "\"null\"")
            {
                Session["haettuWS"] = "-";
            }
            else
            {
                haettuWS = haettuWS.Replace("\"", "");
                Session["haettuWS"] = haettuWS;
            }
            //Session["haettuWS"] = haettuWSMinus;

            /* int qwe = 0;
            for (int asdasd = 0; asdasd < companyData2.Length; asdasd++)
            {
                Response.Write(companyData2[qwe]);
                qwe++;
            } */

        }
    }
}