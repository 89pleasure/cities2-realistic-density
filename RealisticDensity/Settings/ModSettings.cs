using RealisticDensity.Models;
using RealisticDensity.Systems;

namespace RealisticDensity.Settings
{
    public class ModSettings
    {
        public string Version { get; } = MyPluginInfo.PLUGIN_VERSION;

        public bool DisableMod { get; set; } = false;
        public bool SpawnablesEnabled { get; set; } = true;
        public bool CommercialsEnabled { get; set; } = true;
        public float CommercialsFactor { get; set; } = WorkforceFactors.Commercial.Medium;
        public bool OfficesEnabled { get; set; } = true;
        public float OfficesFactor { get; set; } = WorkforceFactors.Office.Medium;
        public bool IndustriesEnabled { get; set; } = true;
        public float IndustryExtractorFactor { get; set; } = WorkforceFactors.IndustryExtractor.Medium;
        public float IndustryProcessingFactor { get; set; } = WorkforceFactors.IndustryProcessing.Medium;
        public float IndustrySellingFactor { get; set; } = WorkforceFactors.IndustrySelling.Medium;
        public bool IndustryIncreaseStorageCapacity { get; set; } = false;
        public bool IndustryIncreaseMaxTransports { get; set; } = true;
        public bool CityServicesEnabled { get; set; } = true;

        // CONVERSIONS TO EXTENDED TOOLTIP MODEL
        public static implicit operator RealisticDensityModel(ModSettings settings)
        {
            var destination = new RealisticDensityModel();
            foreach (var sourceProperty in settings.GetType().GetProperties())
            {
                var destinationProperty = destination.GetType().GetProperty(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(settings));
                }
            }

            return destination;
        }
    }
}
