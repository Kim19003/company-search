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
            ////////////////////////////////////////////////////////////////////////
            // Error[001]: Only integers allowed as for some search box values
            // Error[002]: Error in checking the null string values
            // Error[003]: Error in the searching function
            // Error[004]: Error in the integer checking function (related to 001)
            // Error[005]: Error in the more details function
            // Error[006]: Search can't be done by only using the Company form box.
            // Error[007]: Error in the Company form value checking function (related to 006)
            ////////////////////////////////////////////////////////////////////////

            Session.Timeout = 60;

            // Retain the queries
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

            // Empty the session variable values
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
            Session["OneBoxSearch"] = false;

            // If something goes wrong with the search, get an error message.
            if (Session["SearchError"] != null)
            errorText1.Text = (string)Session["SearchError"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
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
                Response.Redirect("index.aspx");
            }
            else
            {
            if ((bool)Session["OneBoxSearch"] == true)
            {
                //errorText1.Text = "Error[006]: To make sure the exact search works, please insert more values.";
                  errorText1.Text = "Error[006]: You can't do the search with only using the Company form box.";
                  companyFormBox.Text = "";
            }
            else
            {
            if (Convert.ToString(Session["IsItNumber"]) == "yes" && Convert.ToString(Session["IsItNumber2"]) == "yes" || String.IsNullOrEmpty(earliestBox.Text) && Convert.ToString(Session["IsItNumber2"]) == "yes" || String.IsNullOrEmpty(latestBox.Text) && Convert.ToString(Session["IsItNumber"]) == "yes" || String.IsNullOrEmpty(earliestBox.Text) && String.IsNullOrEmpty(latestBox.Text) && (bool)Session["OneBoxSearch"] == false)
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
            }
            catch (Exception err)
            {
                errorText1.Text = "Error[002]: Something went horribly wrong.";
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
            try
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
                errorText1.Text = "Error[001]: Only integers allowed as the year inputs!";
            }
            }
            catch (Exception err)
            {
                errorText1.Text = "Error[004]: Something went horribly wrong.";
            }

        }

        protected void latestBox_TextChanged(object sender, EventArgs e)
        {
            try
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
                errorText1.Text = "Error[001]: Only integers allowed as the year inputs!";
            }
            }
            catch (Exception err)
            {
                errorText1.Text = "Error[004]: Something went horribly wrong.";
            }
        }

        protected void companySizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompanySize = companySizeBox.Text;
            Session["CompanySize"] = CompanySize;
        }

        protected void companyFormBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CompanyForm = companyFormBox.Text;
                if (!string.IsNullOrEmpty(CompanyForm) && string.IsNullOrEmpty((string)Session["BusinessSector"]) || string.IsNullOrEmpty((string)Session["Location"]) ||
                    string.IsNullOrEmpty((string)Session["CompanySize"]) || string.IsNullOrEmpty((string)Session["earliestDay"]) || string.IsNullOrEmpty((string)Session["earliestMonth"]) || Session["Earliest"] == "" ||
                    string.IsNullOrEmpty((string)Session["latestDay"]) || string.IsNullOrEmpty((string)Session["latestMonth"]) || Session["Latest"] == "")
                {
                    Session["OneBoxSearch"] = true;
                }
                else
                {
                    Session["CompanyForm"] = CompanyForm;

                    // Making sure the value stays false.
                    Session["OneBoxSearch"] = false;
                } 
            }
            catch (Exception err)
            {
                errorText1.Text = "Error[007]: Something went horribly wrong.";
            }
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