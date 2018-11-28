﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class pregledOpravljenihServisov : System.Web.UI.Page
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
            dictionary = Baza.DobiOpravljenaNarocila(ids);
            foreach (KeyValuePair<string, int> entry in dictionary)
            {
                //this.DropDownList2.Items.Insert(0, new ListItem(entry.Key, entry.Value.ToString()));
                TableRow tRow = new TableRow();
                this.Table1.Rows.Add(tRow);
                TableCell tCell1 = new TableCell();
                tCell1.Text = entry.Key;
                tRow.Cells.Add(tCell1);
            }
        }
    }
}