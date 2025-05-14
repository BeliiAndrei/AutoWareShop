using System.Web.Optimization;

namespace WebShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region ScriptBundles

            bundles.Add(new ScriptBundle("~/assets/js/all").Include(
                "~/assets/js/libs.min.js",
                "~/assets/js/main.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/scripts/jquery-3.6.0.js",
                "~/scripts/jquery-migrate-3.3.2.js"
            ));

            #endregion

            #region StyleBundles

            bundles.Add(new StyleBundle("~/assets/css/all").Include(
                "~/assets/css/styles.min.css"
                ));

            #endregion

        }
    }
}
