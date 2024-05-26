using MLKit.Maui.Barcode;
using MLKit.Maui.TextRecognition;

namespace ExampleApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var status = await MLPermissionsExtensions.RequestBarcodeServicePermissions();
            if (status != PermissionStatus.Granted)
                throw new PermissionException("ahh poop");

            var file = await MediaPicker.PickPhotoAsync();

#if ANDROID
            var textRecognitionService = new TextRecognitionService();
            var result = await textRecognitionService.GetTextFromImage(file);
            var r = result.Success;
            //var barcodeService = new BarcodeService([BarcodeFormat.All]);
            //var barcodes = await barcodeService.GetBarcodesFromImage(file);

            //if (barcodes != null)
            //{
            //    foreach (var barcode in barcodes)
            //    {
            //        var r = barcode;
            //    }
            //}
#endif
        }
    }

}
