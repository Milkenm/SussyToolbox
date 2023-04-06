using ScriptsLibV2.Util;

namespace SussyAPI.WallpaperChanger;
public class WallpaperChanger
{
	public static void ChangeWallpaper(string location, WallpaperStyle style)
	{
		Utils.SetDesktopWallpaper(location, style);
	}
}
