using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.AppService;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HueCallStatusUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
			this.Loaded += MainPage_Loaded;
            App.AppServiceConnected += App_AppServiceConnected;
        }

        private async void App_AppServiceConnected(object sender, EventArgs e)
        {
            var req = new ValueSet();
            req.Add("REQUEST", "GetMicrophoneStatus");
            // send the ValueSet to the fulltrust process
            AppServiceResponse response = await App.Connection.SendMessageAsync(req);

            // check the result
            response.Message.TryGetValue("RESPONSE", out object result);
            if (result.ToString() == "INUSE")
            {
                textBlockStatus.Text = "Microphone in use!";
            } else if (result.ToString() == "NOTINUSE")
            {
                textBlockStatus.Text = "Microphone not in use.";
            }
            // no longer need the AppService connection
            App.AppServiceDeferral.Complete();
        }
    

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
            if (ApiInformation.IsApiContractPresent(
             "Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
        }



	}
}
