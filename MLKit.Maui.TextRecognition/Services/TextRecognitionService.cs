namespace MLKit.Maui.TextRecognition;

#if !ANDROID && !IOS
/// <summary>
/// Plain net TextRecognitionService for unit tests
/// </summary>
public class TextRecognitionService : ITextRecognitionService
{
    Task<TextRecognitionResult> ITextRecognitionService.GetTextFromImage(FileResult imageFile)
    {
        throw new NotImplementedException();
    }

    Task<TextRecognitionResult> ITextRecognitionService.GetTextFromImage(byte[] imageBytes)
    {
        throw new NotImplementedException();
    }
}
#endif