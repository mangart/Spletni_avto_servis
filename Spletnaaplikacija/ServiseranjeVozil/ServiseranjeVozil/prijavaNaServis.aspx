<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prijavaNaServis.aspx.cs" Inherits="ServiseranjeVozil.prijavaNaServis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prijava na servis</title>
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
    <li><a href="odstraniPrijavo.aspx">Odstani prijavo na servis</a></li>
    <li><a href="pregledServisov.aspx">Pregled odobrenih servisov</a></li>
    <li><a href="odjava.aspx">Odjava</a></li>
</ul>
</nav>

<article class="article">
<p style="text-align:center"><h3>Pozrdavljeni v aplikaciji servis vozil</h3></p>
<p style="text-align:center" id="peter" runat="server">Tukaj se lahko prijavite na servis vašega vozila</p>
    <form id="form1" runat="server">
    <div>

        <asp:Label ID="Label1" runat="server" Text="Izberite svoje vozilo"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" style="margin-left: 48px">
        </asp:DropDownList>
    
        <br />
        <asp:Label ID="Label2" runat="server" Text="Izberite poslovalnico"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server" style="margin-left: 46px">
        </asp:DropDownList>
    
    
        <br />
        <asp:Label ID="Label3" runat="server" Text="Izberite dan servisa"></asp:Label>
        <asp:DropDownList ID="DropDownList3" runat="server" style="margin-left: 54px">
        </asp:DropDownList>
    
    
        <br />
        <asp:Label ID="Label4" runat="server" Text="Izberite mesec servisa"></asp:Label>
        <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True" style="margin-left: 42px">
        </asp:DropDownList>
   
    
        <br />
        <asp:Label ID="Label5" runat="server" Text="Izberite leto servisa"></asp:Label>
        <asp:DropDownList ID="DropDownList5" runat="server" style="margin-left: 57px">
        </asp:DropDownList>
    
    
        <br />
        <asp:Label ID="Label6" runat="server" Text="Izberite uro servisa"></asp:Label>
        <asp:DropDownList ID="DropDownList6" runat="server" style="margin-left: 62px">
        </asp:DropDownList>
    
    
        <br />
        <asp:Label ID="Label7" runat="server" Text="Izberite minuto servisa"></asp:Label>
        <asp:DropDownList ID="DropDownList7" runat="server" style="margin-left: 41px">
        </asp:DropDownList>
    
   
        <br />
        <asp:Label ID="Label8" runat="server" Text="Opišite problem vozila"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 88px"></asp:TextBox>
    
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="Dodaj prijavo" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Nazaj" OnClick="Button2_Click" style="margin-left: 53px" />
    
    
    </div>
    </form>
</article>

<footer>Avtor: Žan Gostič</footer>
</div>
</body>
</html>
