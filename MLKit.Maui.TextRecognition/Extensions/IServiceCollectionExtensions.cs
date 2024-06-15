namespace MLKit.Maui.TextRecognition;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds the TextRecognitionService to the service collection as a singleton.
    /// </summary>
    public static IServiceCollection AddTextRecognitionService(this IServiceCollection services)
    {
        services.AddSingleton<ITextRecognitionService, TextRecognitionService>();
        return services;
    }
}
