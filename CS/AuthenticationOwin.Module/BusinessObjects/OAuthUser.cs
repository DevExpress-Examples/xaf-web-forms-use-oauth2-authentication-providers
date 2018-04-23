using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AuthenticationOwin.Module.BusinessObjects {
    public class OAuthUser : PermissionPolicyUser, IAuthenticationOAuthUser {
        public bool EnableStandardAuthentication {
            get { return GetPropertyValue<bool>("EnableStandardAuthentication"); }
            set { SetPropertyValue("EnableStandardAuthentication", value); }
        }
        [Association, Aggregated]
        public XPCollection<EmailEntity> OAuthAuthenticationEmails {
            get { return GetCollection<EmailEntity>("OAuthAuthenticationEmails"); }
        }
        public OAuthUser(Session session) : base(session) { }
    }
    public class EmailEntity : BaseObject {
        public EmailEntity(Session session) : base(session) {
        }
        [RuleUniqueValue("Unique_Email", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        public string Email {
            get { return GetPropertyValue<string>("Email"); }
            set { SetPropertyValue("Email", value); }
        }
        [Association]
        public OAuthUser OAuthUser {
            get { return GetPropertyValue<OAuthUser>("OAuthUser"); }
            set { SetPropertyValue("OAuthUser", value); }
        }
    }
}
