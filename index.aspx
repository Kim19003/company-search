<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="nettisivut_app.Index" %>

<!DOCTYPE html>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Net"%>
<script runat="server" language="C#">

 </script>
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
      <h2>
        SEARCH
      </h2>

        <label>Name of the company:</label><br>
        <asp:TextBox ID="businessSectorBox" placeholder="Insert the name..." runat="server" CssClass="searchbox space" OnTextChanged="businessSectorBox_TextChanged"></asp:TextBox>
<p></p>
      <label>Business ID:</label><br>
        <asp:TextBox ID="locationBox" placeholder="Insert the business ID..." runat="server" CssClass="searchbox space" OnTextChanged="locationBox_TextChanged"></asp:TextBox>
	<p></p>
      <label>Foundation time: </label>
        <label style="color:grey">(2020 if blank)</label><br>
      <label>Earliest:&nbsp;</label>
        <asp:DropDownList ID="theDay" OnSelectedIndexChanged="theDay_SelectedIndexChanged" runat="server" CssClass="searchboxVar2 space space2 space3" Width="97px">
                  <asp:ListItem Value="" Text="Day..." style="color:grey"> </asp:ListItem>
                  <asp:ListItem Value="01">01</asp:ListItem>
                  <asp:ListItem Value="02">02</asp:ListItem>
                  <asp:ListItem Value="03">03</asp:ListItem>
                  <asp:ListItem Value="04">04</asp:ListItem>
                  <asp:ListItem Value="05">05</asp:ListItem>
                  <asp:ListItem Value="06">06</asp:ListItem>
                  <asp:ListItem Value="07">07</asp:ListItem>
                  <asp:ListItem Value="08">08</asp:ListItem>
                  <asp:ListItem Value="09">09</asp:ListItem>
                  <asp:ListItem Value="10">10</asp:ListItem>
                  <asp:ListItem Value="11">11</asp:ListItem>
                  <asp:ListItem Value="12">12</asp:ListItem>
            <asp:ListItem Value="13">13</asp:ListItem>
            <asp:ListItem Value="14">14</asp:ListItem>
            <asp:ListItem Value="15">15</asp:ListItem>
            <asp:ListItem Value="16">16</asp:ListItem>
            <asp:ListItem Value="17">17</asp:ListItem>
            <asp:ListItem Value="18">18</asp:ListItem>
            <asp:ListItem Value="19">19</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
            <asp:ListItem Value="21">21</asp:ListItem>
            <asp:ListItem Value="22">22</asp:ListItem>
            <asp:ListItem Value="23">23</asp:ListItem>
            <asp:ListItem Value="24">24</asp:ListItem>
            <asp:ListItem Value="25">25</asp:ListItem>
            <asp:ListItem Value="26">26</asp:ListItem>
            <asp:ListItem Value="27">27</asp:ListItem>
            <asp:ListItem Value="28">28</asp:ListItem>
            <asp:ListItem Value="29">29</asp:ListItem>
            <asp:ListItem Value="30">30</asp:ListItem>
            <asp:ListItem Value="31">31</asp:ListItem>
                  </asp:DropDownList>
                      <asp:DropDownList ID="theMonth" OnSelectedIndexChanged="theMonth_SelectedIndexChanged" runat="server" CssClass="searchboxVar2 space space2" Width="97px">
                  <asp:ListItem Value="" Text="Month..." style="color:grey"> </asp:ListItem>
                  <asp:ListItem Value="01">January</asp:ListItem>
                  <asp:ListItem Value="02">February</asp:ListItem>
                  <asp:ListItem Value="03">March</asp:ListItem>
                  <asp:ListItem Value="04">April</asp:ListItem>
                  <asp:ListItem Value="05">May</asp:ListItem>
                  <asp:ListItem Value="06">June</asp:ListItem>
                  <asp:ListItem Value="07">July</asp:ListItem>
                  <asp:ListItem Value="08">August</asp:ListItem>
                  <asp:ListItem Value="09">September</asp:ListItem>
                  <asp:ListItem Value="10">October</asp:ListItem>
                  <asp:ListItem Value="11">November</asp:ListItem>
                  <asp:ListItem Value="12">December</asp:ListItem>
                  </asp:DropDownList>
      <asp:TextBox ID="earliestBox" placeholder="Insert the year..." runat="server" CssClass="searchboxVar space" style="margin-top:12px" OnTextChanged="earliestBox_TextChanged" Width="97px"></asp:TextBox>
        <br>
	    <label style="margin-right:10px">Latest:&nbsp;</label>
        <asp:DropDownList ID="theDay2" OnSelectedIndexChanged="theDay2_SelectedIndexChanged" runat="server" CssClass="searchboxVar2 space space2 space3Var" Width="97px">
                  <asp:ListItem Value="" Text="Day..." style="color:grey"> </asp:ListItem>
                  <asp:ListItem Value="01">01</asp:ListItem>
                  <asp:ListItem Value="02">02</asp:ListItem>
                  <asp:ListItem Value="03">03</asp:ListItem>
                  <asp:ListItem Value="04">04</asp:ListItem>
                  <asp:ListItem Value="05">05</asp:ListItem>
                  <asp:ListItem Value="06">06</asp:ListItem>
                  <asp:ListItem Value="07">07</asp:ListItem>
                  <asp:ListItem Value="08">08</asp:ListItem>
                  <asp:ListItem Value="09">09</asp:ListItem>
                  <asp:ListItem Value="10">10</asp:ListItem>
                  <asp:ListItem Value="11">11</asp:ListItem>
                  <asp:ListItem Value="12">12</asp:ListItem>
            <asp:ListItem Value="13">13</asp:ListItem>
            <asp:ListItem Value="14">14</asp:ListItem>
            <asp:ListItem Value="15">15</asp:ListItem>
            <asp:ListItem Value="16">16</asp:ListItem>
            <asp:ListItem Value="17">17</asp:ListItem>
            <asp:ListItem Value="18">18</asp:ListItem>
            <asp:ListItem Value="19">19</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
            <asp:ListItem Value="21">21</asp:ListItem>
            <asp:ListItem Value="22">22</asp:ListItem>
            <asp:ListItem Value="23">23</asp:ListItem>
            <asp:ListItem Value="24">24</asp:ListItem>
            <asp:ListItem Value="25">25</asp:ListItem>
            <asp:ListItem Value="26">26</asp:ListItem>
            <asp:ListItem Value="27">27</asp:ListItem>
            <asp:ListItem Value="28">28</asp:ListItem>
            <asp:ListItem Value="29">29</asp:ListItem>
            <asp:ListItem Value="30">30</asp:ListItem>
            <asp:ListItem Value="31">31</asp:ListItem>
                  </asp:DropDownList>
                              <asp:DropDownList ID="theMonth2" OnSelectedIndexChanged="theMonth2_SelectedIndexChanged" runat="server" CssClass="searchboxVar2 space space2" Width="97px">
                  <asp:ListItem Value="" Text="Month..." style="color:grey"> </asp:ListItem>
                  <asp:ListItem Value="01">January</asp:ListItem>
                  <asp:ListItem Value="02">February</asp:ListItem>
                  <asp:ListItem Value="03">March</asp:ListItem>
                  <asp:ListItem Value="04">April</asp:ListItem>
                  <asp:ListItem Value="05">May</asp:ListItem>
                  <asp:ListItem Value="06">June</asp:ListItem>
                  <asp:ListItem Value="07">July</asp:ListItem>
                  <asp:ListItem Value="08">August</asp:ListItem>
                  <asp:ListItem Value="09">September</asp:ListItem>
                  <asp:ListItem Value="10">October</asp:ListItem>
                  <asp:ListItem Value="11">November</asp:ListItem>
                  <asp:ListItem Value="12">December</asp:ListItem>
                  </asp:DropDownList>
    <asp:TextBox ID="latestBox" placeholder="Insert the year..." runat="server" CssClass="searchboxVar space" OnTextChanged="latestBox_TextChanged" Width="97px" style="margin-left:1px;"></asp:TextBox>
<p></p>
    <label>Company size:</label><br />
      <asp:DropDownList ID="companySizeBox" OnSelectedIndexChanged="companySizeBox_SelectedIndexChanged" runat="server" CssClass="searchbox space space2">
                  <asp:ListItem Value="" Text="Choose company size..." style="color:grey"></asp:ListItem>
                  <asp:ListItem Value="Small"> Small </asp:ListItem>
                  <asp:ListItem Value="Medium"> Medium </asp:ListItem>
                  <asp:ListItem Value="Large"> Large </asp:ListItem>
      </asp:DropDownList>
        <p></p>
    <label>Company form:</label><br />
      <asp:DropDownList ID="companyFormBox" OnSelectedIndexChanged="companyFormBox_SelectedIndexChanged" runat="server" CssClass="searchbox space space2">
                  <asp:ListItem Value="" Text="Choose company form..." style="color:grey"> </asp:ListItem>
                  <asp:ListItem Value="OY">OY</asp:ListItem>
                  <asp:ListItem Value="AOY">AOY</asp:ListItem>
                  <asp:ListItem Value="OK">OK</asp:ListItem>
      </asp:DropDownList>
        <p></p>
  <asp:Label ID="errorText1" runat="server" CssClass="errortext" Font-Bold="True"></asp:Label>
<div>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="SEARCH" CssClass="returnbox2 h3" Font-Bold="True" Width="227px" />
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
    </form>
</body>
</html>
