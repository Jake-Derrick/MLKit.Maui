using MLKit.Maui.TextRecognition;

namespace ExampleApp.TextRecognition;

public partial class TextRecognitionView : ContentPage
{
    private readonly ITextRecognitionService _textRecognitionService;

    public TextRecognitionView(ITextRecognitionService textRecognitionService)
    {
        // TODO: Add permissions
        InitializeComponent();
        _textRecognitionService = textRecognitionService;
    }

    public string TextResults;

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var file = await MediaPicker.PickPhotoAsync();
        if (file is null)
            return;

        var result = await _textRecognitionService.GetTextFromImage(file);
        ResultLabel.Text = result.Success ? result.AllText : "Failed";
    }
}