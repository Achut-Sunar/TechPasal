using System;
using TechPasalWebForms.Data;

namespace TechPasalWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadFeatured();
        }

        private void LoadFeatured()
        {
            var repo = new ProductRepository();
            rptFeatured.DataSource = repo.GetFeatured();
            rptFeatured.DataBind();
        }
    }
}
