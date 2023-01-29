namespace ScpSecretLaboratorySettingsChanger.Objects
{
    public static class Translations
    {
        private static string[] translationCodes =
        {
            "be",
            "ca",
            "cs",
            "da",
            "de",
            "de-AT",
            "English (default)",
            "es",
            "fi",
            "fr",
            "fr-CA",
            "hu",
            "it",
            "ja",
            "kk-KZ",
            "ko",
            "lv",
            "nb",
            "nl",
            "pl",
            "pt",
            "pt-BR",
            "ro",
            "ru",
            "sk",
            "sr-Cyrl-BA",
            "sr-Letn-BA",
            "th",
            "tr",
            "uk",
            "vi",
            "zh",
            "zh-Hant"
        };

        public static string GetTranslationValue(int index)
        {
            return translationCodes[index];
        }

        public static int GetIndexOfTranslation(string value)
        {
            for (int i = 0; i < translationCodes.Length; i++) 
            {
                if (translationCodes[i].Equals(value))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
