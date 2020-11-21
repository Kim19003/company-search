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

    <button class="dropdownbutton" style="cursor: pointer"><h2 style="padding:0px;margin:0px">Menu</h2></button>

	<div class="dropdown-content">

	<a href="index.aspx"><h3 style="padding:0px;margin:0px">Search</h3></a>

    <a href="about.aspx"><h3 style="padding:0px;margin:0px">About</h3></a>
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
            <asp:Label ID="Label1" runat="server" BackColor="#edc5d5"></asp:Label><br />
            <asp:Label ID="Label7" runat="server" Text="The business ID of the company is: "></asp:Label>
            <asp:Label ID="Label2" runat="server" BackColor="#edc5d5"></asp:Label><br />
            <asp:Label ID="Label8" runat="server" Text="The company was founded in between: "></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="(Foundation time: Earliest and Latest)" BackColor="#edc5d5"></asp:Label><br />
            <asp:Label ID="Label10" runat="server" Text="The company's form is:  "></asp:Label>
            <asp:Label ID="Label5" runat="server" Text="" BackColor="#edc5d5"></asp:Label>
            </p>
            <br />
                <h2>
        SEARCH RESULTS:
		</h2>
            <p class="labelstyle removepm">
            <asp:Label ID="Label11" runat="server" Text="Unnamed Company" Font-Bold="True" Font-Size="Large"></asp:Label><br />
            <asp:Label ID="Label12" runat="server" Text="Registered: "></asp:Label>
            <asp:Label ID="Label13" runat="server" Text="(Registered)" BackColor="#edc5d5"></asp:Label><br />
            <asp:Label ID="Label14" runat="server" Text="Business ID: "></asp:Label>
            <asp:Label ID="Label15" runat="server" Text="(Business ID)" BackColor="#edc5d5"></asp:Label><br />
            <asp:Label ID="Label16" runat="server" Text="Company form: "></asp:Label>
            <asp:Label ID="Label18" runat="server" Text="(Company form)" BackColor="#edc5d5"></asp:Label><br />               
            <asp:Button ID="Button1" runat="server" Font-Bold="False" OnClick="Button1_Click" Text="More details" BackColor="#BD3B70" BorderWidth="0px" Font-Names="Bahnschrift" Font-Size="12pt" ForeColor="White" style=" font-family: Bahnschrift;
    margin-top: 5px;
    padding-left: 10px;
    padding-right: 10px;
    font-weight: 500;
    font-size: 16px;
    outline:none;
    /* border-radius: 12px; */
    cursor: pointer;" />
                 </p>
            <br />
            <br />
            <p class="labelstyle removepm">
            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label20" runat="server" Text="" BackColor="#edc5d5"></asp:Label><br />
            </p>
		</div>
      <div>
	  <a class="returnbox" style="margin-top:130px" href="index.aspx"><h3 style="padding:0px;margin:0px">RETURN</h3></a><br />
	  </div>
    </div>
    <div class="väli2">
    </div>
    <div class="footer">
      <p style="font-size: 12px">
        This site uses avoindata.prh.fi as a data source.
      </p>
    </div>
  </div>




    </form>




</body>
</html>


