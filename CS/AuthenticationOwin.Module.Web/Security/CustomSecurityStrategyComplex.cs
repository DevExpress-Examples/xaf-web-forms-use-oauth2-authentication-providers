using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;

namespace AuthenticationOwin.Module.Web.Security
{
    public class CustomSecurityStrategyComplex : SecurityStrategyComplex
    {
        protected override void InitializeNewUserCore(IObjectSpace objectSpace, object user)
        {
            base.InitializeNewUserCore(objectSpace, user);
        }
        public void InitializeNewUser(IObjectSpace objectSpace, object user)
        {
            InitializeNewUserCore(objectSpace, user);
        }
        public override void Logoff()
        {
            if (HttpContext.Current.Request.Cookies[".AspNet.External"] != null)
            {
                HttpContext.Current.Response.Cookies[".AspNet.External"].Expires = DateTime.Now.AddDays(-1);
            }
            base.Logoff();
        }
    }
}
