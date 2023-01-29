using SFML.Window;

namespace ScpSecretLaboratorySettingsChanger.Helpers
{
    public static class DisplaySettingsHelper
    {

        public static string[] GetAllResolutions()
        {
            VideoMode[] modes = VideoMode.FullscreenModes;
            string[] resolutionList = new string[modes.Length];
            for (int i = 0; i < modes.Length; i++)
            {
                resolutionList[i] = $"{modes[modes.Length - 1 - i].Width} x {modes[modes.Length - 1 - i].Height}";
            }
            return resolutionList;
        }
    }
}
