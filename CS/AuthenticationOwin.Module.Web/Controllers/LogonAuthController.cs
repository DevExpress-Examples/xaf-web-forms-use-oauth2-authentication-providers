using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.ExpressApp.Web.Utils;
using Microsoft.Owin.Security;

namespace AuthenticationOwin.Module.Web.Controllers {
    public class LogonAuthController : ViewController<DetailView> {
        public const string OAuthParameter = "oauth";

        private SimpleAction googleAction;
        private SimpleAction facebookAction;
        private SimpleAction microsoftAction;

        private void Challenge(string provider) {
            string redirectUrl = WebApplication.LogonPage + "?oauth=true";
            AuthenticationProperties properties = new AuthenticationProperties();
            properties.RedirectUri = redirectUrl;
            properties.Dictionary["Provider"] = provider;
            HttpContext.Current.GetOwinContext().Authentication.Challenge(properties, provider);
        }
        private void facebookAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            Challenge("Facebook");
        }
        private void googleAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            Challenge("Google");
        }
        private void microsoftAccountAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            Challenge("Microsoft");
        }
        private IList<string> GetProviderNames() {
            IList<AuthenticationDescription> descriptions = HttpContext.Current.GetOwinContext().Authentication.GetAuthenticationTypes((AuthenticationDescription d) => d.Properties != null && d.Properties.ContainsKey("Caption")) as IList<AuthenticationDescription>;
            List<string> providersNames = new List<string>();
            foreach(AuthenticationDescription description in descriptions) {
                providersNames.Add(description.AuthenticationType);
            }
            return providersNames;
        }
        private void CurrentRequestPage_Load(object sender, System.EventArgs e) {
            ((Page)sender).Load -= CurrentRequestPage_Load;
            LogonController logonController = Frame.GetController<LogonController>();
            if(logonController != null && logonController.AcceptAction.Active) {
                logonController.AcceptAction.DoExecute();
            }
        }
        private void AcceptAction_Changed(object sender, ActionChangedEventArgs e) {
            if(e.ChangedPropertyType == ActionChangedType.Active) {
                SetActionsActive(((ActionBase)sender).Active);
            }
        }
        private void SetActionsActive(bool logonActionActive) {
            foreach(ActionBase action in Actions) {
                action.Active["LogonActionActive"] = logonActionActive;
            }
            RegisterVisibleUserExistingTextScript(logonActionActive);
        }
        private void RegisterVisibleUserExistingTextScript(bool visible) {
            ((WebWindow)Frame).RegisterClientScript("LogonActionActive",
                        string.Format("SetVisibleUserExistingText({0});", ClientSideEventsHelper.ToJSBoolean(visible)), true);
        }
        protected override void OnActivated() {
            base.OnActivated();
            LogonController logonController = Frame.GetController<LogonController>();
            if(logonController != null) {
                logonController.AcceptAction.Changed += AcceptAction_Changed;
            }
        }
        protected override void OnDeactivated() {
            LogonController logonController = Frame.GetController<LogonController>();
            if(logonController != null) {
                logonController.AcceptAction.Changed -= AcceptAction_Changed;
            }
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated() {
            LogonController logonController = Frame.GetController<LogonController>();
            if(logonController != null) {
                SetActionsActive(logonController.AcceptAction.Active);
            }

            IList<string> providersName = GetProviderNames() as IList<string>;
            if(providersName.Count == 0) {
                RegisterVisibleUserExistingTextScript(false);
            }
            googleAction.Active["ProviderIsSet"] = providersName.Contains("Google");
            facebookAction.Active["ProviderIsSet"] = providersName.Contains("Facebook");
            microsoftAction.Active["ProviderIsSet"] = providersName.Contains("Microsoft");

            if(IsOAuthRequest && WebWindow.CurrentRequestPage != null) {
                WebWindow.CurrentRequestPage.Load += CurrentRequestPage_Load;
            }
            base.OnViewControlsCreated();
        }
        public static bool IsOAuthRequest
        {
            get
            {
                return HttpContext.Current.Request.Params[OAuthParameter] != null; // &&
                                                                                   //(HttpContext.Current.Request.UrlReferrer == null || HttpContext.Current.Request.Url.Host != HttpContext.Current.Request.UrlReferrer.Host);
            }
        }
        public LogonAuthController() {
            googleAction = new SimpleAction(this, "LoginWithGoogle", "OAuthActions");
            googleAction.Caption = "Google";
            googleAction.Execute += googleAction_Execute;
            microsoftAction = new SimpleAction(this, "LoginWithMicrosoft", "OAuthActions");
            microsoftAction.Execute += microsoftAccountAction_Execute;
            microsoftAction.Caption = "Microsoft";
            facebookAction = new SimpleAction(this, "LoginWithFacebook", "OAuthActions");
            facebookAction.Caption = "Facebook";
            facebookAction.Execute += facebookAction_Execute;
        }
    }
}