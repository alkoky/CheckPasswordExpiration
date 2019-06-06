<%@ Page Language="c#" AutoEventWireup="True" Inherits=" Common.Feature.ChangeExpiredPassword.ChangeExpiredPasswordPage" MasterPageFile="~/sitecore/shell/DialogPage.Master" CodeBehind="ChangeExpiredPassword.cs" %>

<%@ Register TagPrefix="sc" TagName="DialogHeader" Src="~/sitecore/shell/DialogHeader.ascx" %>
<%@ OutputCache Location="None" VaryByParam="none" %>


<asp:Content ContentPlaceHolderID="Head" runat="server">

  <meta http-equiv="X-Frame-Options" content="SAMEORIGIN">
  <title>Sitecore</title>
 
  <style>
    body {
      background-color: white;
      color: #5E5E5E;
      line-height: 1.42857143;
    }

    .titleText {
      display: none;
    }

    #ChangePassword {
      width: 100%;
    }

    .scPasswordForm {
      width: 100%;
      text-align: left !important;
    }

      .scPasswordForm input[type=password] {
        width: -webkit-calc(100% - 15px) !important;
        width: calc(100% - 15px) !important;
        display: inline !important;
      }


      .scPasswordForm tr:nth-child(1) td, .scPasswordForm tr:nth-child(2) td, .scPasswordForm tr:nth-child(3) td {
        padding: 5px;
      }

        .scPasswordForm tr:nth-child(1) td:first-child, .scPasswordForm tr:nth-child(2) td:first-child, .scPasswordForm tr:nth-child(3) td:first-child {
          vertical-align: top;
          padding-top: 14px;
          padding-left: 0;
        }

      .scPasswordForm tr:nth-child(4) td {
        text-align: left;
        padding-top: 10px;
      }

      .scPasswordForm input + span {
        color: #ca241c;
        margin-left: 5px;
      }

    .scDialogContentContainer {
      overflow: visible !important;
    }

    .scFormDialogFooter {
      position: absolute !important;
      bottom: 0;
      left: 0;
      right: 0;
    }

    .scErrorPanel {
      margin-top: 15px;
      color: #ca241c;
      line-height: 1.42857143;
    }

    .validationErrors {
      padding: 5px 0 0;
      color: #ca241c !important;
      line-height: 1.42857143;
      text-align: left;
    }
  </style>

</asp:Content>

<asp:Content ContentPlaceHolderID="DialogHeader" runat="server">
  <sc:DialogHeader Name="Change Password" Description="Enter your current password and the new password that you want to use." Icon="Control/32x32/edit_mask.png" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
  <asp:ChangePassword ID="ChangePassword" ClientIDMode="Static" runat="server"
    DisplayUserName="true" TitleTextStyle-CssClass="titleText" SuccessPageUrl="Login/" >
    <ChangePasswordButtonStyle CssClass="scButton scButtonPrimary"  />
    <ContinueButtonStyle CssClass="scButton scButtonPrimary" />
    <CancelButtonStyle CssClass="scButton" />
    <InstructionTextStyle />
    <LabelStyle HorizontalAlign="Left" CssClass="label" />
    <TextBoxStyle CssClass="textBox" />
    <ValidatorTextStyle CssClass="validationErrors" />
    <FailureTextStyle HorizontalAlign="Left" CssClass="validationErrors" />
  </asp:ChangePassword>

  <script type="text/javascript">
    document.observe('dom:loaded', function () {
      var footerOkCancel = $$('.footerOkCancel')[0];
      if ($('ChangePasswordPushButton')) {
        Element.addClassName($$('table table')[0], 'scPasswordForm');
        Element.insert(footerOkCancel, { bottom: $('ChangePasswordPushButton').remove() });
        Element.insert(footerOkCancel, { bottom: $('CancelPushButton').remove() });
        Element.remove($$('.scPasswordForm tr:first-child')[0]);
        Element.remove($$('.scPasswordForm tr:last-child')[0]);
      } else {
          Element.insert(footerOkCancel, { bottom: $('ContinuePushButton').remove() });
          window.location.replace('login.aspx');
      }
    });
  </script>
</asp:Content>
