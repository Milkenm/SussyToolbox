using System;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Win32;

using ScriptsLibV2.Util;

using SussyAPI.SearchIconBlocker;

namespace SussyToolbox.Pages;
/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{
	private SearchIconBlocker _searchIconBlocker = new SearchIconBlocker();

	public MainPage()
	{
		InitializeComponent();
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		comboBox_taskbarState.ItemsSource = Enum.GetValues(typeof(SearchBoxStyle));
		comboBox_taskbarState.SelectedItem = SearchBoxStyle.Hidden;
		comboBox_wallpaperStyle.ItemsSource = Enum.GetValues(typeof(WallpaperStyle));
		comboBox_wallpaperStyle.SelectedItem = WallpaperStyle.Stretched;

		comboBox_taskbarState.SelectionChanged += SelectionChanged;
		checkBox_lockTaskbarSearch.Checked += OnCheckBoxSearchChanged;
	}

	private void OnCheckBoxSearchChanged(object sender, RoutedEventArgs e)
	{
		if (checkBox_lockTaskbarSearch.IsChecked == null) return;

		_searchIconBlocker.Listening = (bool)checkBox_lockTaskbarSearch.IsChecked;
	}

	private void SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		SearchBoxStyle style = (SearchBoxStyle)comboBox_taskbarState.SelectedItem;
		_searchIconBlocker.BoxStyle = style;
	}

	private void ButtonChangeWallpaperClick(object sender, RoutedEventArgs e)
	{
		OpenFileDialog dialog = new OpenFileDialog();
		dialog.Filter = "PNG Image |*.png";
		if (dialog.ShowDialog() == true)
		{
			string imagePath = dialog.FileName;
			Utils.SetDesktopWallpaper(imagePath, (WallpaperStyle)comboBox_wallpaperStyle.SelectedItem);
		}
	}
}
