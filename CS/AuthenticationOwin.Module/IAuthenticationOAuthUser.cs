using AuthenticationOwin.Module.BusinessObjects;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationOwin.Module {
    public interface IAuthenticationOAuthUser {
        string UserName { get; set; }
        XPCollection<EmailEntity> OAuthAuthenticationEmails { get; }
        bool EnableStandardAuthentication { get; }
    }
}
