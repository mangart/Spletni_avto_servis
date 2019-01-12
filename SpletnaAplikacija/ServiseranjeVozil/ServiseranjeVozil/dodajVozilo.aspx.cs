using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class dodajVozilo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["stanje"] != null)
            {
                if ((int)Session["stanje"] > 1)
                {
                    Response.Redirect("index.aspx");
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
            if (!IsPostBack)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                dictionary = Baza.DobiZnamke();
                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    this.DropDownList3.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                }
                dictionary = new Dictionary<string, int>();
                int idzn = Int32.Parse(this.DropDownList3.SelectedValue);
                dictionary = Baza.DobiModele(idzn);
                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    this.DropDownList2.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                }
            }


        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text.Length != 0)
            {
                int znamka = Int32.Parse(this.DropDownList3.SelectedValue);
                int model = Int32.Parse(this.DropDownList2.SelectedValue);
                int idStranke = Baza.DobiStranko((int)Session["Uporabnik"]);
                string uspeh = Baza.VnesiVozilo(znamka, model, Int32.Parse(this.TextBox1.Text), idStranke);
                this.peter.InnerHtml = uspeh;
                //Response.Redirect("index.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }



        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            int idzn = Int32.Parse(this.DropDownList3.SelectedValue);
            dictionary = Baza.DobiModele(idzn);
            this.DropDownList2.Items.Clear();
            foreach (KeyValuePair<string, int> entry in dictionary)
            {
                this.DropDownList2.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
            }
            this.UpdatePanel.Update();
        }
    }
}