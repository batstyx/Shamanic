using Shamanic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamanic
{
    public class LocalizedAttribute : Attribute
    {
        private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();

        private static string Get(string key)
        {
            return Resources.ResourceManager.GetString(key);
        }

        public string Key { get; }
        public string Description { get; }

        public LocalizedAttribute(string key)
        {
            Key = key;
            Description = Get(Key);
        }
    }
}
