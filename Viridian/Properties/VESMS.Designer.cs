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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.3.0.0")]
    internal sealed partial class VESMS : global::System.Configuration.ApplicationSettingsBase {
        
        private static VESMS defaultInstance = ((VESMS)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new VESMS())));
        
        public static VESMS Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("root\\virtualization\\v2")]
        public string WMINamespace {
            get {
                return ((string)(this["WMINamespace"]));
            }
            set {
                this["WMINamespace"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Msvm_VirtualSystemManagementService")]
        public string ClassName {
            get {
                return ((string)(this["ClassName"]));
            }
            set {
                this["ClassName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Class name does not match!")]
        public string ClassNameExceptionMessage {
            get {
                return ((string)(this["ClassNameExceptionMessage"]));
            }
            set {
                this["ClassNameExceptionMessage"] = value;
            }
        }
    }
}
