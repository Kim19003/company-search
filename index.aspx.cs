using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Xml;

namespace nettisivut_app
{
    public partial class Index : System.Web.UI.Page
    {
        public int viisikymmenta = 50;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            // Session["BusinessSector"] = "Pirkanmaan Pienkoti"; //kokeilukoodi valmiilla inputilla
            Session["BusinessSector"] = businessSectorBox.Text; //business sector variable on yrityksen nimi nykyään
            Session["Location"] = locationBox.Text; //location variable on business id nykyään
            Session["Earliest"] = earliestBox.Text;
            Session["Latest"] = latestBox.Text;
            Session["CompanySize"] = companySizeBox.Text;
            Session["CompanyForm"] = companyFormBox.Text;
            Session["earliestMonth"] = theMonth.Text;
            Session["latestMonth"] = theMonth2.Text;
            Session["earliestDay"] = theDay.Text;
            Session["latestDay"] = theDay2.Text;
            Session["responseFromFile"] = "";
            Session["responseFromFile2"] = "";
            Session["haettuNimi"] = "";
            Session["haettuRD"] = "";
            Session["haettuBI"] = "";
            Session["haettuForm"] = "";
            Session["haettuDetails"] = "";
            Session["tulostenMaara"] = "";
            Session["RegTime"] = "";
            Session["earliestSearcher"] = "";
            Session["latestSearcher"] = "";
            Session["haettuStreet"] = "";
            Session["haettuCity"] = "";
            Session["haettuPC"] = "";
            Session["haettuCountry"] = "";
            Session["haettuPhone"] = "";
            Session["haettuWS"] = "";


            //hakuohjelma
            /* StringWriter writer = new StringWriter();
            WebRequest myRequest = WebRequest.Create(@"https://avoindata.prh.fi/tr/v1?totalResults=false&maxResults=1000&companyRegistrationFrom=2020-01-01&companyRegistrationTo=2020-12-31");
            WebResponse response = myRequest.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromFile = reader.ReadToEnd();
            Session["responseFromFile"] = responseFromFile; */

            // Session["haettuNimi"] = responseFromFile; //testaa että ohjelma lukee koko sivun

            /* string[] words = responseFromFile.Split('}');
            int i = 0;
            while (!words[i].Contains((string)Session["BusinessSector"]) && i < words.Length) //laskee monta kertaa tietty hakusana on löytynyt
            {
                i++;
            }
            string[] companyData = words[i].Split(','); //tiedon noutaminen + sanojen splittaus

            //1 on business id, 2 on name jne...

            //name
            string haettuNimiRaw = companyData[2];
            string haettuNimi = haettuNimiRaw.Substring(8);
            string haettuNimiMinus = haettuNimi.Remove(haettuNimi.Length - 1);
            //registry date
            string haettuRDRaw = companyData[3];
            string haettuRD = haettuRDRaw.Substring(20);
            string haettuRDMinus = haettuRD.Remove(haettuRD.Length - 1);
            //business id
            string haettuBIRaw = companyData[1];
            string haettuBI = haettuBIRaw.Substring(15);
            string haettuBIMinus = haettuBI.Remove(haettuBI.Length - 1);
            //company form
            string haettuFormRaw = companyData[4];
            string haettuForm = haettuFormRaw.Substring(15);
            string haettuFormMinus = haettuForm.Remove(haettuForm.Length - 1);

            Session["haettuNimi"] = haettuNimiMinus; //tallentaa nimen
            Session["haettuRD"] = haettuRDMinus; //tallentaa registration daten
            Session["haettuBI"] = haettuBIMinus; //tallentaa business idn
            Session["haettuForm"] = haettuFormMinus; //tallentaa company formin */
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["earliestDay"]) && string.IsNullOrEmpty((string)Session["earliestMonth"]) && Session["Earliest"] != ""
                || string.IsNullOrEmpty((string)Session["earliestDay"]) && Session["Earliest"] == "" && (string)Session["earliestMonth"] != ""
                || string.IsNullOrEmpty((string)Session["earliestMonth"]) && Session["Earliest"] == "" && (string)Session["earliestDay"] != ""
                //
                || string.IsNullOrEmpty((string)Session["latestDay"]) && string.IsNullOrEmpty((string)Session["latestMonth"]) && Session["Latest"] != ""
                || string.IsNullOrEmpty((string)Session["latestDay"]) && Session["Latest"] == "" && (string)Session["latestMonth"] != ""
                || string.IsNullOrEmpty((string)Session["latestMonth"]) && Session["Latest"] == "" && (string)Session["latestDay"] != ""
                //
                || (string)Session["latestMonth"] != "" && Session["Latest"] != "" && (string)Session["latestDay"] != ""
                && string.IsNullOrEmpty((string)Session["earliestDay"]) && string.IsNullOrEmpty((string)Session["earliestMonth"]) && Session["Earliest"] == ""
                //
                || (string)Session["earliestMonth"] != "" && Session["Earliest"] != "" && (string)Session["earliestDay"] != ""
                && string.IsNullOrEmpty((string)Session["latestDay"]) && string.IsNullOrEmpty((string)Session["latestMonth"]) && Session["Latest"] == "")
            {
                Response.Redirect("index.aspx"); //lataa sivun uudelleen ettei crashaa
            }
            else
            {
            if (Convert.ToString(Session["IsItNumber"]) == "yes" && Convert.ToString(Session["IsItNumber2"]) == "yes" || String.IsNullOrEmpty(earliestBox.Text) && Convert.ToString(Session["IsItNumber2"]) == "yes" || String.IsNullOrEmpty(latestBox.Text) && Convert.ToString(Session["IsItNumber"]) == "yes" || String.IsNullOrEmpty(earliestBox.Text) && String.IsNullOrEmpty(latestBox.Text))
            {
                errorText1.Text = "";
                Response.Redirect("hauntulokset.aspx");
                }
            else
            {
                earliestBox.Text = "";
                latestBox.Text = "";
            }
            }
        }

        public void businessSectorBox_TextChanged(object sender, EventArgs e)
        {
            string BusinessSector = businessSectorBox.Text;
            Session["BusinessSector"] = BusinessSector;
        }

        protected void locationBox_TextChanged(object sender, EventArgs e)
        {
            string Location = locationBox.Text;
            Session["Location"] = Location;
        }

        protected void earliestBox_TextChanged(object sender, EventArgs e)
        {
            int Earliest;
            bool isNumeric = int.TryParse(earliestBox.Text, out Earliest);
            if (isNumeric)
            {
                Session["IsItNumber"] = "yes";
                Earliest = Convert.ToInt32(earliestBox.Text);
                Session["Earliest"] = Earliest;
            }
            else
            {
                Session["IsItNumber"] = "no";
                errorText1.Text = "ERROR: Only integers allowed as the year inputs!";
            }
            
        }

        protected void latestBox_TextChanged(object sender, EventArgs e)
        {
            int Latest;
            bool isNumeric2 = int.TryParse(latestBox.Text, out Latest);
            if (isNumeric2)
            {
                Session["IsItNumber2"] = "yes";
                Latest = Convert.ToInt32(latestBox.Text);
                Session["Latest"] = Latest;
            }
            else
            {
                Session["IsItNumber2"] = "no";
                errorText1.Text = "ERROR: Only integers allowed as the year inputs!";
            }
        }

        protected void companySizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompanySize = companySizeBox.Text;
            Session["CompanySize"] = CompanySize;
        }

        protected void companyFormBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompanyForm = companyFormBox.Text;
            Session["CompanyForm"] = CompanyForm;
        }
        protected void theMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string earliestMonth = theMonth.Text;
            Session["earliestMonth"] = earliestMonth;
        }
        protected void theMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string latestMonth = theMonth2.Text;
            Session["latestMonth"] = latestMonth;
        }
        protected void theDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            string earliestDay = theDay.Text;
            Session["earliestDay"] = earliestDay;
        }
        protected void theDay2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string latestDay = theDay2.Text;
            Session["latestDay"] = latestDay;
        }
    }
    }