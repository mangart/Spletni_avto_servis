<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ServiseranjeVozil.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Spletni servis vozil</title>
    <meta charset="UTF-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<link href="style.css" rel="stylesheet" />
</head>
<body>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
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
    <li><a href="odstraniZnamko.aspx">Odstrani znamko</a></li>
    <li><a href="dodajKraj.aspx">Dodaj kraj</a></li>
    <li><a href="brisiKraj.aspx">Brisi kraj</a></li>
    <li><a href="dodajanjeModela.aspx">Dodaj model</a></li>
    <li><a href="brisiModel.aspx">Briši model</a></li>
    <li><a href="dodajPoslovalnico.aspx">Dodaj novo poslovalnico</a></li>
    <li><a href="brisiPoslovalnice.aspx">Briši poslovalnico</a></li>
    <li><a href="dodajZaposlenega.aspx">Dodaj zaposlenega</a></li>
    <li><a href="brisiUporabnika.aspx">Briši uporabnika</a></li>
    <li><a href="pregledOpravljenihServisov.aspx">Pregled opravljenih servisev</a></li>
</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server"></p>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>

</body>
</html>
