<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dodajVozilo.aspx.cs" Inherits="ServiseranjeVozil.dodajVozilo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dodaj vozilo</title>
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
    <li><a href="dodajVozilo.aspx">Dodajte vozilo</a></li>
    <li><a href="prijavaNaServis.aspx">Prijava na servis</a></li>
    <li><a href="brisiVozilo.aspx">Briši svoja vozila</a></li>
    <li><a href="pregledServisov.aspx">Pregled odobrenih servisov</a></li>
    <li><a href="pregledOpravljenihServisov.aspx">Pregled opravljenih servisev</a></li>
    <li><a href="odjava.aspx">Odjava</a></li>
</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server">Tukaj lahko dodate svoja vozila</p>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:updatepanel runat="server" id="UpdatePanel" UpdateMode="Conditional">
        <contenttemplate>
        <asp:Label ID="Label1" runat="server" Text="Izberite znamko"></asp:Label>
        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" style="margin-left: 87px">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Izberite model"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server" style="margin-left: 97px">
        </asp:DropDownList>
        <br />
        </contenttemplate>
        </asp:updatepanel>
        <asp:Label ID="Label3" runat="server" Text="Letnik vozila"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 55px" type="number" ></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Dodaj vozilo" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Nazaj" OnClick="Button2_Click" style="margin-left: 54px" />
        <br />
    
    </div>
    </form>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>
</body>
</html>
