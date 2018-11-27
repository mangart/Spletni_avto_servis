<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registracija.aspx.cs" Inherits="ServiseranjeVozil.Registracija" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registracija</title>
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
</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server">Tukaj se lahko registrirate</p>
    <form id="form1" runat="server">

    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Uporabniško ime"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 76px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Geslo"></asp:Label>
        <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" style="margin-left: 146px"></asp:TextBox>
        <br />
    
        <asp:Label ID="Label3" runat="server" Text="Ponovno geslo"></asp:Label>
        <asp:TextBox ID="TextBox3" TextMode="Password" runat="server" style="margin-left: 92px"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Ime"></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server" style="margin-left: 158px"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Priimek"></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server" style="margin-left: 133px"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="E-mail"></asp:Label>
        <asp:TextBox ID="TextBox6" runat="server" style="margin-left: 142px"></asp:TextBox>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Telefon"></asp:Label>
        <asp:TextBox ID="TextBox7" runat="server" style="margin-left: 133px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Registriraj se" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Nazaj" />
        <br />
    
    </div>
    </form>

</article>

<footer>Avtor: Žan Gostič</footer>
</div>

</body>
</html>
