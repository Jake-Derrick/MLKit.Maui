
namespace MLKit.Maui.TextRecognition;

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
