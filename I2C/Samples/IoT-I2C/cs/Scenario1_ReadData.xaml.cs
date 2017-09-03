//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.Devices.I2c;
using Windows.Devices.Enumeration;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Spi;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SDKTemplate
{
    public sealed partial class Scenario1_ReadData : Page
    {
        private SpiDevice _mcp3008;
        private I2cDevice sensor;
        private DispatcherTimer timer;
        private bool flag = true;

        public Scenario1_ReadData()
        {
            this.InitializeComponent();
            //_deviceClient = DeviceClient.Create(IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(DeviceId, DeviceKey), TransportType.Mqtt);
            //
            //SendDeviceToCloudMessagesAsync();
            //
            ////ReceiveC2dAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StopScenario();
        }

        private async Task StartScenarioAsync()
        {
            string i2cDeviceSelector = I2cDevice.GetDeviceSelector();
            IReadOnlyList<DeviceInformation> devices = await DeviceInformation.FindAllAsync(i2cDeviceSelector);

            var sensor_settings = new I2cConnectionSettings(0x48);

            // This will result in a NullReferenceException in Timer_Tick below.
            sensor = await I2cDevice.FromIdAsync(devices[0].Id, sensor_settings);


            //using SPI0 on the Pi
            var spiSettings = new SpiConnectionSettings(0);//for spi bus index 0
            spiSettings.ClockFrequency = 3600000; //3.6 MHz
            spiSettings.Mode = SpiMode.Mode0;
            
            string spiQuery = SpiDevice.GetDeviceSelector("SPI0");
            //using Windows.Devices.Enumeration;
            var deviceInfo = await DeviceInformation.FindAllAsync(spiQuery);
            if (deviceInfo != null && deviceInfo.Count > 0 && flag)
            {
            
                _mcp3008 = await SpiDevice.FromIdAsync(deviceInfo[0].Id, spiSettings);
                textSPI.Text = "SPI is connected!!! :-)";
                flag = false;
            }
            else if (flag)
            {
                textSPI.Text = "SPI Device Not Found :-(";
            }

             // Start the polling timer.
             timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(500) };
             timer.Tick += Timer_Tick;
             timer.Tick += humidityRead;
             timer.Start();
             
        }

        void StopScenario()
        {
            if (timer != null)
            {
                timer.Tick -= Timer_Tick;
                timer.Tick -= humidityRead;
                timer.Stop();
                timer = null;
            }

            // Release the I2C sensor.
            if (sensor != null)
            {
                sensor.Dispose();
                sensor = null;
            }
            
        }

        async void StartStopScenario()
        {
            Debug.WriteLine("Device Activated\n");
            await AzureIoTHub.SendDeviceToCloudMessageAsync();
            Debug.WriteLine("after msg\n");

            if (timer != null)
            {
                StopScenario();
                StartStopButton.Content = "Start";
                ScenarioControls.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                StartStopButton.IsEnabled = false;
                await StartScenarioAsync();
                StartStopButton.IsEnabled = true;
                StartStopButton.Content = "Stop";
                ScenarioControls.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

        }

        private void Timer_Tick(object sender, object e)
        {
            // Read data from I2C.
            var command = new byte[1];
            var temperatureData = new byte[2];


            // If this next line crashes with a NullReferenceException, then
            // there was a sharing violation on the device. (See StartScenarioAsync above.)
            //
            // If this next line crashes for some other reason, then there was
            // an error accessing the device.

            // Read temperature.
            command[0] = 0x48;
            // If this next line crashes, then there was an error accessing the sensor.
            sensor.WriteRead(command, temperatureData);

            // Calculate and report the temperature.
            var rawTempReading = temperatureData[0] << 8 | temperatureData[1];
            var rawShifted = rawTempReading >> 4;
            float temperature = rawShifted * 0.0625f;
            CurrentTemp.Text = temperature.ToString();
        }

        private void humidityRead(object sender, object e)
        {
            //From data sheet -- 1 byte selector for channel 0 on the ADC
            //First Byte sends the Start bit for SPI
            //Second Byte is the Configuration Bit
            //1 - single ended 
            //0 - d2
            //0 - d1
            //0 - d0
            //             S321XXXX <-- single-ended channel selection configure bits
            var transmitBuffer = new byte[3] { 1, 0x80, 0x00 };
            var receiveBuffer = new byte[3];

            _mcp3008.TransferFullDuplex(transmitBuffer, receiveBuffer);

            var result = ((receiveBuffer[1] & 3) << 8) + receiveBuffer[2];
            var output = result;
            CurrentHumidity.Text = output.ToString();

        }
    }
}
