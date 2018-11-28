using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class brisiVozilo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uporabnik"] == null || Session["stanje"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else if ((int)Session["stanje"] != 1)
            {
                Response.Redirect("index.aspx");
            }

                int ids = Baza.DobiStranko((int)Session["uporabnik"]);
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                dictionary = Baza.DobiVozila(ids);
                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    //this.DropDownList2.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                    TableRow tRow = new TableRow();
                    this.Table1.Rows.Add(tRow);
                    TableCell tCell1 = new TableCell();
                    TableCell tCell2 = new TableCell();
                    tCell1.Text = entry.Key;
                    Button bt = new Button();
                    bt.Text = "Briši";
                    bt.ID = entry.Value.ToString();
                    bt.Click += OnClick;
                    tRow.Cells.Add(tCell1);
                    tCell2.Controls.Add(bt);
                    tRow.Cells.Add(tCell2);
                }
            

        }

        protected void OnClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonId = button.ID;
            int idv = Int32.Parse(button.ID);
            int lahkoBrisan = Baza.PreveriServis(idv);
            if(lahkoBrisan == 1)
            {
                string bri = Baza.BrisiVozilo(idv);
                if(bri == "")
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    this.peter.InnerHtml = bri;
                }

            }
            else
            {
                this.peter.InnerHtml = "Vozila se ne, da izbrisati, ker je njegov servis trenutno že odobren!";
            }
            System.Diagnostics.Debug.WriteLine("Moj id je "+ buttonId);
        }
    }
}