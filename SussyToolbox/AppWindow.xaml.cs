using System.Windows;

using SussyToolbox.Pages;

namespace SussyToolbox;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class AppWindow : Window
{
	public AppWindow()
	{
		InitializeComponent();
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		frame_content.Navigate(new MainPage());
	}
}
