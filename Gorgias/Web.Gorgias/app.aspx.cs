using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gorgias;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace WebGorgias
{
    public partial class app : System.Web.UI.Page
    {
        public Gorgias.Business.DataTransferObjects.Web.LowAppProfileModel myApp = new Gorgias.Business.DataTransferObjects.Web.LowAppProfileModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["profileid"] != null)
            {
                string profileURL = Request.QueryString["profileid"];
                myApp = Gorgias.BusinessLayer.Facades.Facade.WebFacade().getLowAppProfile(profileURL);

                var title = new HtmlMeta {Content = myApp.ProfileFullname };
                title.Attributes.Add("property", "og:title");
                Header.Controls.Add(title);                

                var image = new HtmlMeta { Content = myApp.ProfileImage + "?timestamp=" + DateTime.Now.Millisecond.ToString() + DateTime.Now.Minute.ToString() };
                image.Attributes.Add("property", "og:image");
                Header.Controls.Add(image);

                var http = new HtmlMeta { Content = "http://gorgias.azurewebsites.net/app/" + profileURL };
                http.Attributes.Add("http-equiv", "refresh");
                Header.Controls.Add(http);

                HtmlMeta meta = new HtmlMeta();
                meta.HttpEquiv = "Refresh";
                meta.Content = "7;url=/app/" + profileURL;
                Header.Controls.Add(meta);

                //Thread obj = new Thread(new ThreadStart(this.RedirectToApp));
                //obj.IsBackground = true;
                //obj.Start();
            }   
        }   
        
        public void RedirectToApp()
        {
            Thread.Sleep(10000);
            HttpContext.Current.Response.Redirect("/app/" + Request.QueryString["profileid"]);
        }
    }
}