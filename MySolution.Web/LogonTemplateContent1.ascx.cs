using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web.Controls;
using DevExpress.ExpressApp.Web.Templates;
using System;
using System.Collections.Generic;

namespace MySolution.Web {
    public partial class LogonTemplateContent1 : TemplateContent, IXafPopupWindowControlContainer, IHeaderImageControlContainer {
        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            LogoLink.NavigateUrl = Request.ApplicationPath;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
        }
        public override void SetStatus(ICollection<string> statusMessages) {
        }
        public override IActionContainer DefaultContainer {
            get { return null; }
        }
        public override object ViewSiteControl {
            get { return viewSiteControl; }
        }
        public XafPopupWindowControl XafPopupWindowControl {
            get { return PopupWindowControl; }
        }
        public override void BeginUpdate() {
            base.BeginUpdate();
            this.PopupActions.BeginUpdate();
        }
        public override void EndUpdate() {
            this.PopupActions.EndUpdate();
            base.EndUpdate();
        }
        public ThemedImageControl HeaderImageControl { get { return TIC; } }
    }
}
