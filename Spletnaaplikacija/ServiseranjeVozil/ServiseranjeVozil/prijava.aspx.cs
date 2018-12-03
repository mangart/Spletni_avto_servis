using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class prijava : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int iupo;
            if(this.TextBox1.Text.Length != 0 && this.TextBox2.Text.Length != 0)
            {
                if (this.TextBox1.Text.Equals("admin") && this.TextBox2.Text.Equals("zemlja"))
                {
                    iupo = 1;
                    Session["uporabnik"] = 1;
                    Session["stanje"] = 3;
                    Response.Redirect("index.aspx");
                }
                else if (this.TextBox1.Text.Equals("admin"))
                {
                    iupo = 0;
                }
                else
                {
                    iupo = Baza.validiraj(this.TextBox1.Text, this.TextBox2.Text);
                }
                if(iupo == 0)
                {
                    this.peter.InnerHtml = "Geslo ni pravilno!";
                }
                else if(iupo == -2)
                {
                    this.peter.InnerHtml = "Uporabni ne obstaja!";
                }
                else if(iupo > 0)
                {
                    this.peter.InnerHtml = "Geslo in uporabniško ime sta pravilna!";
                    Session["uporabnik"] = iupo;
                    int stranka = Baza.dobiStanje(iupo);
                    if(stranka == 0)
                    {
                        Session["stanje"] = 2;
                        this.peter.InnerHtml = "Uporabnik je Zaposleni!";
                    }
                    else
                    {
                        Session["stanje"] = 1;
                        this.peter.InnerHtml = "Uporabnik je Stranka!";
                    }
                    Response.Redirect("index.aspx");
                }
                else
                {
                    this.peter.InnerHtml = "Napaka podatkovne baze, kasneje poskusite ponovno!";
                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}