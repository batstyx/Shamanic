using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Engine;

namespace Shamanic
{
    public class Strings
    {
        public static string Get(string key) => LocalizeDictionary.Instance.GetLocalizedObject("Shamanic", "Resources", key, LocalizeDictionary.Instance.Culture)?.ToString();
    }
}
