﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shamanic.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.3.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Auto")]
        public global::Hearthstone_Deck_Tracker.Enums.DisplayMode OverloadCounterDisplay {
            get {
                return ((global::Hearthstone_Deck_Tracker.Enums.DisplayMode)(this["OverloadCounterDisplay"]));
            }
            set {
                this["OverloadCounterDisplay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Auto")]
        public global::Hearthstone_Deck_Tracker.Enums.DisplayMode TotemsCounterDisplay {
            get {
                return ((global::Hearthstone_Deck_Tracker.Enums.DisplayMode)(this["TotemsCounterDisplay"]));
            }
            set {
                this["TotemsCounterDisplay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Never")]
        public global::Hearthstone_Deck_Tracker.Enums.DisplayMode OpponentCountersDisplay {
            get {
                return ((global::Hearthstone_Deck_Tracker.Enums.DisplayMode)(this["OpponentCountersDisplay"]));
            }
            set {
                this["OpponentCountersDisplay"] = value;
            }
        }
    }
}
