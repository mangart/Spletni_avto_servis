using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class dodajanjeModela : System.Web.UI.Page
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
                dictionary = Baza.DobiZnamke();
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
            if(this.TextBox1.Text.Length > 0)
            {
                string rez = Baza.VnesiModel(Int32.Parse(this.DropDownList1.SelectedValue), this.TextBox1.Text);
                if(rez == "")
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