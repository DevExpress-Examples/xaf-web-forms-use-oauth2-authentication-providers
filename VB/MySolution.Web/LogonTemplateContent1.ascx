<%@ Control Language="vb" CodeBehind="LogonTemplateContent1.ascx.vb" ClassName="LogonTemplateContent1" Inherits="MySolution.Web.LogonTemplateContent1" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v21.2, Version=21.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v21.2, Version=21.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v21.2, Version=21.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v21.2, Version=21.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates"
    TagPrefix="xaf" %>

<div class="LogonTemplate">
    <xaf:XafUpdatePanel ID="UPPopupWindowControl" runat="server">
        <xaf:XafPopupWindowControl runat="server" id="PopupWindowControl" />
    </xaf:XafUpdatePanel>
    <xaf:XafUpdatePanel ID="UPHeader" runat="server">
        <div class="white borderBottom width100" id="headerTableDiv">
            <div class="paddings sizeLimit" style="margin: auto">
                <table id="headerTable" class="headerTable xafAlignCenter white width100 sizeLimit" style="height: 60px;">
                    <tbody>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" ID="LogoLink">
                                    <xaf:ThemedImageControl ID="TIC" ImageName="Logo" BorderWidth="0px" runat="server" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </xaf:XafUpdatePanel>

    <div style="top: 25%; width: 100%; position: absolute">
        <table class="LogonMainTable LogonContentWidth">
            <tr>
                <td>
                    <xaf:XafUpdatePanel ID="UPEI" runat="server">
                        <xaf:ErrorInfoControl ID="ErrorInfo" Style="margin: 10px 0px 10px 0px" runat="server" />
                    </xaf:XafUpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="LogonContent LogonContentWidth">
                        <tr>
                            <td class="LogonContentCell">
                                <xaf:XafUpdatePanel ID="UPVSC" runat="server">
                                    <xaf:ViewSiteControl ID="viewSiteControl" runat="server" />
                                </xaf:XafUpdatePanel>

                                <xaf:XafUpdatePanel ID="UPPopupActions" runat="server" CssClass="right">
                                    <xaf:ActionContainerHolder ID="PopupActions" runat="server" Orientation="Horizontal" ContainerStyle="Buttons">
                                        <Menu width="100%" ItemAutoWidth="False" />
                                        <ActionContainers>
                        <xaf:WebActionContainer ContainerId="PopupActions" />
                    </ActionContainers>
                                    </xaf:ActionContainerHolder>
                                </xaf:XafUpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="UseExisting" class="StaticText width100" style="margin: 50px 0 20px;">
                                    <tr>
                                        <td style="min-width: 130px;">or use existing</td>
                                        <td class="width100" style="padding-top: 7px;"><hr></td>
                                    </tr>
                                </table>
                                <xaf:XafUpdatePanel ID="XafUpdatePanelOAuth" runat="server" CssClass="UPOAuth right">
                                    <xaf:ActionContainerHolder ID="OAuthActions" CssClass="UPOAuthACH" runat="server" Orientation="Horizontal" ContainerStyle="Buttons">
                                        <Menu ID="OAuthMenu" ClientInstanceName="UPOAuthMenu" HorizontalAlign="Left" Width="100%" ItemAutoWidth="False" />
                                        <ActionContainers>
                                            <xaf:WebActionContainer ContainerId="OAuthActions" />
                                        </ActionContainers>
                                    </xaf:ActionContainerHolder>
                                </xaf:XafUpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<script type="text/javascript">
    (function() {
        var mainWindow = xaf.Utils.GetMainWindow();
        mainWindow.pageLoaded = false;
        $(window).on("load", function() {
            var mainWindow = xaf.Utils.GetMainWindow();
            mainWindow.pageLoaded = true;
            PageLoaded();
        });
    })();
</script>