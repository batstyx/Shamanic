using WPFLocalizeExtension.Engine;

namespace Shamanic.Properties
{
    public class Strings
    {
        public static string Get(string key) => LocalizeDictionary.Instance.GetLocalizedObject("Shamanic", "Resources", key, LocalizeDictionary.Instance.Culture)?.ToString();
    }
}
