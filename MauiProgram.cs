using Microsoft.Extensions.Logging;

namespace CalculateurAge
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // enleve le trait de soulignement d'Android dans les champs
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("SansTrait", (handler, view) =>
            {
#if ANDROID
    handler.PlatformView.BackgroundTintList =
        Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("SansTrait", (handler, view) =>
            {
#if ANDROID
    handler.PlatformView.BackgroundTintList =
        Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            return builder.Build();
        }
    }
}
