using System.DirectoryServices.AccountManagement;
using System.Management;
using System.Security.Principal;

using Microsoft.Win32;

namespace SussyAPI.SearchIconBlocker;
public class SearchIconBlocker
{
	private ManagementEventWatcher _watcher;

	public SearchIconBlocker()
	{
		SecurityIdentifier userIdentity = WindowsIdentity.GetCurrent().User;
		string sid = UserPrincipal.Current.Sid.ToString();

		// Your query goes below; "KeyPath" is the key in the registry that you
		// want to monitor for changes. Make sure you escape the \ character.
		WqlEventQuery query = new WqlEventQuery($@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{sid}\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Search' AND ValueName='SearchboxTaskbarMode'");

		_watcher = new ManagementEventWatcher(query);
		_watcher.EventArrived += this.OnRegistryChange;
		_watcher.Start();
	}

	private void OnRegistryChange(object sender, EventArrivedEventArgs e)
	{

	}

	private void ChangeStyle(SearchBoxStyle style)
	{
		RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Search", true);
		if (key != null)
		{
			key.SetValue("SearchboxTaskbarMode", ((int)style).ToString(), RegistryValueKind.DWord);
			key.Close();
		}
	}

	~SearchIconBlocker()
	{
		_watcher.Stop();
	}

	private SearchBoxStyle _boxStyle = SearchBoxStyle.Hidden;
	public SearchBoxStyle BoxStyle
	{
		get
		{
			return _boxStyle;
		}
		set
		{
			_boxStyle = value;
			ChangeStyle(value);
		}
	}

	private bool _listening = false;
	public bool Listening
	{
		get
		{
			return _listening;
		}
		set
		{
			if (value == _listening) return;

			_listening = value;
			if (value == true)
			{
				_watcher.Start();
				ChangeStyle(BoxStyle);
			}
			else
			{
				_watcher.Stop();
			}
		}
	}
}

public enum SearchBoxStyle
{
	Hidden,
	Icon,
	SearchBox,
}