<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kartta.aspx.cs" Inherits="nettisivut_app.kartta" %>

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
    <p><iframe src="https://batchgeo.com/map/5511a36b175e6988a4b12c6559e77b7d" frameborder="0" width="100%" height="550" sandbox="allow-top-navigation allow-scripts allow-popups allow-same-origin allow-modals allow-forms" style="border:1px solid #aaa;"></iframe>
	<br>
	<b>Interactive map provided by <a href="https://batchgeo.com/" target="_blank" style="text-decoration:none">BatchGeo</a></b>.
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

