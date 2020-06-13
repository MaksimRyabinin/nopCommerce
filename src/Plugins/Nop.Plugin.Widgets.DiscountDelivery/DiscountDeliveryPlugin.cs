using System.Collections.Generic;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.DiscountDelivery
{
    public class DiscountDeliveryPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;

        public DiscountDeliveryPlugin(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsDiscountDelivery";
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.ProductDetailsAfterBreadcrumb };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsDiscountDelivery/Configure";
        }

        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {
            base.Uninstall();
        }
    }
}
