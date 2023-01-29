using System.ComponentModel;

namespace ScpSecretLaboratorySettingsChanger.Helpers
{
    public static class RegistryReaderHelper
    {
        public static Dictionary<string, object> RegistryKeys = new Dictionary<string, object>();
        public static string FileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory", "registry.txt");

        public static void ReadAllRegistryKeys()
        {
            try
            {
                string[] keys = File.ReadAllLines(FileLocation);
                foreach (string registryKey in keys)
                {
                    string key = registryKey.Substring(0, registryKey.LastIndexOf(':') + 1);
                    string value = registryKey.Substring(registryKey.LastIndexOf(":") + 1);
                    RegistryKeys.Add(key, value);
                }
            }
            catch
            {

            }
        }

        public static void WriteAllRegistryKeys()
        {
            string[] keys = new string[RegistryKeys.Count];
            int index = 0;
            foreach (KeyValuePair<string, object> pair in RegistryKeys)
            {
                keys[index] = pair.Key + pair.Value;
                index++;
            }
            File.WriteAllLines(FileLocation, keys);
        }

        public static void SaveRegistryKeyValue(string key, object value)
        {
            if (RegistryKeys.ContainsKey(key))
            {
                RegistryKeys[key] = value;
            }
            else
            {
                RegistryKeys.Add(key, value);
            }
        }

        public static T? ReadRegistryKeyValue<T>(string key)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                if (RegistryKeys.ContainsKey(key) && !string.IsNullOrWhiteSpace(RegistryKeys[key].ToString()) && converter != null)
                {
                    return (T)converter.ConvertFromString(RegistryKeys[key].ToString());
                }
                return default;
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }
    }
}
