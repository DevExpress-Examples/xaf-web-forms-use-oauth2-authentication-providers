using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;

namespace AuthenticationOwin.Module.Web.Security {
    public class CustomSecurityStrategyComplex : SecurityStrategyComplex {
        protected override void InitializeNewUserCore(IObjectSpace objectSpace, object user) {
            base.InitializeNewUserCore(objectSpace, user);
        }
        public void InitializeNewUser(IObjectSpace objectSpace, object user) {
            InitializeNewUserCore(objectSpace, user);
        }
    }
}
