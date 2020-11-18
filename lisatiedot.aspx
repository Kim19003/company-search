<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lisatiedot.aspx.cs" Inherits="nettisivut_app.lisatiedot" %>

<!DOCTYPE html>
<%

%>
<html lang="en">

<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Company search</title>
  <link href="awp%20style.css" rel="stylesheet" type="text/css" />
</head>

<body class="Etusivu">
  <div class="page">
  <div class="yläosa">
  </div>
    <div class="banner dropdown">

    <button class="dropdownbutton"><h2 style="padding:0px;margin:0px">MENU</h2></button>

	<div class="dropdown-content">

	<a href="index.aspx"><h3 style="padding:0px;margin:0px">Search</h3></a>

    <a href="Viimeeksi%20lisätyt%20yritykset.aspx"><h3 style="padding:0px;margin:0px">Recently added companies</h3></a>
    <a href="Kartta.aspx"><h3 style="padding:0px;margin:0px">Map</h3></a>
    </div>
	</div>
    <div class="väli1">
    </div>
    <div class="mainpage">

        <h2>COMPANY DETAILS</h2>
        <h2 class="custom-h2"><%Response.Write((string)Session["haettuNimi"]); %></h2>
        <p class="labelstyle removepm" style="margin-top:10px">Business ID: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuBI"]); %></span><br>
        Registration time: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuRD"]); %></span><br>
        Company form: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuForm"]); %></span><br>
        Address: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuStreet"] + ", " + (string)Session["haettuCity"]); %></span><br>
        Postal code: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuPC"]); %></span><br>
        Country: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuCountry"]); %></span><br>
        Phone number: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuPhone"]); %></span><br>
        Website: <span style='background-color:#B2DCB6'><%Response.Write((string)Session["haettuWS"]); %></span></p>

<div>
	  <a class="returnbox3" href="javascript: history.go(-1)"><h3 style="padding:0px;margin:0px">RETURN</h3></a>
	  </div>
        </div>
    <div class="väli2">
    </div>
    <div class="footer">
      <p>
      <img src="pictures/logoxd.JPG" alt="logo" height="30px;">

        ©2020 Mukero Oy<br />
        This site uses avoindata.prh.fi as a data source.
      </p>
    </div>
  </div>
</body>
</html>

