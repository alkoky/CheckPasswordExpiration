using System;
using System.ComponentModel;
using System.Web.Security;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.LoggingIn;
using Sitecore.Security.Accounts;
using Sitecore.Data.Items;
using Sitecore;
using Sitecore.Security.Authentication;

namespace Common.Feature.Pipelines.LoggingIn
{
    public class CheckPasswordExpiration : CheckStartPage
    {
        private TimeSpan TimeSpanToExpirePassword { get; set; }
        private string ChangePasswordPageUrl { get; set; }
        private string UserName { get; set; }

        private string GetErrorMessage(NoAccessTo accessTo)
        {
            switch (accessTo)
            {
                case NoAccessTo.Desktop:
                    return "Your login was successful, but you do not have access to the \"Desktop\".";
                case NoAccessTo.ContentEditor:
                    return "Your login was successful, but you do not have access to the \"Content Editor\".";
                case NoAccessTo.PageEditor:
                    return "Your login was successful, but you do not have access to the \"Experience Editor\" mode.";
                case NoAccessTo.PasswordExpired:
                    return $"<a href=\"changepassword.aspx?UserName={UserName}\">Your login was successful, but Your password is expired, please click here to change your password.</a>";
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private enum NoAccessTo
        {
            Desktop,
            ContentEditor,
            PageEditor,
            Empty,
            PasswordExpired,
        }
        public new void Process(LoggingInArgs args)
        {

            Assert.ArgumentNotNull(args, "args");
            bool flag = false;
            NoAccessTo noAccessTo = NoAccessTo.Empty;
            using (UserSwitcher userSwitcher = new UserSwitcher(args.Username, true))
            {
                switch (args.StartUrl)
                {
                    case "/sitecore/shell/default.aspx":
                    {
                        Item item = Client.CoreDatabase.Items["/sitecore/content/Applications/Desktop"];
                        if (!Context.IsAdministrator && (item == null || !item.Access.CanRead()))
                        {
                            noAccessTo = NoAccessTo.Desktop;
                            flag = true;
                        }

                        break;
                    }

                    case "/sitecore/shell/applications/clientusesoswindows.aspx":
                    {
                        Item item1 = Client.CoreDatabase.Items["/sitecore/content/Applications/Content Editor"];
                        if (item1 == null || !item1.Access.CanRead())
                        {
                            noAccessTo = NoAccessTo.ContentEditor;
                            flag = true;
                        }

                        break;
                    }

                    case "/sitecore/shell/applications/webedit.aspx":
                    {
                        Item item2 = Client.CoreDatabase.Items["/sitecore/content/Applications/WebEdit"];
                        if (item2 == null || !item2.Access.CanRead())
                        {
                            noAccessTo = NoAccessTo.PageEditor;
                            flag = true;
                        }

                        break;
                    }
                }

                var membershipUser = Membership.GetUser(Sitecore.Context.User.Name, false);

                if (IsPasswordExpiredEnabled() && membershipUser != null && HasPasswordExpired(membershipUser))
                {
                    noAccessTo = NoAccessTo.PasswordExpired;
                    UserName = args.Username;

                    flag = true;
                }

                if (flag)
                {
                    AuthenticationHelper authenticationHelper = new AuthenticationHelper(AuthenticationManager.Provider);
                    if (!string.IsNullOrEmpty(args.Username) && args.Password != null && authenticationHelper.ValidateUser(args.Username, args.Password))
                    {
                        args.Success = false;
                        args.AddMessage(GetErrorMessage(noAccessTo));
                        args.AbortPipeline();
                    }
                }
            }
       }

       
        private bool IsPasswordExpiredEnabled()
        {
            return IsTimeSpanToExpirePasswordSet() && IsChangePasswordPageUrlSet();
        }

        private bool IsTimeSpanToExpirePasswordSet()
        {
            return TimeSpanToExpirePassword > default(TimeSpan);
        }

        private bool IsChangePasswordPageUrlSet()
        {
            return !string.IsNullOrWhiteSpace(ChangePasswordPageUrl);
        }

        private bool HasPasswordExpired(MembershipUser user)
        {
            return user.LastPasswordChangedDate.Add(TimeSpanToExpirePassword) <= DateTime.Now;
        }

      
    }
}