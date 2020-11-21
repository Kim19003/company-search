<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lisatiedot.aspx.cs" Inherits="nettisivut_app.lisatiedot" %>

<!DOCTYPE html>
<%

%>
<html lang="en">

<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Company search</title>
  <link href="awp%20style.css" rel="stylesheet" type="text/css"/>
</head>

<body class="Etusivu">
    <form id="form1" runat="server">
  <div class="page">
  <div class="yläosa">
  </div>
    <div class="banner dropdown">

    <button class="dropdownbutton" style="cursor: pointer"><h2 style="padding:0px;margin:0px">Menu</h2></button>

	<div class="dropdown-content">

	<a href="index.aspx"><h3 style="padding:0px;margin:0px">Search</h3></a>

    <a href="about.aspx"><h3 style="padding:0px;margin:0px">About</h3></a>
    </div>
	</div>
    <div class="väli1">
    </div>
    <div class="mainpage">

        <h2>COMPANY DETAILS</h2>
        <h2 class="custom-h2"><%Response.Write((string)Session["haettuNimi"]); %></h2>
        <p class="labelstyle removepm" style="margin-top:10px">Business ID: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuBI"]); %></span><br>
        Registration time: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuRD"]); %></span><br>
        Company form: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuForm"]); %></span><br>
        Address: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuStreet"] + ", " + (string)Session["haettuCity"]); %></span><br>
        Postal code: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuPC"]); %></span><br>
        Country: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuCountry"]); %></span><br>
        Phone number: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuPhone"]); %></span><br>
        Website: <span style='background-color:#edc5d5'><%Response.Write((string)Session["haettuWS"]); %></span></p>

<div>
	  <a class="returnbox3" href="javascript: history.go(-1)"><h3 style="padding:0px;margin:0px">RETURN</h3></a>
	  </div>
        </div>
    <div class="väli2">
    </div>
    <div class="footer">
      <p style="font-size: 14px; color:white; text-align:center">
        This app uses avoindata.prh.fi as a data source.
      </p>
    </div>
  </div>
</form>
</body>
</html>

