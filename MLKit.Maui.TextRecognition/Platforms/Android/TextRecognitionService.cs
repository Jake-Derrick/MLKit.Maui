using Android.Gms.Extensions;
using Android.Graphics;
using Xamarin.Google.MLKit.Vision.Common;
using MLText = Xamarin.Google.MLKit.Vision.Text;

namespace MLKit.Maui.TextRecognition;

/// <summary>
/// Implementation of <see cref="ITextRecognitionService"/>
/// </summary>
public class TextRecognitionService : ITextRecognitionService
{
    private readonly MLText.ITextRecognizer _textRecognizer;

    public TextRecognitionService()
    {
        _textRecognizer = MLText.TextRecognition.GetClient(MLText.Latin.TextRecognizerOptions.DefaultOptions);
    }

    /// <inheritdoc />
    public async Task<TextRecognitionResult> GetTextFromImage(FileResult imageFile)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
            throw new PermissionException("StorageRead Permission Required.");

        using var stream = await imageFile.OpenReadAsync();
        using var bitmap = BitmapFactory.DecodeStream(stream);

        return await GetTextFromBitmap(bitmap);
    }

    /// <inheritdoc />
    public async Task<TextRecognitionResult> GetTextFromImage(byte[] imageBytes)
    {
        using var bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
        return await GetTextFromBitmap(bitmap);
    }

    private async Task<TextRecognitionResult> GetTextFromBitmap(Bitmap? bitmap)
    {
        if (bitmap is null)
            return new TextRecognitionResult() { Success = false };

        using var image = InputImage.FromBitmap(bitmap, 0);
        var text = (MLText.Text)await _textRecognizer.Process(image);

        if (text is null)
            return new TextRecognitionResult() { Success = false };

        return ConvertToTextRecognitionResult(text);

    }

    private static TextRecognitionResult ConvertToTextRecognitionResult(MLText.Text text)
    {
        var result = new TextRecognitionResult()
        {
            AllText = text.GetText(),
            Success = true
        };

        foreach (var block in text.TextBlocks)
        {
            var textBlock = CreateTextRecognitionBlock(block);
            result.Blocks.Add(textBlock);
        }

        return result;
    }

    private static TextRecognitionBlock CreateTextRecognitionBlock(MLText.Text.TextBlock block)
    {
        var textBlock = new TextRecognitionBlock(block.Text, ToMauiPoints(block.GetCornerPoints()), []);

        foreach (var line in block.Lines)
        {
            var textLine = CreateTextRecognitionLine(line);
            textBlock.Lines.Add(textLine);
        }

        return textBlock;
    }

    private static TextRecognitionLine CreateTextRecognitionLine(MLText.Text.Line line)
    {
        var textLine = new TextRecognitionLine(line.Text, line.Confidence, ToMauiPoints(line.GetCornerPoints()), line.Angle, []);

        foreach (var element in line.Elements)
        {
            var textElement = CreateTextRecognitionElement(element);
            textLine.Elements.Add(textElement);
        }

        return textLine;
    }

    private static TextRecognitionElement CreateTextRecognitionElement(MLText.Text.Element element)
    {
        return new TextRecognitionElement(element.Text, element.Confidence, ToMauiPoints(element.GetCornerPoints()), element.Angle);
    }

    private static List<Microsoft.Maui.Graphics.Point> ToMauiPoints(Android.Graphics.Point[] androidPoints)
    {
        var mauiPoints = new List<Microsoft.Maui.Graphics.Point>();
        foreach (var androidPoint in androidPoints)
            mauiPoints.Add(new(androidPoint.X, androidPoint.Y));

        return mauiPoints;
    }

}
