using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class dodajZaposlenega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uporabnik"] == null || Session["stanje"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else if ((int)Session["stanje"] != 3)
            {
                Response.Redirect("index.aspx");
            }
            if (!IsPostBack)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                dictionary = Baza.DobiPoslovalnice();
                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    this.DropDownList1.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text.Length != 0 && this.TextBox2.Text.Length != 0 && this.TextBox3.Text.Length != 0 && this.TextBox4.Text.Length != 0 && this.TextBox5.Text.Length != 0)
            {
                if (this.TextBox2.Text.Equals(this.TextBox3.Text))
                {
                    int stanje = Baza.registrirajZaposlenega(this.TextBox1.Text, this.TextBox2.Text, this.TextBox4.Text, this.TextBox5.Text, Int32.Parse(this.DropDownList1.SelectedValue));
                    if (stanje == 0)
                    {
                        Response.Redirect("index.aspx");
                    }
                    else if (stanje == 2)
                    {
                        this.peter.InnerHtml = "To uporabniško ime že obstaja";
                    }
                    else
                    {
                        this.peter.InnerHtml = "Napaka s podatkovno bazo poskusite kasneje";
                    }
                }

                }
            else
            {
                this.peter.InnerHtml = "Vsi podatki niso izpolnjeni!";
            }
            }
    }
}