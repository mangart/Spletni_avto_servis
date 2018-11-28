using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class dodajZnamko : System.Web.UI.Page
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
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text.Length > 0)
            {
                string rez = Baza.VnesiZnamko(this.TextBox1.Text);
                if (rez == "")
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    this.peter.InnerHtml = rez;
                }
            }
        }
    }
}