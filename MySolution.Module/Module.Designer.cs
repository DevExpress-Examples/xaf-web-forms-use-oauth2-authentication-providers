﻿namespace MySolution.Module {
	partial class MySolutionModule {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			// 
			// MySolutionModule
			// 
		    this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifference));
		    this.AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
		}

		#endregion
	}
}
