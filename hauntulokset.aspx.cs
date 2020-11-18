using System;
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
            Session.Timeout = 60;
            Session["SearchError"] = null;
            try
            {
                if (string.IsNullOrEmpty((string)Session["earliestMonth"])
                    && string.IsNullOrEmpty((string)Session["earliestDay"])
                    && Session["Earliest"] == ""
                    //&& string.IsNullOrEmpty((string)Session["Earliest"])
                    && Session["Latest"] == ""
                    //&& string.IsNullOrEmpty((string)Session["Latest"])
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
                    //Label3.Text = (string)Session["earliestDay"] + "-" + (string)Session["earliestMonth"] + "-" + Convert.ToString(Session["Earliest"]) + " - " + (string)Session["latestDay"] + "-" + (string)Session["latestMonth"] + "-" + Convert.ToString(Session["Latest"]);
                }

                //***hakuohjelma alkaa (nimi käyttää BusinessSector variablea)***
                if (string.IsNullOrEmpty((string)Session["BusinessSector"])
                    && string.IsNullOrEmpty((string)Session["CompanyForm"])
                    && string.IsNullOrEmpty((string)Session["Location"])) //täyttää variablet spämmillä jos tyhjät (antaa no resultsin)
                {
                    Session["BusinessSector"] = "d1211313dd13";
                    Session["CompanyForm"] = "d1211313dd13";
                    Session["Location"] = "d1211313dd13";
                }
                if (Session["Earliest"] == "" || Session["Latest"] == "") //hakee automaattisesti 2020 vuodelta, jos foundation timeä ei asetettu
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
                string responseFromFile2 = reader.ReadToEnd();
                Session["responseFromFile"] = responseFromFile2;
                string responseFromFile = (string)Session["responseFromFile"];
                // Session["haettuNimi"] = responseFromFile; //testaa että ohjelma lukee koko sivun
                string[] words = responseFromFile.Split('}');
                if (responseFromFile.Contains("\"name\":\"" + (string)Session["BusinessSector"]) //kaatumisen ohittaminen
                   && responseFromFile.Contains("\"companyForm\":\"" + (string)Session["CompanyForm"])
                   && responseFromFile.Contains("\"businessId\":\"" + (string)Session["Location"]))
                {
                    int i = 0;
                    //while (!words[i].Contains("\"name\":\"" + (string)Session["BusinessSector"]) && i < words.Length) //etsii hakusanalla ja tallentaa tiedot arrayhin
                    while (!words[i].Contains("\"name\":\"" + (string)Session["BusinessSector"]) && i < words.Length
                        || !words[i].Contains("\"companyForm\":\"" + (string)Session["CompanyForm"]) && i < words.Length
                        || !words[i].Contains("\"businessId\":\"" + (string)Session["Location"]) && i < words.Length)
                    {
                        i++;
                    }
                    string[] companyData = words[i].Split(','); //tiedon noutaminen + sanojen splittaus
                    //Session["CompanyData"] = companyData[2] + " " + companyData[3] + " " + companyData[1] + " " + companyData[4] + " " + companyData[5];
                    //Session["CompanyData"] = companyData[3];

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
                    //company details
                    string haettuDetailsRaw = companyData[5];
                    string haettuDetails = haettuDetailsRaw.Substring(14);
                    string haettuDetailsMinus = haettuDetails.Remove(haettuDetails.Length - 1);

                    Session["haettuNimi"] = haettuNimiMinus; //tallentaa nimen
                    Session["haettuRD"] = haettuRDMinus; //tallentaa registration daten
                    Session["haettuBI"] = haettuBIMinus; //tallentaa business idn
                    Session["haettuForm"] = haettuFormMinus; //tallentaa company formin
                    Session["haettuDetails"] = haettuDetailsMinus; //tallentaa companyn details urlin

                    //haun tulokset
                    Label11.Text = (string)Session["haettuNimi"]; //nimi
                    Label13.Text = (string)Session["haettuRD"]; //registration time
                    Label15.Text = (string)Session["haettuBI"]; //business id
                    Label18.Text = (string)Session["haettuForm"]; //company form

                    //tulosten määrä
                    int tulostenMaara = 0; //kesken vielä
                    int tul = 2; //2 on namen indeksi
                    if (companyData.Length >= (tul + 7)) //companyDatan pituus yhdelle yritykselle on 7)
                    {
                        while (companyData[tul] == "\"name\":\"" + (string)Session["BusinessSector"])
                        {
                            tulostenMaara++;
                            if (companyData.Length > (tul + 7) && companyData[tul + 7] != null)
                            {
                                //do nothing
                            }
                            else
                            {
                                tul += 7;
                            }
                        }
                    }
                    else
                    {
                        tulostenMaara = 1;
                    }
                    //Session["tulostenMaara"] = companyData;
                    Session["tulostenMaara"] = Convert.ToString(tulostenMaara);
                    Label19.Text = "Number of results found: ";
                    Label20.Text = (string)Session["tulostenMaara"];
                }
                else
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
                //***hakuohjelma loppuu***

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
                Label4.Text = (string)Session["CompanySize"];
            }
            catch (Exception err)
            {
                //Session["SearchError"] = Session["CompanyData"];
                Session["SearchError"] = "Error[003]: Something went horribly wrong.";
                Response.Redirect("index.aspx");
            }
        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("lisatiedot.aspx");
        }
    }
}