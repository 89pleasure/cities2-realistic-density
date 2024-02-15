using RealisticDensity.Models;
using RealisticDensity.Systems;
using Gooee.Plugins;
using Gooee.Plugins.Attributes;
using Unity.Entities;

namespace RealisticDensity.Controllers
{
    public class RealisticDensityController : Controller<RealisticDensityModel>
    {
        RealisticDensityUISystem m_RealisticDensityUISystem;
        public override RealisticDensityModel Configure()
        {
            m_RealisticDensityUISystem = World.GetOrCreateSystemManaged<RealisticDensityUISystem>();
            RealisticDensityModel model = m_RealisticDensityUISystem.m_ModSettings;
            model.Translations = m_RealisticDensityUISystem.m_SettingLocalization;
            model.Version = MyPluginInfo.PLUGIN_VERSION;

            return model;
        }

        [OnTrigger]
        public void DoSave()
        {
            var settings = m_RealisticDensityUISystem.m_RealisticDensitySystem.m_LocalSettings;
            settings.ModSettings = Model;
            settings.Save();
        }
    }
}
