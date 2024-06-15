using ExampleApp.TextRecognition;

namespace ExampleApp;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private async void OnTextRecognitionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_serviceProvider.GetRequiredService<TextRecognitionView>());
    }
}
