﻿#pragma checksum "..\..\EditCarWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D9D8E269FB499696492DE4B437377BA6DB9D18F0"
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


namespace KlasykaGatunku {
    
    
    /// <summary>
    /// EditCarWindow
    /// </summary>
    public partial class EditCarWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backButton;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox brandTextBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox modelTextBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox typeComboBox;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox fuelComboBox;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox colorComboBox;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox productionYearTextBox;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mileageTextBox;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox availabilityCheckBox;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox priceCategoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\EditCarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox registerplateTextBox;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\EditCarWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/KlasykaGatunku;component/editcarwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditCarWindow.xaml"
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
            
            #line 48 "..\..\EditCarWindow.xaml"
            this.backButton.Click += new System.Windows.RoutedEventHandler(this.backButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.brandTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\EditCarWindow.xaml"
            this.brandTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.brandTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.modelTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\EditCarWindow.xaml"
            this.modelTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.modelTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.typeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.fuelComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.colorComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.productionYearTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 112 "..\..\EditCarWindow.xaml"
            this.productionYearTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.productionYearTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.mileageTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 113 "..\..\EditCarWindow.xaml"
            this.mileageTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.mileageTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 9:
            this.availabilityCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.priceCategoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.registerplateTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 122 "..\..\EditCarWindow.xaml"
            this.registerplateTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.registerPlateTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 12:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 132 "..\..\EditCarWindow.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

