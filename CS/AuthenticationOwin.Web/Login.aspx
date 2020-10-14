<%@ Page Language="C#" AutoEventWireup="true" Inherits="LoginPage" EnableViewState="false" CodeBehind="Login.aspx.cs" %>

<%@ Register Assembly="DevExpress.ExpressApp.Web.v18.2, Version=18.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers" TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v18.2, Version=18.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Templates.Controls" TagPrefix="tc" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v18.2, Version=18.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v18.2, Version=18.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Logon</title>
    <style>
        .LogonTemplate .UPOAuth .menuButtons .dxm-item {
            background-color: white !important;
            color: #2C86D3 !important;
            border: 1px solid #d3d3d3 !important;
        }

            .LogonTemplate .UPOAuth .menuButtons .dxm-item .dx-vam {
                color: #2C86D3 !important;
            }

            .LogonTemplate .UPOAuth .menuButtons .dxm-item.dxm-hovered {
                background-color: white !important;
                color: #2C86D3 !important;
                border: 1px solid #d3d3d3 !important;
            }

        .LogonTemplate .UPOAuth .menuButtons.menuButtons_XafTheme .dxm-item a.dx {
            padding: 7px 21px 7px 21px !important;
            width: 105px;
        }

        .LogonTemplate .UPOAuth .dxm-spacing {
            padding: 0 !important;
        }

        .LogonTemplate .UPOAuth .menuButtons.menuButtons_XafTheme .dxm-item a.dx .dx-vam {
            padding-left: 5px;
        }

        .LogonTemplate .UPOAuth .menuActionImageSVG .dxm-image,
        .LogonTemplate .UPOAuth .dxm-popup .menuActionImageSVG .dxm-image,
        .LogonTemplate .UPOAuth .smallImage2 .dxm-image,
        .LogonTemplate .UPOAuth .dxm-popup .smallImage2 .dxm-image {
            padding: 3px 4px 3px 4px !important;
        }

        .LogonTemplate .UPOAuth .menuButtons.menuButtons_XafTheme .dxm-item.dxm-hovered a.dx {
            color: #2C86D3 !important;
            background-color: #F0F0F0 !important;
            background-image: none;
        }

        .LogonTemplate .UPOAuth .menuButtons .dxm-item {
            padding-left: 0px !important;
            padding-right: 0px !important;
            float: left;
            margin: 8px 8px 0 0;
        }

            .LogonTemplate .UPOAuth .menuButtons .dxm-item.LoginWithGoogle,
            .LogonTemplate .UPOAuth .menuButtons.menuButtons_XafTheme .dxm-item.LoginWithGoogle.dxm-hovered a.dx,
            .LogonTemplate .UPOAuth .menuButtons .dxm-item.LoginWithGoogle .dx-vam {
                background-color: #EA4335 !important;
                color: #fff !important;
                border: none !important;
            }

            .LogonTemplate .UPOAuth .menuButtons .dxm-item.LoginWithFacebook,
            .LogonTemplate .UPOAuth .menuButtons.menuButtons_XafTheme .dxm-item.LoginWithFacebook.dxm-hovered a.dx,
            .LogonTemplate .UPOAuth .menuButtons .dxm-item.LoginWithFacebook .dx-vam {
                background-color: #3D5A98 !important;
                color: #fff !important;
                border: none !important;
            }

            .LogonTemplate .UPOAuth .menuButtons .dxm-item.LoginWithMicrosoft,
            .LogonTemplate .UPOAuth .menuButtons.menuButtons_XafTheme .dxm-item.LoginWithMicrosoft.dxm-hovered a.dx,
            .LogonTemplate .UPOAuth .menuButtons .dxm-item.LoginWithMicrosoft .dx-vam {
                background-color: #27B0E5 !important;
                color: #fff !important;
                border: none !important;
            }

        .StaticText {
            color: #9a9a9a;
            font-weight: bold;
            font-size: 17px;
        }
    </style>
    <script>
        function SetVisibleUserExistingText(visible) {
            function SetVisible() {
                if (visible) {
                    document.getElementById('UseExisting').style.display = 'table';
                } else {
                    document.getElementById('UseExisting').style.display = 'none';
                }
            }
            if (document.getElementById('UseExisting')) {
                SetVisible();
            } else {
                document.addEventListener("DOMContentLoaded", function () { SetVisible(); });
            }
        }
    </script>
</head>
<body class="Dialog">
    <div id="PageContent" class="PageContent DialogPageContent">
        <form id="form1" runat="server">
            <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
            <div id="Content" runat="server" />
        </form>
    </div>
</body>
</html>
