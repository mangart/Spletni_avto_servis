using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class Registracija : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.peter.InnerHtml = "Je postback";
            }
            else
            {
                this.peter.InnerHtml = "Ni postback";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            if(this.TextBox1.Text.Length != 0 && this.TextBox2.Text.Length != 0 && this.TextBox3.Text.Length != 0 && this.TextBox4.Text.Length != 0 && this.TextBox5.Text.Length != 0 && this.TextBox6.Text.Length != 0 && this.TextBox7.Text.Length != 0)
            {
                if (this.TextBox6.Text.Contains("@") && this.TextBox7.Text.All(Char.IsDigit))
                {
                    if (this.TextBox2.Text.Equals(this.TextBox3.Text))
                    {
                       int stanje =  Baza.registriraj(this.TextBox1.Text, this.TextBox2.Text, this.TextBox4.Text, this.TextBox5.Text, this.TextBox6.Text, this.TextBox7.Text);
                        if(stanje == 0)
                        {
                            Response.Redirect("index.aspx");
                        }
                        else if(stanje == 2)
                        {
                            this.peter.InnerHtml = "To uporabniško ime že obstaja";
                        }
                        else
                        {
                            this.peter.InnerHtml = "Napaka s podatkovno bazo poskusite kasneje";
                        }
                    }
                    else
                    {
                        this.peter.InnerHtml = "Gesli se ne ujemata!";
                    }
                }
                else
                {
                    this.peter.InnerHtml = "Telefon in/ali email nista pravilnega formata";
                }
            }
            else
            {
                this.peter.InnerHtml = "Vsi podatki niso izpolnjeni!";
            }
            /*
            this.peter.InnerHtml = this.TextBox5.Text;
            Response.Redirect("index.aspx");
            */

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}