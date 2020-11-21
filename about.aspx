<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="nettisivut_app.about" %>

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
        <h2>ABOUT</h2>
  <p class="custom-p"><b>Description:</b> Finnish company search application,<br />shows the best result for the user.<br /><br />
      <b>Background:</b> Perfection of my older school project.<br /><br />
      <b>Methods used:</b> C#, ASP.NET, JavaScript, HTML & CSS.<br /><br />
      <b>Made by:</b> Kim19003, additional thanks to my old project group<br />members.<br /><br />
      <b>Color pallet:</b> "Portada" by: Diego Fernández Aguilar.
  </p>
    <image src ="\pictures\color%20pallet.png" style="max-width: 100%; min-width: 100%;"></image>
        <p class="custom-p">
            </p>
               <div>
	  <a class="returnbox" style="margin-top:60px" href="index.aspx"><h3 style="padding:0px;margin:0px">RETURN</h3></a><br />
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

