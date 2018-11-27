﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ServiseranjeVozil.index" %>

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
