<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dodajanjeModela.aspx.cs" Inherits="ServiseranjeVozil.dodajanjeModela" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dodaj Model</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="style.css" rel="stylesheet" />
</head>
<body>
<div class="flex-container">
<header>
  <h1>Spletni servis vozil</h1>
</header>

<nav class="nav">
<ul>
	<li><a href="index.aspx">Začetna stran</a></li>
    <li><a href="dodajZnamko.aspx">Dodaj znamko</a></li>
    <li><a href="dodajKraj.aspx">Dodaj kraj</a></li>
    <li><a href="dodajanjeModela.aspx">Dodaj model</a></li>
    <li><a href="brisiModel.aspx">Briši model</a></li>
    <li><a href="dodajPoslovalnico.aspx">Dodaj novo poslovalnico</a></li>
    <li><a href="brisiPoslovalnice.aspx">Briši poslovalnico</a></li>
    <li><a href="dodajZaposlenega.aspx">Dodaj zaposlenega</a></li>
    <li><a href="brisiUporabnika.aspx">Briši uporabnika</a></li>
    <li><a href="odjava.aspx">Odjava</a></li>

</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server">Tukaj lahko dodate nove modele vozil</p>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Izberi znamko"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" style="margin-left: 57px">
        </asp:DropDownList>
    
        <br />
        <asp:Label ID="Label2" runat="server" Text="Nov model"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 27px"></asp:TextBox>
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="Dodaj model" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Nazaj" OnClick="Button2_Click" style="margin-left: 50px" />
    
    </div>
    </form>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>
</body>
</html>
