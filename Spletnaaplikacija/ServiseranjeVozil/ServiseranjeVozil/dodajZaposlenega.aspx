<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dodajZaposlenega.aspx.cs" Inherits="ServiseranjeVozil.dodajZaposlenega" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dodaj novega zaposlenega</title>
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
    <li><a href="Registracija.aspx">Registracija</a></li>
    <li><a href="prijava.aspx">Prijava</a></li>
    <li><a href="dodajVozilo.aspx">Dodajte vozilo</a></li>
    <li><a href="odjava.aspx">Odjava</a></li>
    <li><a href="prijavaNaServis.aspx">Prijava na servis</a></li>
    <li><a href="brisiVozilo.aspx">Briši svoja vozila</a></li>
    <li><a href="odstraniPrijavo.aspx">Odstani prijavo na servis</a></li>
    <li><a href="pregledServisov.aspx">Pregled odobrenih servisov</a></li>
    <li><a href="potrdiServis.aspx">Potrdi servis</a></li>
    <li><a href="opraviServis.aspx">Opravi servise</a></li>
    <li><a href="dodajZnamko.aspx">Dodaj znamko</a></li>
    <li><a href="dodajKraj.aspx">Dodaj kraj</a></li>
    <li><a href="dodajanjeModela.aspx">Dodaj model</a></li>
    <li><a href="brisiModel.aspx">Briši model</a></li>
    <li><a href="dodajPoslovalnico.aspx">Dodaj novo poslovalnico</a></li>
    <li><a href="brisiPoslovalnice.aspx">Briši poslovalnico</a></li>
    <li><a href="dodajZaposlenega.aspx">Dodaj zaposlenega</a></li>
</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server">Tukaj lahko dodajate nove zaposlene</p>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Uporabniško ime"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 57px"></asp:TextBox>
    
        <br />
        <asp:Label ID="Label2" runat="server" Text="Geslo"></asp:Label>
        <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" style="margin-left: 125px"></asp:TextBox>
    
        <br />
        <asp:Label ID="Label3" runat="server" Text="Ponovno geslo"></asp:Label>
        <asp:TextBox ID="TextBox3" TextMode="Password" runat="server" style="margin-left: 72px"></asp:TextBox>
    
        <br />
        <asp:Label ID="Label4" runat="server" Text="Ime"></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server" style="margin-left: 138px"></asp:TextBox>
    
        <br />
        <asp:Label ID="Label5" runat="server" Text="Priimek"></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server" style="margin-left: 115px"></asp:TextBox>
    
        <br />
        <asp:Label ID="Label6" runat="server" Text="Poslovalnica"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" style="margin-left: 140px">
        </asp:DropDownList>
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="Dodaj zaposlenega" OnClick="Button1_Click" style="height: 26px" />
        <asp:Button ID="Button2" runat="server" Text="Nazaj" OnClick="Button2_Click" style="margin-left: 36px" />
    
    </div>
    </form>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>
</body>
</html>
