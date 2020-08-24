using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationOwin.Module.Security {
    public class CustomAuthenticationStandardProvider : AuthenticationStandardProvider {
        public CustomAuthenticationStandardProvider(Type userType) : base(userType) {
        }
        public override object Authenticate(IObjectSpace objectSpace) {
            IAuthenticationOAuthUser user = base.Authenticate(objectSpace) as IAuthenticationOAuthUser;
            if(user != null && !user.EnableStandardAuthentication) {
                throw new InvalidOperationException("Password authentication is not allowed for this user.");
            }
            return user;
        }
    }
}
