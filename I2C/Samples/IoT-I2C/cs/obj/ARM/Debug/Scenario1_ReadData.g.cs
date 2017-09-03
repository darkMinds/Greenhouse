﻿#pragma checksum "C:\Users\edidavid\Desktop\I2C\Samples\IoT-I2C\shared\Scenario1_ReadData.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C99F238EE70781FE303D3E3D9E2AA37D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDKTemplate
{
    partial class Scenario1_ReadData : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        private class Scenario1_ReadData_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IScenario1_ReadData_Bindings
        {
            private global::SDKTemplate.Scenario1_ReadData dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.Button obj2;

            public Scenario1_ReadData_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // Scenario1_ReadData.xaml line 33
                        this.obj2 = (global::Windows.UI.Xaml.Controls.Button)target;
                        ((global::Windows.UI.Xaml.Controls.Button)target).Click += (global::System.Object sender, global::Windows.UI.Xaml.RoutedEventArgs e) =>
                        {
                            this.dataRoot.StartStopScenario();
                        };
                        break;
                    default:
                        break;
                }
            }

            // IScenario1_ReadData_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::SDKTemplate.Scenario1_ReadData)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::SDKTemplate.Scenario1_ReadData obj, int phase)
            {
                if (obj != null)
                {
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Scenario1_ReadData.xaml line 33
                {
                    this.StartStopButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 3: // Scenario1_ReadData.xaml line 34
                {
                    this.ScenarioControls = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Scenario1_ReadData.xaml line 39
                {
                    this.textSPI = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // Scenario1_ReadData.xaml line 35
                {
                    this.CurrentTemp = (global::Windows.UI.Xaml.Documents.Run)(target);
                }
                break;
            case 6: // Scenario1_ReadData.xaml line 37
                {
                    this.CurrentHumidity = (global::Windows.UI.Xaml.Documents.Run)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // Scenario1_ReadData.xaml line 13
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    Scenario1_ReadData_obj1_Bindings bindings = new Scenario1_ReadData_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

