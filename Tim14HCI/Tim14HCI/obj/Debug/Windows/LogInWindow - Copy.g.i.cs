﻿#pragma checksum "..\..\..\Windows\LogInWindow - Copy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "18476978E884B5EE9CA779C94978B2EC8D604B0B02EC14F09FE4D7B3595894B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Tim14HCI.Windows;


namespace Tim14HCI.Windows {
    
    
    /// <summary>
    /// LogInWindow
    /// </summary>
    public partial class LogInWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 50 "..\..\..\Windows\LogInWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_message;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Windows\LogInWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbx_username;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Windows\LogInWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox psbx_password;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Tim14HCI;component/windows/loginwindow%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\LogInWindow - Copy.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lbl_message = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.txbx_username = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\..\Windows\LogInWindow - Copy.xaml"
            this.txbx_username.KeyDown += new System.Windows.Input.KeyEventHandler(this.txbx_username_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.psbx_password = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 55 "..\..\..\Windows\LogInWindow - Copy.xaml"
            this.psbx_password.KeyDown += new System.Windows.Input.KeyEventHandler(this.txbx_username_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 56 "..\..\..\Windows\LogInWindow - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 72 "..\..\..\Windows\LogInWindow - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
