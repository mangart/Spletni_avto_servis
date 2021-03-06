﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class opraviServis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uporabnik"] == null || Session["stanje"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else if ((int)Session["stanje"] != 2)
            {
                Response.Redirect("index.aspx");
            }

            int idz = Baza.DobiZaposlenega((int)Session["uporabnik"]);
            int idp = Baza.DobiPoslovalnico((int)Session["uporabnik"]);
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary = Baza.DobiNarocilaOdobrena(idp);
            foreach (KeyValuePair<string, int> entry in dictionary)
            {
                //this.DropDownList2.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                TableRow tRow = new TableRow();
                this.Table1.Rows.Add(tRow);
                TableCell tCell1 = new TableCell();
                TableCell tCell2 = new TableCell();
                TableCell tCell3 = new TableCell();
                tCell1.Text = entry.Key;
                Button bt = new Button();
                bt.Text = "Opravi";
                bt.ID = entry.Value.ToString();
                bt.Click += OnClick;
                Button bt1 = new Button();
                bt1.Text = "Briši";
                bt1.ID = "0" + entry.Value.ToString();
                bt1.Click += OnClick;
                tRow.Cells.Add(tCell1);
                tCell2.Controls.Add(bt);
                tRow.Cells.Add(tCell2);
                tCell3.Controls.Add(bt1);
                tRow.Cells.Add(tCell3);

            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonId = button.ID;
            string btntxt = button.Text;
            int idn = Int32.Parse(button.ID);
            if (btntxt.Equals("Opravi"))
            {
                string bri = Baza.OpraviNarocilo(idn);
                if (bri == "")
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
                string bri = Baza.BrisiNarocilo(idn);
                if (bri == "")
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    this.peter.InnerHtml = bri;
                }
            }
            System.Diagnostics.Debug.WriteLine("Moj id je " + buttonId);
        }
    }
}