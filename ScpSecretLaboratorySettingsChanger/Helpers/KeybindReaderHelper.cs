namespace ScpSecretLaboratorySettingsChanger.Helpers
{
    public static class KeybindReaderHelper
    {
        public static Dictionary<string, object> Keybinds = new Dictionary<string, object>();
        public static string FileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory", "keybinding.txt");

        public static void RealAllKeybinds()
        {
            string[] keybindCombos = File.ReadAllText(FileLocation).Split(';');
            foreach (string keyCombo in keybindCombos)
            {
                string[] keyInput = keyCombo.Split(':');
                Keybinds.Add(keyInput[0], keyInput[1]);
            }
        }
    }
}
