<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="odstraniPrijavo.aspx.cs" Inherits="ServiseranjeVozil.odstraniPrijavo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tukaj lahko odstranite še ne potrjene prijave na servis</title>
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

</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server">Tukaj lahko brišete vaša naročila na servis</p>
    <form id="form1" runat="server">
    <div>
    
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
    
    </div>
    </form>
</body>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>
</html>
