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
        public string Key { get; }
        public string Description { get; }

        public LocalizedAttribute(string key)
        {
            Key = key;
            Description = Strings.Get(Key);
        }
    }
}
