using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace ScreenWatchApp
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Убираем статус-бар
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            // Убираем панель навигации (если нужно)
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                SystemUiFlags.ImmersiveSticky |
                SystemUiFlags.HideNavigation |
                SystemUiFlags.Fullscreen);
        }

        //public void RestartApp()
        //{
        //    var context = Platform.CurrentActivity;
        //    var intent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
        //    intent.AddFlags(Android.Content.ActivityFlags.ClearTop);
        //    context.StartActivity(intent);
        //    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        //}
    }
}
