using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using System;
using System.Web;

namespace MySolution.Module.Web.Security {
    public class CustomSecurityStrategyComplex : SecurityStrategyComplex {
        protected override void InitializeNewUserCore(IObjectSpace objectSpace, object user) {
            base.InitializeNewUserCore(objectSpace, user);
        }
        public void InitializeNewUser(IObjectSpace objectSpace, object user) {
            InitializeNewUserCore(objectSpace, user);
        }
        public override void Logoff() {
            if (HttpContext.Current.Request.Cookies[".AspNet.External"] != null) {
                HttpContext.Current.Response.Cookies[".AspNet.External"].Expires = DateTime.Now.AddDays(-1);
            }
            if (HttpContext.Current.Request.Cookies[".AspNet.Cookies"] != null) {
                HttpContext.Current.Response.Cookies[".AspNet.Cookies"].Expires = DateTime.Now.AddDays(-1);
            }
            base.Logoff();
        }
    }
}
