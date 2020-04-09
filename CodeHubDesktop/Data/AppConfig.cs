using HandyControl.Data;
using System;

namespace CodeHubDesktop
{
    internal class AppConfig
    {
        public static readonly string SavePath = $"{AppDomain.CurrentDomain.BaseDirectory}AppConfig.json";

        public string Lang { get; set; } = "fa-IR";
        public bool IsModernStyle { get; set; } = false;
        public bool StoreSnippet { get; set; } = false;
        public SkinType Skin { get; set; } = SkinType.Default;
    }
}