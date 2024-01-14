#if ANDROID
using Xamarin.Google.MLKit.Vision.Barcode.Common;
#endif

namespace MLKit.Maui;

public static class BarcodeExtensions
{

#if ANDROID
    public static int[] ToAndroidFormat(this HashSet<BarcodeFormat> formats)
    {
        if (formats.Count == 0)
            return [Barcode.FormatAllFormats];

        var result = new List<int>();
        foreach (BarcodeFormat format in formats)
            result.Add(format.ToAndroidFormat());

        return result.ToArray();
    }

    public static int ToAndroidFormat(this BarcodeFormat format)
    {
        return format switch
        {
            BarcodeFormat.Code_128 => Barcode.FormatCode128,
            BarcodeFormat.Code_39 => Barcode.FormatCode39,
            BarcodeFormat.Code_93 => Barcode.FormatCode93,
            BarcodeFormat.Codabar => Barcode.FormatCodabar,
            BarcodeFormat.EAN_13 => Barcode.FormatEan13,
            BarcodeFormat.EAN_8 => Barcode.FormatEan8,
            BarcodeFormat.ITF => Barcode.FormatItf,
            BarcodeFormat.UPC_A => Barcode.FormatUpcA,
            BarcodeFormat.UPC_E => Barcode.FormatUpcE,
            BarcodeFormat.QR => Barcode.FormatQrCode,
            BarcodeFormat.PDF_417 => Barcode.FormatPdf417,
            BarcodeFormat.Aztec => Barcode.FormatAztec,
            BarcodeFormat.DataMatrix => Barcode.FormatDataMatrix,
            BarcodeFormat.All => Barcode.FormatAllFormats,
            _ => (int)BarcodeFormat.Unknown
        };
    }

    public static BarcodeFormat ToMauiFormat(int androidFormat)
    {
        return androidFormat switch
        {
            Barcode.FormatCode128 => BarcodeFormat.Code_128,
            Barcode.FormatCode39 => BarcodeFormat.Code_39,
            Barcode.FormatCode93 => BarcodeFormat.Code_93,
            Barcode.FormatCodabar => BarcodeFormat.Codabar,
            Barcode.FormatEan13 => BarcodeFormat.EAN_13,
            Barcode.FormatEan8 => BarcodeFormat.EAN_8,
            Barcode.FormatItf => BarcodeFormat.ITF,
            Barcode.FormatUpcA => BarcodeFormat.UPC_A,
            Barcode.FormatUpcE => BarcodeFormat.UPC_E,
            Barcode.FormatQrCode => BarcodeFormat.QR,
            Barcode.FormatPdf417 => BarcodeFormat.PDF_417,
            Barcode.FormatAztec => BarcodeFormat.Aztec,
            Barcode.FormatDataMatrix => BarcodeFormat.DataMatrix,
            Barcode.FormatAllFormats => BarcodeFormat.All,
            _ => BarcodeFormat.Unknown
        };
    }
#endif

}
