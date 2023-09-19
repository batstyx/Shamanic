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
        private double GetDefaultDblValue(string key) => double.Parse(Properties[key].DefaultValue.ToString());
        public int DefaultPlayerLeft => GetDefaultIntValue("PlayerLeft");
        public int DefaultPlayerTop => GetDefaultIntValue("PlayerTop");
        public int DefaultOpponentLeft => GetDefaultIntValue("OpponentLeft");
        public int DefaultOpponentTop => GetDefaultIntValue("OpponentTop");
        public double DefaultPlayerOpacity => GetDefaultDblValue("PlayerOpacity");
        public double DefaultPlayerScale => GetDefaultDblValue("PlayerScale");
        public double DefaultOpponentOpacity => GetDefaultDblValue("OpponentOpacity");
        public double DefaultOpponentScale => GetDefaultDblValue("OpponentScale");

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
        public void ResetPlayerDisplay()
        {
            PlayerOpacity = DefaultPlayerOpacity;
            PlayerScale = DefaultPlayerScale;
        }
        public void ResetOpponentDisplay()
        {
            OpponentOpacity = DefaultOpponentOpacity;
            OpponentScale = DefaultOpponentScale;
        }

    }
}
