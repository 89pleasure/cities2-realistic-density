﻿using Newtonsoft.Json;
using RealisticDensity.Settings;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTooltip.Settings
{
    public class LocalSettings
    {
        private RealisticDensitySettings m_Settings;
        public RealisticDensitySettings Settings => m_Settings;

        public void Init() => Load();
        public void Reload() => Load();

        /// <summary>
        /// Save settings to a local JSON file
        /// </summary>
        /// <param name="settings"></param>
        public async Task Save()
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filename = "UserSettings.json";
            string fullFilePath = Path.Combine(assemblyDirectory, filename);

            try
            {
                string updatedSettingsJson = JsonConvert.SerializeObject(m_Settings);
                using StreamWriter writer = new(fullFilePath, false, Encoding.UTF8);
                await writer.WriteAsync(updatedSettingsJson);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.Log($"Error saving settings: {e.Message}");
            }
        }

        /// <summary>
        /// Load settings from a local JSON file
        /// </summary>
        private void Load()
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filename = "UserSettings.json";
            string fullFilePath = Path.Combine(assemblyDirectory, filename);

            if (!File.Exists(fullFilePath))
            {
                UnityEngine.Debug.Log("No user settings found. Use default settings.");
                fullFilePath = Path.Combine(assemblyDirectory, "DefaultSettings.json");
                if (!File.Exists(fullFilePath))
                {
                    UnityEngine.Debug.Log($"Error loading settings: {fullFilePath} does not exist.");
                    return;
                }
            }
            else
            {
                UnityEngine.Debug.Log("User settings successfully loaded.");
            }

            try
            {
                // Access settings
                string settingsJson = File.ReadAllText(fullFilePath);
                RealisticDensitySettings localSettingsItem = JsonConvert.DeserializeObject<RealisticDensitySettings>(settingsJson);

                if (localSettingsItem != null)
                {
                    m_Settings = localSettingsItem;
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.Log($"Error loading settings: {e.Message}");
            }
        }
    }
}
