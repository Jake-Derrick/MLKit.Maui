﻿using Android.Gms.Extensions;
using Android.Graphics;
using Android.Runtime;
using Java.Util;
using Xamarin.Google.MLKit.Vision.Barcode.Common;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Common;

namespace MLKit.Maui;

/// <inheritdoc />
public class BarcodeService : IBarcodeService
{
    private readonly int[] _barcodeFormats;

    public BarcodeService(HashSet<BarcodeFormat> barcodeFormats)
    {
        _barcodeFormats = barcodeFormats.ToAndroidFormat();
    }

    /// <inheritdoc />
    public async Task<List<BarcodeResult>> GetBarcodesFromImage(FileResult imageFile)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
            throw new PermissionException("StorageRead Permission Required. Please call RequestBarcodeServicePermissions before using this.");

        using var stream = await imageFile.OpenReadAsync();
        using var bitmap = BitmapFactory.DecodeStream(stream);
        if (bitmap is null)
            return [];

        using var image = InputImage.FromBitmap(bitmap, 0);

        var scanner = BarcodeScanning.GetClient(new BarcodeScannerOptions.Builder().SetBarcodeFormats(_barcodeFormats[0], _barcodeFormats.Skip(1).ToArray()).Build());

        var barcodes = await scanner.Process(image);
        if (barcodes is null)
            return [];

        var javaList = barcodes.JavaCast<ArrayList>();
        if (javaList is null)
            return [];

        return ConvertToBarcodeResults(javaList);
    }

    private List<BarcodeResult> ConvertToBarcodeResults(ArrayList barcodes)
    {
        var barcodeResults = new List<BarcodeResult>();

        foreach (var barcodeObj in barcodes.ToArray())
        {
            var barcode = barcodeObj.JavaCast<Barcode>();

            var bounds = barcode.BoundingBox;
            var mauiBounds = new Microsoft.Maui.Graphics.Rect(bounds.Left, bounds.Top, bounds.Height(), bounds.Width());

            List<Microsoft.Maui.Graphics.Point> cornerPoints = [];
            foreach (var cornerPoint in barcode.GetCornerPoints())
                cornerPoints.Add(new Microsoft.Maui.Graphics.Point(cornerPoint.X, cornerPoint.Y));

            barcodeResults.Add(new(barcode.DisplayValue, barcode.RawValue, barcode.GetRawBytes(), mauiBounds, cornerPoints, BarcodeExtensions.ToMauiFormat(barcode.Format)));
        }

        return barcodeResults;
    }
}
