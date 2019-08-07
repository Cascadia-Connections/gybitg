using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace gybitg.Views.Manage
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string EditAthleteProfile => "EditAthleteProfile";

        public static string EditAthleteStats => "EditAthleteStats";

        public static string EditCoachProfile => "EditCoachProfile";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string AthleteList => "AthleteList";

        public static string DeleteProfile => "DeleteProfileNavClass";

        //added by Kevin on 4/15/19
        public static string BasicSearch => "BasicSearch";

        public static string SearchResults => "SearchResults";

        public static string AdvancedSearch => "AdvancedSearch";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string BasicSearchNavClass(ViewContext viewContext) => PageNavClass(viewContext, BasicSearch);

        public static string AdvancedSearchNavClass(ViewContext viewContext) => PageNavClass(viewContext, AdvancedSearch);

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string EditAthleteProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, EditAthleteProfile);

        public static string EditAthleteStatsNavClass(ViewContext viewContext) => PageNavClass(viewContext, EditAthleteStats);

        public static string EditCoachProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, EditCoachProfile);

        public static string AthleteListNavClass(ViewContext viewContext) => PageNavClass(viewContext, AthleteList);
        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        public static string DeleteProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeleteProfile);


        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
