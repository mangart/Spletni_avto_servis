<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="odjava.aspx.cs" Inherits="ServiseranjeVozil.odjava" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Odjava</title>
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
<p style="text-align:center" id="peter" runat="server">Sedaj ste odjavljeni</p>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>
</body>
</html>
