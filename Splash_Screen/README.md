# Reusable WinForms splash screen

`Splash_Screen` is a .NET Framework 4.7.2 class library with no CymaLab or
third-party dependencies. Add a project reference to use it in another WinForms
application. The consuming app supplies its own artwork; CymaLab embeds its PNG
in the application assembly, so no external image file is needed at runtime.

```csharp
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);

using (var artwork = Image.FromFile("YourSplash.png"))
using (var splash = SplashScreen.Show(artwork, new SplashOptions
{
    Width = 800,
    StatusColor = Color.FromArgb(32, 32, 32),
    StatusPosition = 0.87f,
    DotsPosition = 0.94f
}))
using (var main = new MainForm())
{
    splash.Report("Loading settings");
    // Perform real preparation here on the main STA thread.
    // Pass splash as IProgress<string> to report additional stages.
    main.Shown += (sender, args) => splash.Complete(main);
    Application.Run(main);
}
```

Call from an `[STAThread]` entry point. The splash runs a separate STA message
loop, keeping its dots animated during synchronous control construction and
initialization. `Report` is thread-safe and paints each message before returning.
Disposal closes the window, joins its thread, and releases the cloned artwork,
font, and timer; repeated disposal is safe. Keep the `using` scope around startup
so exceptions also close the splash. Display startup error dialogs after closing
the splash, as shown in CymaLab's `Program.cs`.

Use `Complete(main)` in the main form's `Shown` event for the normal handoff:
it requests foreground ownership for the main window before closing the splash,
then brings the main window forward and activates it after the show sequence.
The main window does not stay permanently topmost. Windows controls whether a
foreground request is allowed (for example, if the user has switched applications).
`Dispose()` alone remains appropriate for canceled or failed startup.

The window preserves the artwork's aspect ratio and fits the current screen.
Positions range from 0 to 1 relative to its height. Alt+F4 cannot dismiss it;
its lifetime is controlled by the application. There is no progress bar,
percentage estimate, or forced delay. A quick startup can finish quickly.

CymaLab reports control construction, settings loading/application, chart setup,
communication service initialization, and device discovery startup. Discovery
continues normally in the main app; splash completion does not require connected
hardware. The app name and version in the supplied PNG remain part of the image;
replace that artwork when those printed details change.

CymaLab's entry point waits for the splash to paint before entering a separate
method marked `NoInlining`, keeping main-form type resolution and initialization
behind the splash. Process/.NET startup, WinForms setup, and decoding the splash
artwork necessarily happen before the splash can be displayed.
