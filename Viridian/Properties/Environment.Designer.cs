﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Viridian.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.4.0.0")]
    internal sealed partial class Environment : global::System.Configuration.ApplicationSettingsBase {
        
        private static Environment defaultInstance = ((Environment)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Environment())));
        
        public static Environment Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".")]
        public string Server {
            get {
                return ((string)(this["Server"]));
            }
            set {
                this["Server"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\Root\\Virtualization\\V2")]
        public string Virtualization {
            get {
                return ((string)(this["Virtualization"]));
            }
            set {
                this["Virtualization"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Microsoft:Hyper-V:SubType:2")]
        public string VirtualSystemSubtype {
            get {
                return ((string)(this["VirtualSystemSubtype"]));
            }
            set {
                this["VirtualSystemSubtype"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\ProgramData\\Microsoft\\Windows\\Hyper-V\\")]
        public string ConfigurationDataRoot {
            get {
                return ((string)(this["ConfigurationDataRoot"]));
            }
            set {
                this["ConfigurationDataRoot"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\Root\\Microsoft\\Windows\\Storage")]
        public string Storage {
            get {
                return ((string)(this["Storage"]));
            }
            set {
                this["Storage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\Root\\Microsoft")]
        public string Microsoft {
            get {
                return ((string)(this["Microsoft"]));
            }
            set {
                this["Microsoft"] = value;
            }
        }
    }
}
