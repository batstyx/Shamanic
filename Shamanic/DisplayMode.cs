using System;
using System.ComponentModel;

namespace Shamanic
{
    [Serializable]
    public enum DisplayMode
    {
        [Localized("DisplayModeAlways")]
        Always,
        [Localized("DisplayModeClass")]
        Class,
        [Localized("DisplayModeCard")]
        Card,
        [Localized("DisplayModeNever")]
        Never
    }
}
