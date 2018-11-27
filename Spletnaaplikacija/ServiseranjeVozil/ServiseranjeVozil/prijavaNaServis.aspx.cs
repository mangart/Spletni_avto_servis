using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class prijavaNaServis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["stanje"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else if((int)Session["stanje"] != 1)
            {
                Response.Redirect("index.aspx");
            }
            if (!IsPostBack)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                dictionary = Baza.DobiPoslovalnice();
                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    this.DropDownList2.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                }
                dictionary = new Dictionary<string, int>();
                int stranka = Baza.DobiStranko((int)Session["uporabnik"]);
                dictionary = Baza.DobiVozila(stranka);
                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    this.DropDownList1.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                }
                for(int i = 2100;i > 2017; i--)
                {
                    this.DropDownList5.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
                }
                this.DropDownList8.Items.Insert(0, new ListItem("December", "12"));
                this.DropDownList8.Items.Insert(0, new ListItem("November", "11"));
                this.DropDownList8.Items.Insert(0, new ListItem("Oktober", "10"));
                this.DropDownList8.Items.Insert(0, new ListItem("Setember", "9"));
                this.DropDownList8.Items.Insert(0, new ListItem("August", "8"));
                this.DropDownList8.Items.Insert(0, new ListItem("Julij", "7"));
                this.DropDownList8.Items.Insert(0, new ListItem("Junij", "6"));
                this.DropDownList8.Items.Insert(0, new ListItem("Maj", "5"));
                this.DropDownList8.Items.Insert(0, new ListItem("April", "4"));
                this.DropDownList8.Items.Insert(0, new ListItem("Marec", "3"));
                this.DropDownList8.Items.Insert(0, new ListItem("Februar", "2"));
                this.DropDownList8.Items.Insert(0, new ListItem("Januar","1"));

                for(int i = 23; i >= 0; i--)
                {
                    this.DropDownList6.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
                }
                for (int i = 59; i >= 0; i--)
                {
                    this.DropDownList7.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
                }


            }
            int j;
            if(this.DropDownList8.SelectedValue.Equals("1") || this.DropDownList8.SelectedValue.Equals("3") || this.DropDownList8.SelectedValue.Equals("5") || this.DropDownList8.SelectedValue.Equals("7") || this.DropDownList8.SelectedValue.Equals("8") || this.DropDownList8.SelectedValue.Equals("10") || this.DropDownList8.SelectedValue.Equals("12"))
            {
                j = 31;
            }
            else if (this.DropDownList8.SelectedValue.Equals("2"))
            {
                j = 28;
            }
            else
            {
                j = 30;
            }
            this.DropDownList3.Items.Clear();
            for (int i = 1; i < j + 1; i++)
            {
                this.DropDownList3.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
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
                int idv = Int32.Parse(this.DropDownList1.SelectedValue);
                int idp = Int32.Parse(this.DropDownList2.SelectedValue);
                int dan = Int32.Parse(this.DropDownList3.SelectedValue);
                int mes = Int32.Parse(this.DropDownList8.SelectedValue);
                int leto = Int32.Parse(this.DropDownList5.SelectedValue);
                int ura = Int32.Parse(this.DropDownList6.SelectedValue);
                int min = Int32.Parse(this.DropDownList7.SelectedValue);
                string opis = this.TextBox1.Text;
                string rez = Baza.VnesiNarocilo(idv, idp, ura, min, dan, mes, leto, opis);
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