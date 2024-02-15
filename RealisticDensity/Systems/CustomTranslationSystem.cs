﻿using Game;
using Game.SceneFlow;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace RealisticDensity.Systems
{
    public class CustomTranslationSystem: GameSystemBase
    {
        public string Prefix { get; set; } = MyPluginInfo.PLUGIN_NAME.Trim().ToLower();
        private string LanguageCode { get; set; }
        public string CurrentLanguageCode => LanguageCode;
        private JsonDocument Translations { get; set; }
        public static string FrameworkDescription { get; }

        protected override void OnCreate()
        {
            base.OnCreate();
            LanguageCode = GameManager.instance.localizationManager.activeLocaleId;
            UnityEngine.Debug.Log("CustomTranslationSystem created.");
            LoadCustomTranslations();
        }

        protected override void OnUpdate()
        {
        }

        public void ReloadTranslations(string locale)
        {
            LanguageCode = locale;
            UnityEngine.Debug.Log("Reloading translations.");
            LoadCustomTranslations();
        }

        private void LoadCustomTranslations()
        {
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string directory = Path.GetDirectoryName(assemblyPath);
            string langPackZip = Path.Combine(directory, "language_pack.data");

            // Load JSON file based on language code
            try
            {
                if (!File.Exists(langPackZip))
                {
                    UnityEngine.Debug.Log($"Language pack not found at {langPackZip}.");
                    return;
                }

                UnityEngine.Debug.Log($"Language pack found at {langPackZip}.");

                using ZipArchive zipArchive = ZipFile.OpenRead(langPackZip);
                foreach (ZipArchiveEntry entry in zipArchive.Entries)
                {
                    if (entry.FullName.StartsWith(LanguageCode) && entry.FullName.EndsWith(".json"))
                    {
                        using StreamReader languageStream = new(entry.Open(), Encoding.UTF8);
                        string languageContent = languageStream.ReadToEnd();

                        Translations = JsonDocument.Parse(languageContent);
                        UnityEngine.Debug.Log($"Successfully loaded custom translations for {LanguageCode}.");

                        return;
                    }
                }

            } catch (Exception e)
            {
                UnityEngine.Debug.Log($"Failed to load custom translations.");
                UnityEngine.Debug.Log(e.Message);
            }
        }

        public string GetTranslation(string key, string fallback = "Translation missing.", params string[] vars)
        {
            if (Translations == null || !Translations.RootElement.TryGetProperty($"{Prefix}.{key}", out var rawTranslationString))
            {
                return fallback;
            }

            return HandleTranslationString(rawTranslationString.GetString(), fallback, vars);
        }

        public string GetLocalGameTranslation(string id, string fallback = "Translation failed.", params string[] vars)
        {
            if (GameManager.instance == null || !GameManager.instance.localizationManager.activeDictionary.TryGetValue(id, out string translatedText))
            {
                return fallback;
            }

            return HandleTranslationString(translatedText, fallback, vars);
        }

        private string HandleTranslationString(string translation, string fallback, params string[] vars)
        {
            if (vars != null && vars.Length > 0)
            {
                if (vars.Length % 2 != 0)
                {
                    UnityEngine.Debug.Log("Invalid amount of arguments. It must be a even number.");
                    return fallback;
                }

                for (int i = 0; i < vars.Length; i += 2)
                {
                    string placeholder = vars[i];
                    string value = vars[i + 1];

                    translation = translation.Replace("{" + placeholder + "}", value);
                }

                return translation;
            }

            return translation;
        }
    }
}