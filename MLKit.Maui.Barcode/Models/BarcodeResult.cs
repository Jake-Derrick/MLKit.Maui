namespace MLKit.Maui;

public record BarcodeResult(string DisplayValue, string RawValue, byte[] RawBytes, Rect BoundingBox, List<Point> CornerPoints, BarcodeFormat Format);