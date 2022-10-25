using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Utils;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace MySolution.Module.Web.Controllers {
    public class LogonAuthController : ViewController<DetailView> {
        public const string OAuthParameter = "oauth";
        private SimpleAction microsoftAction;

        public LogonAuthController() {
            TargetObjectType = typeof(AuthenticationStandardLogonParameters);
            microsoftAction = new SimpleAction(this, "LoginWithMicrosoft", "OAuthActions");
            microsoftAction.Execute += microsoftAccountAction_Execute;
            microsoftAction.Caption = "Microsoft";
        }
        protected override void OnActivated() {
            base.OnActivated();
            LogonController logonController = Frame.GetController<LogonController>();
            if (logonController != null) {
                logonController.AcceptAction.Changed += AcceptAction_Changed;
            }
        }
        private void Challenge(string provider) {
            string redirectUrl = WebApplication.LogonPage + "?oauth=true";
            AuthenticationProperties properties = new AuthenticationProperties();
            properties.RedirectUri = redirectUrl;
            HttpContext.Current.GetOwinContext().Authentication.Challenge(properties, provider);
        }
        private void microsoftAccountAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            Challenge(OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }
        private IList<string> GetProviderNames() {
            IList<AuthenticationDescription> descriptions = HttpContext.Current.GetOwinContext().Authentication.GetAuthenticationTypes((AuthenticationDescription d) => d.Properties != null && d.Properties.ContainsKey("Caption")) as IList<AuthenticationDescription>;
            List<string> providersNames = new List<string>();
            foreach (AuthenticationDescription description in descriptions) {
                providersNames.Add(description.AuthenticationType);
            }
            return providersNames;
        }
        private void CurrentRequestPage_Load(object sender, System.EventArgs e) {
            ((Page)sender).Load -= CurrentRequestPage_Load;
            LogonController logonController = Frame.GetController<LogonController>();
            if (logonController != null && logonController.AcceptAction.Active) {
                ((ISupportMixedAuthentication)Application.Security).AuthenticationMixed.SetupAuthenticationProvider("OAuthProvider");
                logonController.AcceptAction.DoExecute();
            }
        }
        private void AcceptAction_Changed(object sender, ActionChangedEventArgs e) {
            if (e.ChangedPropertyType == ActionChangedType.Active) {
                SetActionsActive(((ActionBase)sender).Active);
            }
        }
        private void SetActionsActive(bool logonActionActive) {
            foreach (ActionBase action in Actions) {
                action.Active["LogonActionActive"] = logonActionActive;
            }
            RegisterVisibleUserExistingTextScript(logonActionActive);
        }
        private void RegisterVisibleUserExistingTextScript(bool visible) {
            ((WebWindow)Frame).RegisterClientScript("LogonActionActive",
                        string.Format("SetVisibleUserExistingText({0});", ClientSideEventsHelper.ToJSBoolean(visible)), true);
        }
        protected override void OnViewControlsCreated() {
            LogonController logonController = Frame.GetController<LogonController>();
            if (logonController != null) {
                SetActionsActive(logonController.AcceptAction.Active);
            }

            IList<string> providersName = GetProviderNames() as IList<string>;
            if (providersName.Count == 0) {
                RegisterVisibleUserExistingTextScript(false);
            }
            microsoftAction.Active["ProviderIsSet"] = providersName.Contains(OpenIdConnectAuthenticationDefaults.AuthenticationType);

            if (IsOAuthRequest && WebWindow.CurrentRequestPage != null) {
                WebWindow.CurrentRequestPage.Load += CurrentRequestPage_Load;
            }
            base.OnViewControlsCreated();
        }
        public static bool IsOAuthRequest {
            get {
                return HttpContext.Current.Request.Params[OAuthParameter] != null;
            }
        }
        protected override void OnDeactivated() {
            LogonController logonController = Frame.GetController<LogonController>();
            if (logonController != null) {
                logonController.AcceptAction.Changed -= AcceptAction_Changed;
            }
            base.OnDeactivated();
        }
    }
}
