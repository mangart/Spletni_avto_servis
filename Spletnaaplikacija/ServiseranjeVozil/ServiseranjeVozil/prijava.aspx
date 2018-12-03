<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prijava.aspx.cs" Inherits="ServiseranjeVozil.prijava" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prijava</title>
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

</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server">Tukaj se lahko prijavite</p>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Uporabniško ime"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 52px"></asp:TextBox>
    
        <br />
        <asp:Label ID="Label2" runat="server" Text="Geslo"></asp:Label>
        <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" style="margin-left: 120px"></asp:TextBox>
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="Prijava" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Nazaj" OnClick="Button2_Click" style="margin-left: 51px" />
    
    </div>
    </form>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>
</body>
</html>
