namespace MLKit.Maui.TextRecognition;

public class TextRecognitionResult
{
    public bool Success { get; set; }

    public string AllText { get; set; } = "";

    public List<TextRecognitionBlock> Blocks { get; set; } = [];
}

public class TextRecognitionBlock(string text, List<Point> cornerPoints, List<TextRecognitionLine> lines)
{
    public string Text { get; set; } = text;
    public List<Point> CornerPoints { get; set; } = cornerPoints;
    public List<TextRecognitionLine> Lines { get; set; } = lines;
}

public class TextRecognitionLine(string text, double confidence, List<Point> cornerPoints, double rotationDegree, List<TextRecognitionElement> elements)
{
    public string Text { get; set; } = text;
    public double Confidence { get; set; } = confidence;
    public List<Point> CornerPoints { get; set; } = cornerPoints;
    public double RotationDegree { get; set; } = rotationDegree;
    public List<TextRecognitionElement> Elements { get; set; } = elements;
}

public class TextRecognitionElement(string text, double confidence, List<Point> cornerPoints, double rotationDegree)
{
    public string Text { get; set; } = text;
    public double Confidence { get; set; } = confidence;
    public List<Point> CornerPoints { get; set; } = cornerPoints;
    public double RotationDegree { get; set; } = rotationDegree;
}
