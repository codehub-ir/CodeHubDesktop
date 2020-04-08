using CodeHubDesktop.Language;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CodeHubDesktop
{
    internal class GlobalData
    {
        public static void Init()
        {
            if (File.Exists(AppConfig.SavePath))
            {
                try
                {
                    string json = File.ReadAllText(AppConfig.SavePath);
                    Config = (string.IsNullOrEmpty(json) ? new AppConfig() : JsonConvert.DeserializeObject<AppConfig>(json)) ?? new AppConfig();

                }
                catch
                {
                    Config = new AppConfig();
                }
            }
            else
            {
                Config = new AppConfig();
            }
        }

        public static void Save()
        {
            try
            {

                string json = JsonConvert.SerializeObject(Config);
                File.WriteAllText(AppConfig.SavePath, json);
            }
            catch (UnauthorizedAccessException)
            {
                HandyControl.Controls.MessageBox.Error(Lang.ResourceManager.GetString("AdminError"), Lang.ResourceManager.GetString("AdminErrorTitle"));
            }
        }

        public static AppConfig Config { get; set; }

    }
}