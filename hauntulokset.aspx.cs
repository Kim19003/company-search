﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Linq.Expressions;

namespace nettisivut_app
{
    public partial class hauntulokset : System.Web.UI.Page
    {
        public int viisikymmenta = 50;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            Session.Timeout = 60;
            Session["SearchError"] = null;
            Session["BusinessSector"] = Session["BusinessSector2"];

                // Convert to lower cases (example: "KULJETUS" -> "kuljetus")
                string ncs = (string)Session["BusinessSector2"];
            Session["BusinessSectorCS"] = ncs.ToLower();

                // Convert lowercase text to starting as capital letter (example: "kuljetus" -> "Kuljetus")
                string cap = (string)Session["BusinessSector2"];
            if (cap == null)
            cap = null;
            if (cap.Length > 1)
            Session["BusinessSectorCA"] = char.ToUpper(cap[0]) + cap.Substring(1);

                // Convert uppercase text to starting as capital letter, and continuing with lowercase letters (example: "KULJETUS" -> "Kuljetus")
                string upCap = (string)Session["BusinessSector2"];
                if (upCap == null)
                    upCap = null;
                if (upCap.Length > 1)
                {
                    string uplet = Convert.ToString(char.ToUpper(upCap[0]));
                    string lastpart = upCap.Substring(1);
                    string lcascont = lastpart.ToLower();
                    Session["BusinessSectorUCAP"] = uplet + lcascont;
                }

            bool firstWay = false;
            bool secondWay = false;
            bool thirdWay = false;
            bool fourthWay = false;

                if (string.IsNullOrEmpty((string)Session["earliestMonth"])
                    && string.IsNullOrEmpty((string)Session["earliestDay"])
                    && Session["Earliest"] == ""
                    && Session["Latest"] == ""
                    && string.IsNullOrEmpty((string)Session["latestMonth"])
                    && string.IsNullOrEmpty((string)Session["latestDay"]))
                {
                    Label3.Text = "";
                }
                else
                {
                    Session["earliestSearcher"] = (int)Session["Earliest"] + "-" + (string)Session["earliestMonth"] + "-" + (string)(Session["earliestDay"]);
                    Session["latestSearcher"] = (int)Session["Latest"] + "-" + (string)Session["latestMonth"] + "-" + (string)(Session["latestDay"]);
                    Label3.Text = (int)Session["Earliest"] + "-" + (string)Session["earliestMonth"] + "-" + (string)(Session["earliestDay"]) + " - " + (int)Session["Latest"] + "-" + (string)Session["latestMonth"] + "-" + (string)(Session["latestDay"]);
                }

                // The search program starts
                if (string.IsNullOrEmpty((string)Session["BusinessSector"])
                    && string.IsNullOrEmpty((string)Session["CompanyForm"])
                    && string.IsNullOrEmpty((string)Session["Location"])) // Fills empty variables with spam and gives "no results"
                {
                    Session["BusinessSector"] = "d1211313dd13";
                    Session["CompanyForm"] = "d1211313dd13";
                    Session["Location"] = "d1211313dd13";
                }
                if (Session["Earliest"] == "" || Session["Latest"] == "") // Searchs automatically with year 2020, if no timeline chosen
                {
                    string resultsFromTo = "https://avoindata.prh.fi/tr/v1?totalResults=false&maxResults=1000&companyRegistrationFrom=2020-01-01&companyRegistrationTo=2020-12-31";
                    Session["resultsFromToVar"] = resultsFromTo;
                }
                else
                {
                    string resultsFromTo = "https://avoindata.prh.fi/tr/v1?totalResults=false&maxResults=1000&companyRegistrationFrom=" + (string)Session["earliestSearcher"] + "&companyRegistrationTo=" + (string)Session["latestSearcher"];
                    Session["resultsFromToVar"] = resultsFromTo;
                }

                StringWriter writer = new StringWriter();
                WebRequest myRequest = WebRequest.Create((string)Session["resultsFromToVar"]);
                WebResponse response = myRequest.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromFile = reader.ReadToEnd();
                Session["responseFromFile"] = responseFromFile;
                // Session["haettuNimi"] = responseFromFile; // Tests if the program reads the whole page
                string[] words = responseFromFile.Split('}');

                // Check if it contains name without lowercase convert
                if (responseFromFile.Contains("\"name\":\"" + (string)Session["BusinessSector"])
                   && responseFromFile.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"])
                   && responseFromFile.Contains("\"businessId\":\"" + (string)Session["Location"]))
                    firstWay = true;
                if (firstWay)
                {
                    int i = 0;
                    while (!words[i].Contains("\"name\":\"" + (string)Session["BusinessSector"]) && i < words.Length
                        || !words[i].Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length
                        || !words[i].Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                    {
                        i++;
                    }
                    string[] companyData = words[i].Split(','); // Getting the data and splitting it

                    // Name
                    string haettuNimiRaw = companyData[2];
                    string haettuNimi = haettuNimiRaw.Substring(8);
                    string haettuNimiMinus = haettuNimi.Remove(haettuNimi.Length - 1);
                    // Registry date
                    string haettuRDRaw = companyData[3];
                    string haettuRD = haettuRDRaw.Substring(20);
                    string haettuRDMinus = haettuRD.Remove(haettuRD.Length - 1);
                    // Business Id
                    string haettuBIRaw = companyData[1];
                    string haettuBI = haettuBIRaw.Substring(15);
                    string haettuBIMinus = haettuBI.Remove(haettuBI.Length - 1);
                    // Company form
                    string haettuFormRaw = companyData[4];
                    string haettuForm = haettuFormRaw.Substring(15);
                    string haettuFormMinus = haettuForm.Remove(haettuForm.Length - 1);
                    // Company details
                    string haettuDetailsRaw = companyData[5];
                    string haettuDetails = haettuDetailsRaw.Substring(14);
                    string haettuDetailsMinus = haettuDetails.Remove(haettuDetails.Length - 1);

                    Session["haettuNimi"] = haettuNimiMinus; // Saves the name
                    Session["haettuRD"] = haettuRDMinus; // Saves the registry date
                    Session["haettuBI"] = haettuBIMinus; // Saves the business Id
                    Session["haettuForm"] = haettuFormMinus; // Saves the company form
                    Session["haettuDetails"] = haettuDetailsMinus; // Saves the "more details" -url

                    // Search results
                    Label11.Text = (string)Session["haettuNimi"]; // Name
                    Label13.Text = (string)Session["haettuRD"]; // Registry date
                    Label15.Text = (string)Session["haettuBI"]; // Business Id
                    Label18.Text = (string)Session["haettuForm"]; // Company form

            // Result amount calculator
            int tulostenMaara = 0;
                i = 0;
                    if (!string.IsNullOrEmpty((string)Session["BusinessSector"]) && string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"])) // Name
                    {
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSector"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["BusinessSector"]) && string.IsNullOrEmpty((string)Session["Location"])) // Company form
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length)
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSector"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Business Id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["BusinessSector"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Name and Business Id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSector"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSector"])) // Company form and Business id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["BusinessSector"])) // Company form and Name
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"name\":\"" + (string)Session["BusinessSector"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["BusinessSector"]) && !string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["CompanyForm"])) // All
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSector"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]) && str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    Label19.Text = "Number of results found: ";
                    Label20.Text = Convert.ToString(tulostenMaara);
                }


            // Check if it contains name with lowercase convert
            if (responseFromFile.Contains("\"name\":\"" + (string)Session["BusinessSectorCS"])
            && responseFromFile.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"])
            && responseFromFile.Contains("\"businessId\":\"" + (string)Session["Location"]))
                secondWay = true;
            if (secondWay)
            {
                int i = 0;
                while (!words[i].Contains("\"name\":\"" + (string)Session["BusinessSectorCS"]) && i < words.Length
                    || !words[i].Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length
                    || !words[i].Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                {
                    i++;
                }
                string[] companyData = words[i].Split(',');

                string haettuNimiRaw = companyData[2];
                string haettuNimi = haettuNimiRaw.Substring(8);
                string haettuNimiMinus = haettuNimi.Remove(haettuNimi.Length - 1);

                string haettuRDRaw = companyData[3];
                string haettuRD = haettuRDRaw.Substring(20);
                string haettuRDMinus = haettuRD.Remove(haettuRD.Length - 1);

                string haettuBIRaw = companyData[1];
                string haettuBI = haettuBIRaw.Substring(15);
                string haettuBIMinus = haettuBI.Remove(haettuBI.Length - 1);

                string haettuFormRaw = companyData[4];
                string haettuForm = haettuFormRaw.Substring(15);
                string haettuFormMinus = haettuForm.Remove(haettuForm.Length - 1);

                string haettuDetailsRaw = companyData[5];
                string haettuDetails = haettuDetailsRaw.Substring(14);
                string haettuDetailsMinus = haettuDetails.Remove(haettuDetails.Length - 1);

                Session["haettuNimi"] = haettuNimiMinus;
                Session["haettuRD"] = haettuRDMinus;
                Session["haettuBI"] = haettuBIMinus;
                Session["haettuForm"] = haettuFormMinus;
                Session["haettuDetails"] = haettuDetailsMinus;

                // Search results
                Label11.Text = (string)Session["haettuNimi"];
                Label13.Text = (string)Session["haettuRD"];
                Label15.Text = (string)Session["haettuBI"];
                Label18.Text = (string)Session["haettuForm"];

                // Result amount calculator
                int tulostenMaara = 0;
                i = 0;
                if (!string.IsNullOrEmpty((string)Session["BusinessSectorCS"]) && string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"])) // Name
                {
                    foreach (string str in words)
                    {
                        if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorCS"]))
                        {
                            tulostenMaara++;
                        }
                    }
                }
                if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["BusinessSectorCS"]) && string.IsNullOrEmpty((string)Session["Location"])) // Company form
                {
                    tulostenMaara = 0;
                    foreach (string str in words)
                    {
                        if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length)
                        {
                            tulostenMaara++;
                        }
                    }
                }
                if (!string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSectorCS"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Business Id
                {
                    tulostenMaara = 0;
                    foreach (string str in words)
                    {
                        if (str.Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                        {
                            tulostenMaara++;
                        }
                    }
                }
                if (!string.IsNullOrEmpty((string)Session["BusinessSectorCS"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Name and Business Id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorCS"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSectorCS"])) // Company form and Business id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["BusinessSectorCS"])) // Company form and Name
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"name\":\"" + (string)Session["BusinessSectorCS"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["BusinessSectorCS"]) && !string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["CompanyForm"])) // All
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorCS"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]) && str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    Label19.Text = "Number of results found: ";
                Label20.Text = Convert.ToString(tulostenMaara);
            }

                // Check if it contains name starting with capital letter
                if (responseFromFile.Contains("\"name\":\"" + (string)Session["BusinessSectorCA"])
                   && responseFromFile.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"])
                   && responseFromFile.Contains("\"businessId\":\"" + (string)Session["Location"]))
                    thirdWay = true;
                if (thirdWay)
                {
                    int i = 0;
                    while (!words[i].Contains("\"name\":\"" + (string)Session["BusinessSectorCA"]) && i < words.Length
                        || !words[i].Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length
                        || !words[i].Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                    {
                        i++;
                    }
                    string[] companyData = words[i].Split(','); // Getting the data and splitting it

                    // Name
                    string haettuNimiRaw = companyData[2];
                    string haettuNimi = haettuNimiRaw.Substring(8);
                    string haettuNimiMinus = haettuNimi.Remove(haettuNimi.Length - 1);
                    // Registry date
                    string haettuRDRaw = companyData[3];
                    string haettuRD = haettuRDRaw.Substring(20);
                    string haettuRDMinus = haettuRD.Remove(haettuRD.Length - 1);
                    // Business Id
                    string haettuBIRaw = companyData[1];
                    string haettuBI = haettuBIRaw.Substring(15);
                    string haettuBIMinus = haettuBI.Remove(haettuBI.Length - 1);
                    // Company form
                    string haettuFormRaw = companyData[4];
                    string haettuForm = haettuFormRaw.Substring(15);
                    string haettuFormMinus = haettuForm.Remove(haettuForm.Length - 1);
                    // Company details
                    string haettuDetailsRaw = companyData[5];
                    string haettuDetails = haettuDetailsRaw.Substring(14);
                    string haettuDetailsMinus = haettuDetails.Remove(haettuDetails.Length - 1);

                    Session["haettuNimi"] = haettuNimiMinus; // Saves the name
                    Session["haettuRD"] = haettuRDMinus; // Saves the registry date
                    Session["haettuBI"] = haettuBIMinus; // Saves the business Id
                    Session["haettuForm"] = haettuFormMinus; // Saves the company form
                    Session["haettuDetails"] = haettuDetailsMinus; // Saves the "more details" -url

                    // Search results
                    Label11.Text = (string)Session["haettuNimi"]; // Name
                    Label13.Text = (string)Session["haettuRD"]; // Registry date
                    Label15.Text = (string)Session["haettuBI"]; // Business Id
                    Label18.Text = (string)Session["haettuForm"]; // Company form

                    // Result amount calculator
                    int tulostenMaara = 0;
                    i = 0;
                    if (!string.IsNullOrEmpty((string)Session["BusinessSectorCA"]) && string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"])) // Name
                    {
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorCA"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["BusinessSectorCA"]) && string.IsNullOrEmpty((string)Session["Location"])) // Company form
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length)
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSectorCA"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Business Id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["BusinessSectorCA"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Name and Business Id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorCA"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSectorCA"])) // Company form and Business id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["BusinessSectorCA"])) // Company form and Name
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"name\":\"" + (string)Session["BusinessSectorCA"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["BusinessSectorCA"]) && !string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["CompanyForm"])) // All
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorCA"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]) && str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    Label19.Text = "Number of results found: ";
                    Label20.Text = Convert.ToString(tulostenMaara);
                }

                // Check if it contains name with capital + lowercase convert
                if (responseFromFile.Contains("\"name\":\"" + (string)Session["BusinessSectorUCAP"])
                   && responseFromFile.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"])
                   && responseFromFile.Contains("\"businessId\":\"" + (string)Session["Location"]))
                    fourthWay = true;
                if (fourthWay)
                {
                    int i = 0;
                    while (!words[i].Contains("\"name\":\"" + (string)Session["BusinessSectorUCAP"]) && i < words.Length
                        || !words[i].Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length
                        || !words[i].Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                    {
                        i++;
                    }
                    string[] companyData = words[i].Split(','); // Getting the data and splitting it

                    // Name
                    string haettuNimiRaw = companyData[2];
                    string haettuNimi = haettuNimiRaw.Substring(8);
                    string haettuNimiMinus = haettuNimi.Remove(haettuNimi.Length - 1);
                    // Registry date
                    string haettuRDRaw = companyData[3];
                    string haettuRD = haettuRDRaw.Substring(20);
                    string haettuRDMinus = haettuRD.Remove(haettuRD.Length - 1);
                    // Business Id
                    string haettuBIRaw = companyData[1];
                    string haettuBI = haettuBIRaw.Substring(15);
                    string haettuBIMinus = haettuBI.Remove(haettuBI.Length - 1);
                    // Company form
                    string haettuFormRaw = companyData[4];
                    string haettuForm = haettuFormRaw.Substring(15);
                    string haettuFormMinus = haettuForm.Remove(haettuForm.Length - 1);
                    // Company details
                    string haettuDetailsRaw = companyData[5];
                    string haettuDetails = haettuDetailsRaw.Substring(14);
                    string haettuDetailsMinus = haettuDetails.Remove(haettuDetails.Length - 1);

                    Session["haettuNimi"] = haettuNimiMinus; // Saves the name
                    Session["haettuRD"] = haettuRDMinus; // Saves the registry date
                    Session["haettuBI"] = haettuBIMinus; // Saves the business Id
                    Session["haettuForm"] = haettuFormMinus; // Saves the company form
                    Session["haettuDetails"] = haettuDetailsMinus; // Saves the "more details" -url

                    // Search results
                    Label11.Text = (string)Session["haettuNimi"]; // Name
                    Label13.Text = (string)Session["haettuRD"]; // Registry date
                    Label15.Text = (string)Session["haettuBI"]; // Business Id
                    Label18.Text = (string)Session["haettuForm"]; // Company form

                    // Result amount calculator
                    int tulostenMaara = 0;
                    i = 0;
                    if (!string.IsNullOrEmpty((string)Session["BusinessSectorUCAP"]) && string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"])) // Name
                    {
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorUCAP"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["BusinessSectorUCAP"]) && string.IsNullOrEmpty((string)Session["Location"])) // Company form
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length)
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSectorUCAP"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Business Id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["BusinessSectorUCAP"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["CompanyForm"])) // Name and Business Id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorUCAP"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && !string.IsNullOrEmpty((string)Session["Location"]) && string.IsNullOrEmpty((string)Session["BusinessSectorUCAP"])) // Company form and Business id
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["CompanyForm"]) && string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["BusinessSectorUCAP"])) // Company form and Name
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && str.Contains("\"name\":\"" + (string)Session["BusinessSectorUCAP"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty((string)Session["BusinessSectorUCAP"]) && !string.IsNullOrEmpty((string)Session["Location"]) && !string.IsNullOrEmpty((string)Session["CompanyForm"])) // All
                    {
                        tulostenMaara = 0;
                        foreach (string str in words)
                        {
                            if (str.Contains("\"name\":\"" + (string)Session["BusinessSectorUCAP"]) && str.Contains("\"businessId\":\"" + (string)Session["Location"]) && str.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]))
                            {
                                tulostenMaara++;
                            }
                        }
                    }
                    Label19.Text = "Number of results found: ";
                    Label20.Text = Convert.ToString(tulostenMaara);
                }

                if (firstWay == false && secondWay == false && thirdWay == false && fourthWay == false)
                    {
                        Label11.Font.Bold = false;
                        Label11.Text = "No results found 😔";
                        Label12.Text = "";
                        Label14.Text = "";
                        Label16.Text = "";
                        //
                        Button1.Text = "";
                        Button1.Visible = false;
                        //
                        Label13.Text = "";
                        Label15.Text = "";
                        Label18.Text = "";
                    }
            // Search program ends here

            if (Session["BusinessSector"] == "d1211313dd13")
            {
                Label1.Text = "";
            }
            else
            {
                Label1.Text = (string)Session["BusinessSector"];
            }
            if (Session["Location"] == "d1211313dd13")
            {
                Label2.Text = "";
            }
            else
            {
                Label2.Text = (string)Session["Location"];
            }
            if (Session["CompanyForm"] == "d1211313dd13")
            {
                Label5.Text = "";
            }
            else
            {
                Label5.Text = (string)Session["CompanyForm"];
            }
            }
            catch (Exception err)
            {
                Session["SearchError"] = "Error creating web request.";
                Response.Redirect("index.aspx");
            }
        }  

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("lisatiedot.aspx");
        }
    }
}