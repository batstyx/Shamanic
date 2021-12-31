using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamanic.Properties
{
    public sealed partial class Settings
    {
        private int GetDefaultIntValue(string key) => int.Parse(Properties[key].DefaultValue.ToString());
        public int DefaultPlayerLeft => GetDefaultIntValue("PlayerLeft");
        public int DefaultPlayerTop => GetDefaultIntValue("PlayerTop");
        public int DefaultOpponentLeft => GetDefaultIntValue("OpponentLeft");
        public int DefaultOpponentTop => GetDefaultIntValue("OpponentTop");
        public void ResetPlayerPosition()
        {
            PlayerLeft = DefaultPlayerLeft;
            PlayerTop = DefaultPlayerTop;
        }
        public void ResetOpponentPosition()
        {
            OpponentLeft = DefaultOpponentLeft;
            OpponentTop = DefaultOpponentTop;
        }
    }
}
