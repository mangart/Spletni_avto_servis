using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiseranjeVozil
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.peter.Style.Add("text-align", "center");
            if (Session["neki"] == null)
            {
                Session["neki"] = "Nakljucno blabla!";
            }
            if(Session["neki"] == "Nakljucno blabla!")
            {
                this.peter.InnerHtml = "Spremenjeno besedilo!";
                Session["neki"] = "Spremenjeno besedilo!";
            }
            else
            {
                Session["neki"] = "Nakljucno blabla!";
                this.peter.InnerHtml = "Nakljucno blabla";
            }
            this.peter.InnerHtml = Baza.NekaFunkcija();

        }
     }

}