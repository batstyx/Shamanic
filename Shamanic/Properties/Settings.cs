using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Shamanic.Properties
{
    public class Setting
    {
        public Setting() { }
        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public sealed partial class Settings
    {
        private int GetDefaultIntValue(string key) => int.Parse(Properties[key].DefaultValue.ToString());
        public int DefaultPlayerLeft => GetDefaultIntValue("PlayerLeft");
        public int DefaultPlayerTop => GetDefaultIntValue("PlayerTop");
        public int DefaultOpponentLeft => GetDefaultIntValue("OpponentLeft");
        public int DefaultOpponentTop => GetDefaultIntValue("OpponentTop");
        public void ResetPlayerPosition()
        {
            PlayerLeft = DefaultPlayerLeft;
            PlayerTop = DefaultPlayerTop;
        }
        public void ResetOpponentPosition()
        {
            OpponentLeft = DefaultOpponentLeft;
            OpponentTop = DefaultOpponentTop;
        }

        private static readonly string Filename = Path.ChangeExtension(LibraryInfo.Name, ".xml");
        internal static string DataDir => Config.Instance.DataDir;
        private static string SettingsPath => Path.Combine(DataDir, Filename);

        public bool HasChanges { get; private set; }

        public Settings()
        {
            var provider = Providers;

            SettingsLoaded += SettingsLoadedEventHandler;
            SettingsSaving += SettingsSavingEventHandler;
        }

        private void SettingsLoadedEventHandler(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            try
            {
                Log.Debug($"Loading {SettingsPath}");

                if (File.Exists(SettingsPath))
                {
                    var actual = XmlManager<List<Setting>>.Load(SettingsPath);

                    foreach (var setting in actual)
                    {
                        try
                        {
                            if (Properties[setting.Name].PropertyType.IsEnum)
                                this[setting.Name] = Enum.Parse(Properties[setting.Name].PropertyType, setting.Value);
                            else
                                this[setting.Name] = Convert.ChangeType(setting.Value, Properties[setting.Name].PropertyType);
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"{setting.Name} loading error");
                            Log.Error(ex);
                        }
                    }                   
                    Log.Info($"{SettingsPath} Loaded");
                }
                else
                {
                    Log.Warn($"{SettingsPath} does not exist");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            PropertyChanged += (s, a) => HasChanges = true;
            Log.Info($"Watching {SettingsPath} changes");
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Log.Debug($"Saving {SettingsPath}");

                var saveFormat = PropertyValues.Cast<SettingsPropertyValue>()
                    .Where(p => p.SerializedValue.ToString() != p.Property.DefaultValue.ToString())
                    .Select(p => new Setting(p.Name, p.SerializedValue.ToString()))
                    .ToList();

                XmlManager<List<Setting>>.Save(SettingsPath, saveFormat);

                HasChanges = false;

                e.Cancel = true;

                Log.Info($"{SettingsPath} Saved");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
