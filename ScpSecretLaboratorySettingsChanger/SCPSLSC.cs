using ScpSecretLaboratorySettingsChanger.Helpers;
using SFML.Window;

namespace ScpSecretLaboratorySettingsChanger
{
    public partial class SCPSLSC : Form
    {
        public SCPSLSC()
        {
            InitializeComponent();
        }

        private void SCPSLSC_Load(object sender, EventArgs e)
        {
            FillResolutions();
            FillMicrophones();
            CHK_VSync_CheckedChanged(sender, e);
            CHK_RenderLights_CheckedChanged(sender, e);
            RegistryReaderHelper.ReadAllRegistryKeys();
            LoadSettings();
        }

        private void FillResolutions()
        {
            foreach (string resolution in DisplaySettingsHelper.GetAllResolutions())
            {
                CBX_ScreenResolution.Items.Add(resolution);
            }
        }

        private void FillMicrophones()
        {
            foreach (string microphone in MicrophoneDetecterHelper.GetReadyMicrophones())
            {
                CBX_InputDevice.Items.Add(microphone);
            }
        }

        private void SCPSLSC_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void BTN_SaveSettings_Click(object sender, EventArgs e)
        {
            // Video
            RegistryReaderHelper.SaveRegistryKeyValue("07graphics_api::-%(|::", CBX_GraphicsAPI.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("07SavedResolutionSet::-%(|::", CBX_ScreenResolution.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("07ScreenMode::-%(|::", CBX_WindowMode.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("00gfxsets_vsync::-%(|::", CHK_VSync.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("07MaxFramerate::-%(|::", CBX_FPSLimit.SelectedIndex == 0 ? -1 : CBX_FPSLimit.SelectedValue);
            RegistryReaderHelper.SaveRegistryKeyValue("07gfxsets_textures::-%(|::", CBX_TextureQuality.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("07gfxsets_aa::-%(|::", CBX_AntiAliasing.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("07gfxsets_ao::-%(|::", CBX_AmbientOcclusion.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("00gfxsets_decals::-%(|::", CHK_Decals.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00gfxsets_RenderLight::-%(|::", CHK_RenderLights.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00gfxsets_shadows::-%(|::", CHK_Shadows.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("07gfxsets_shadres::-%(|::", CBX_ShadowResolution.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("07gfxsets_shaddis_new::-%(|::", CBX_ShadowDistance.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("07Volumetrics::-%(|::", CBX_VolumetricLights.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("07gfxsets_mb::-%(|::", NUD_MotionBlur.Value);
            RegistryReaderHelper.SaveRegistryKeyValue("00gfxsets_bloom::-%(|::", CHK_Bloom.Checked);

            // Audio
            RegistryReaderHelper.SaveRegistryKeyValue("06AudioSettings_Master::-%(|::", TBR_MasterVolume.Value);
            RegistryReaderHelper.SaveRegistryKeyValue("06AudioSettings_Effects::-%(|::", TBR_SoundEffectsVolume.Value);
            RegistryReaderHelper.SaveRegistryKeyValue("06AudioSettings_MenuMusic::-%(|::", TBR_VoiceChatVolume.Value);
            RegistryReaderHelper.SaveRegistryKeyValue("06AudioSettings_VoiceChat::-%(|::", TBR_MenuMusicVolume.Value);
            RegistryReaderHelper.SaveRegistryKeyValue("06AudioSettings_Interface::-%(|::", TBR_InterfaceVolume.Value);
            RegistryReaderHelper.SaveRegistryKeyValue("13VcMicName::-%(|::", CBX_InputDevice.SelectedIndex == 0 ? string.Empty : CBX_InputDevice.Text);
            RegistryReaderHelper.SaveRegistryKeyValue("00VcNoiseRed::-%(|::", CHK_EnableNoiseReduction.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("07MenuTheme::-%(|::", CBX_MenuTheme.SelectedIndex);
            RegistryReaderHelper.SaveRegistryKeyValue("00mute_spectators::-%(|::", CHK_MuteSpectators.Checked);

            // Controls

            // Gameplay
            RegistryReaderHelper.SaveRegistryKeyValue("00ClassIntroFastFade::-%(|::", CHK_FastIntroFade.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00HeadBob::-%(|::", CHK_HeadBob.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00ToggleSprint::-%(|::", CHK_ToggleSprint.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00HealthBarShowsExact::-%(|::", CHK_DisplayExactHP.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00RichPresence::-%(|::", CHK_RichPresence.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00PublicLobby::-%(|::", CHK_PublicLobby.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00HideIP::-%(|::", CHK_HideIPAddress.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00ToggleSearch::-%(|::", CHK_ToggleSearch.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00ModeSwitchSetting079::-%(|::", CHK_Scp079ToggleView.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00PostProcessing079::-%(|::", CHK_Scp079PostProcessing.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00Subtitles::-%(|::", CHK_Subtitles.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00DarkMode::-%(|::", CHK_DarkMode.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("07ragdoll_cleanup::-%(|::", TBR_RagdollCleanupTime.Value);

            // Other
            RegistryReaderHelper.SaveRegistryKeyValue("00DisplaySteamProfile::-%(|::", CHK_DisplaySteamProfile.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00DNT::-%(|::", CHK_DoNotTrack.Checked);
            RegistryReaderHelper.SaveRegistryKeyValue("00ToggleTaskbarFlash::-%(|::", CHK_FlashTaskbar.Checked);

            RegistryReaderHelper.WriteAllRegistryKeys();
        }

        private void LoadSettings()
        {
            // Video
            CBX_GraphicsAPI.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07graphics_api::-%(|::");
            CBX_ScreenResolution.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07SavedResolutionSet::-%(|::");
            CBX_WindowMode.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07ScreenMode::-%(|::");
            CHK_VSync.Checked = RegistryReaderHelper.ReadRegistryKeyValue<bool>("00gfxsets_vsync::-%(|::");
            CBX_FPSLimit.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07MaxFramerate::-%(|::") == -1 ? 0 : RegistryReaderHelper.ReadRegistryKeyValue<int>("07MaxFramerate::-%(|::");
            CBX_TextureQuality.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07gfxsets_textures::-%(|::");
            CBX_AntiAliasing.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07gfxsets_aa::-%(|::");
            CBX_AmbientOcclusion.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07gfxsets_ao::-%(|::");
            CHK_Decals.Checked = RegistryReaderHelper.ReadRegistryKeyValue<bool>("00gfxsets_decals::-%(|::");
            CHK_RenderLights.Checked = RegistryReaderHelper.ReadRegistryKeyValue<bool>("00gfxsets_RenderLight::-%(|::");
            CHK_Shadows.Checked = RegistryReaderHelper.ReadRegistryKeyValue<bool>("00gfxsets_shadows::-%(|::");
            CBX_ShadowResolution.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07gfxsets_shadres::-%(|::");
            CBX_ShadowDistance.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07gfxsets_shaddis_new::-%(|::");
            CBX_VolumetricLights.SelectedIndex = RegistryReaderHelper.ReadRegistryKeyValue<int>("07Volumetrics::-%(|::");
            NUD_MotionBlur.Value = RegistryReaderHelper.ReadRegistryKeyValue<int>("07gfxsets_mb::-%(|::");
            CHK_Bloom.Checked = RegistryReaderHelper.ReadRegistryKeyValue<bool>("00gfxsets_bloom::-%(|::");

            // Audio
            TBR_MasterVolume.Value = FormatSliderValue(RegistryReaderHelper.ReadRegistryKeyValue<double>("06AudioSettings_Master::-%(|::"));
            TBR_SoundEffectsVolume.Value = FormatSliderValue(RegistryReaderHelper.ReadRegistryKeyValue<double>("06AudioSettings_Effects::-%(|::"));
            TBR_VoiceChatVolume.Value = FormatSliderValue(RegistryReaderHelper.ReadRegistryKeyValue<double>("06AudioSettings_MenuMusic::-%(|::"));
            TBR_MenuMusicVolume.Value = FormatSliderValue(RegistryReaderHelper.ReadRegistryKeyValue<double>("06AudioSettings_VoiceChat::-%(|::"));
            TBR_InterfaceVolume.Value = FormatSliderValue(RegistryReaderHelper.ReadRegistryKeyValue<double>("06AudioSettings_Interface::-%(|::"));
        }

        private void SaveSettings()
        {

        }

        private void CHK_VSync_CheckedChanged(object sender, EventArgs e)
        {
            CBX_FPSLimit.Enabled = CHK_VSync.Checked;
        }

        private void CHK_RenderLights_CheckedChanged(object sender, EventArgs e)
        {
            CHK_Shadows.Enabled = CHK_RenderLights.Checked;
            CHK_Shadows.Checked = CHK_RenderLights.Checked ? true : false;
            CBX_ShadowResolution.Enabled = CHK_RenderLights.Checked;
            CBX_ShadowDistance.Enabled = CHK_RenderLights.Checked;
            CBX_VolumetricLights.Enabled = CHK_RenderLights.Checked;
        }

        public static int FormatSliderValue(double value)
        {
            return (int)(value * 100);
        }
    }
}