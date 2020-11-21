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

            // Retain the queries
            Session["BusinessSector2"] = businessSectorBox.Text; // "BusinessSector" is Company name now
            Session["Location"] = locationBox.Text; // "Location" is Business Id now
            Session["Earliest"] = earliestBox.Text;
            Session["Latest"] = latestBox.Text;
            Session["CompanyForm"] = companyFormBox.Text;
            Session["earliestMonth"] = theMonth.Text;
            Session["latestMonth"] = theMonth2.Text;
            Session["earliestDay"] = theDay.Text;
            Session["latestDay"] = theDay2.Text;

            // Empty or reset the session variable values
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
            Session["BusinessSector"] = "";
            Session["BusinessSectorCS"] = "";

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
                errorText1.Text = "";
                Response.Redirect("hauntulokset.aspx");
            }
            }
            catch (Exception err)
            {
                errorText1.Text = "Something went horribly wrong.";
            }
        }

        public void businessSectorBox_TextChanged(object sender, EventArgs e)
        {
            string BusinessSector = businessSectorBox.Text;
            Session["BusinessSector2"] = BusinessSector;
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
                int Earliest = Convert.ToInt32(earliestBox.Text);
                Session["Earliest"] = Earliest;
            }
            catch (Exception err)
            {
                errorText1.Text = "Unexpected error in the second \"Year\" -box.";
            }
        }

        protected void latestBox_TextChanged(object sender, EventArgs e)
        {
            try {
                int Latest = Convert.ToInt32(latestBox.Text);
                Session["Latest"] = Latest;
        }
            catch (Exception err)
            {
                errorText1.Text = "Unexpected error in the first \"Year\" -box.";
            }
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