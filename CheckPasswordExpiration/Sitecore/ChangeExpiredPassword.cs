using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Web;
using Sitecore.Web.UI.HtmlControls;
using System;
 
using System.Web.UI.WebControls;

namespace Common.Feature.ChangeExpiredPassword 
{
     public class ChangeExpiredPasswordPage : Page
    {
        protected ChangePassword ChangePassword;

        private void ChangePassword_ContinueButtonClick(object sender, EventArgs e)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(e, "e");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "close", "top.dialogClose();", true);
        }

        protected override void OnLoad(EventArgs e)
        {
           // ShellPage.IsLoggedIn();
            base.OnLoad(e);
            this.ChangePassword.ContinueButtonClick += new EventHandler(this.ChangePassword_ContinueButtonClick);
            this.ChangePassword.CancelButtonClick += new EventHandler(this.ChangePassword_ContinueButtonClick);

            ChangePassword.UserName = Request.QueryString["UserName"];

            this.TranslateChangePasswordControl(this.ChangePassword);
        }

        private void TranslateChangePasswordControl(ChangePassword control)
        {
            control.CancelButtonText = Translate.Text(control.CancelButtonText);
            control.ChangePasswordButtonText = Translate.Text("Change password");
            control.ChangePasswordFailureText = Translate.Text(control.ChangePasswordFailureText);
            control.ChangePasswordTitleText = Translate.Text(control.ChangePasswordTitleText);
            control.ConfirmNewPasswordLabelText = Translate.Text("Confirm new password:");
            control.ContinueButtonText = Translate.Text(control.ContinueButtonText);
            control.NewPasswordLabelText = Translate.Text("New password:");
            control.PasswordLabelText = Translate.Text("Current password:");
            control.SuccessText = Translate.Text(control.SuccessText);
            control.SuccessTitleText = Translate.Text(control.SuccessTitleText);
            control.UserNameLabelText = Translate.Text(control.UserNameLabelText);
            control.ConfirmPasswordCompareErrorMessage = Translate.Text(control.ConfirmPasswordCompareErrorMessage);
            control.NewPasswordRegularExpressionErrorMessage = Translate.Text(control.NewPasswordRegularExpressionErrorMessage);
            control.ConfirmPasswordRequiredErrorMessage = Translate.Text(control.ConfirmPasswordRequiredErrorMessage);
            control.NewPasswordRequiredErrorMessage = Translate.Text(control.NewPasswordRequiredErrorMessage);
            control.PasswordRequiredErrorMessage = Translate.Text(control.PasswordRequiredErrorMessage);
            control.UserNameRequiredErrorMessage = Translate.Text(control.UserNameRequiredErrorMessage);
        }
    }
}