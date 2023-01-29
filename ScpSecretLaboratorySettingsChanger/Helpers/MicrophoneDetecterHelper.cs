using NAudio.CoreAudioApi;

namespace ScpSecretLaboratorySettingsChanger.Helpers
{
    public static class MicrophoneDetecterHelper
    {
        public static string[] GetReadyMicrophones()
        {
            MMDeviceCollection endpoints = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            string[] microphones = new string[endpoints.Count];
            int index = 0;

            foreach (MMDevice endpoint in endpoints) 
            {
                microphones[index] = endpoint.DeviceFriendlyName;
                index++;
            }

            return microphones;
        }
    }
}
