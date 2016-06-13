﻿namespace Common.Configuration
{
    public static class GlobalConfigs
    {
        const string userTokenConfigName = "userTokenSetting";
        const string nsfwPreferenceConfigName = "nsfwSetting";
        const string playTokenConfigName = "playTokenSetting";

        static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public static string UserToken
        {
            get
            {
                object val = localSettings.Values[userTokenConfigName];
                if (val == null)
                    return string.Empty;
                else
                    return (string)val;
            }
            set { localSettings.Values[userTokenConfigName] = value; }
        }

        public static bool NsfwPerference
        {
            get
            {
                object val = localSettings.Values[nsfwPreferenceConfigName];
                if (val == null)
                    return false;
                else
                    return (bool)val;
            }
            set { localSettings.Values[nsfwPreferenceConfigName] = value; }
        }

        public static string PlayToken
        {
            get
            {
                object val = localSettings.Values[playTokenConfigName];
                if (val == null)
                    return string.Empty;
                else
                    return (string)val;
            }
            set { localSettings.Values[playTokenConfigName] = value; }
        }
    }
}
