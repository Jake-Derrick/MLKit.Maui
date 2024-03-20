namespace MLKit.Maui;

public static class MLPermissionsExtensions
{
    public static async Task<PermissionStatus> RequestBarcodeServicePermissions()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.StorageRead>();

        return status;
    }
}
