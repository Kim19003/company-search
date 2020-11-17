<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hauntulokset.aspx.cs" Inherits="nettisivut_app.hauntulokset" %>

<!DOCTYPE html>
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
        SEARCH QUERIES:
		</h2>
		<div class="searchresults">
		<p class="labelstyle removepm">
            <asp:Label ID="Label6" runat="server" Text="The name of the company is: "></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="" BackColor="#B2DCB6"></asp:Label><br />
            <asp:Label ID="Label7" runat="server" Text="The business ID of the company is: "></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="" BackColor="#B2DCB6"></asp:Label><br />
            <asp:Label ID="Label8" runat="server" Text="The company was founded in between: "></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="(Foundation time: Earliest and Latest)" BackColor="#B2DCB6"></asp:Label><br />
            <asp:Label ID="Label9" runat="server" Text="The company's size is:  "></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="" BackColor="#B2DCB6"></asp:Label><br />
            <asp:Label ID="Label10" runat="server" Text="The company's form is:  "></asp:Label>
            <asp:Label ID="Label5" runat="server" Text="" BackColor="#B2DCB6"></asp:Label>
            </p>
            <br />
                <h2>
        SEARCH RESULTS:
		</h2>
            <p class="labelstyle removepm">
            <asp:Label ID="Label11" runat="server" Text="Unnamed Company" Font-Bold="True" Font-Size="Large"></asp:Label><br />
            <asp:Label ID="Label12" runat="server" Text="Registered: "></asp:Label>
            <asp:Label ID="Label13" runat="server" Text="(Registered)" BackColor="#B2DCB6"></asp:Label><br />
            <asp:Label ID="Label14" runat="server" Text="Business ID: "></asp:Label>
            <asp:Label ID="Label15" runat="server" Text="(Business ID)" BackColor="#B2DCB6"></asp:Label><br />
            <asp:Label ID="Label16" runat="server" Text="Company form: "></asp:Label>
            <asp:Label ID="Label18" runat="server" Text="(Company form)" BackColor="#B2DCB6"></asp:Label><br />
            <asp:Button ID="Button1" runat="server" CssClass="MDbutton" Font-Bold="True" OnClick="Button1_Click" Text="More details" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="0px" Font-Names="Bahnschrift" Font-Size="12pt" ForeColor="Blue" style=" font-family: Bahnschrift;
    margin: 0 auto;
    padding: 0;
    font-weight: bold;
    font-size: 16px;
    color: blue;
    background-color: white;
    border: none;
    outline: none;
    cursor: pointer;" />
                            </p>
            <br />
            <br />
            <p class="labelstyle removepm">
            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label20" runat="server" Text="" BackColor="#B2DCB6"></asp:Label><br />
            </p>
		</div>
      <div>
	  <a class="returnbox" href="index.aspx"><h3 style="padding:0px;margin:0px">RETURN</h3></a><br />
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


