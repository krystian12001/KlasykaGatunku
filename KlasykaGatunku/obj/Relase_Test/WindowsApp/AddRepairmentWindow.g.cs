﻿#pragma checksum "..\..\..\WindowsApp\AddRepairmentWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AB9A976167CD296A51688CB52CE3FFA8CDD06D78"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using KlasykaGatunku;
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


namespace KlasykaGatunku.WindowsApp {
    
    
    /// <summary>
    /// AddRepairmentWindow
    /// </summary>
    public partial class AddRepairmentWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox carComboBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox issueTextBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox costTextBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dateTextBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox isFixedCheckBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;
        
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
            System.Uri resourceLocater = new System.Uri("/KlasykaGatunku;component/windowsapp/addrepairmentwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
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
            this.backButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
            this.backButton.Click += new System.Windows.RoutedEventHandler(this.backButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.carComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 46 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
            this.carComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.carComboBox_SelectedIndexChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.issueTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 47 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
            this.issueTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.issueTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.costTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
            this.costTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.costTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dateTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 49 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
            this.dateTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.dateTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.isFixedCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\WindowsApp\AddRepairmentWindow.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
