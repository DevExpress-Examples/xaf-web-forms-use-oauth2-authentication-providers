using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace MySolution.Module.BusinessObjects {
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DefaultProperty(nameof(UserName))]
    public class ApplicationUser : PermissionPolicyUser, IObjectSpaceLink, ISecurityUserWithLoginInfo {
        public ApplicationUser(Session session) : base(session) { }

        [Browsable(false)]
        [Aggregated, Association("User-LoginInfo")]
        public XPCollection<ApplicationUserLoginInfo> LoginInfo {
            get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
        }

        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

        IObjectSpace IObjectSpaceLink.ObjectSpace { get; set; }

        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
            ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }
        public bool EnableStandardAuthentication {
            get { return GetPropertyValue<bool>(nameof(EnableStandardAuthentication)); }
            set { SetPropertyValue(nameof(EnableStandardAuthentication), value); }
        }
        [Association, Aggregated]
        public XPCollection<EmailEntity> OAuthAuthenticationEmails {
            get { return GetCollection<EmailEntity>(nameof(OAuthAuthenticationEmails)); }
        }
    }
    public class EmailEntity : BaseObject {
        public EmailEntity(Session session) : base(session) {
        }
        [RuleUniqueValue("Unique_Email", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        public string Email {
            get { return GetPropertyValue<string>(nameof(Email)); }
            set { SetPropertyValue(nameof(Email), value); }
        }
        [Association]
        public ApplicationUser ApplicationUser {
            get { return GetPropertyValue<ApplicationUser>(nameof(ApplicationUser)); }
            set { SetPropertyValue(nameof(ApplicationUser), value); }
        }
    }
}
