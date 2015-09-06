﻿using DbExport.Data;
using DBExport.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBExport.Products;
using DBExport.Filtering.ViewModel;

namespace DBExport.Helpers
{
	public class CWindowHelper
	{
		public static void ShowEmployeWindow(CTable table, Func<object, bool> checkRightColl, Action onWindowClosed)
		{
			var wind = new wndSetTypes();
			//wind.Owner = App.Current.MainWindow;
			//wind.Owner.Hide();
			//wind.Closed += (e, a) =>
			//{
			//	wind.Owner.Show();
			//	wind.Activate();
			//};
			wind.DataContext = new Settings.ViewModel.TableSettingViewModel(table, checkRightColl)
			{
				Token = wind.Token,
			};
			wind.Closed += (p, v) =>
				{
					if (onWindowClosed != null)
						onWindowClosed();
				};

			wind.Show();
		}

		public static void ShowEditSelectedTable(CTable table)
		{
			var wind = new wndProducts();
			wind.Owner = App.Current.MainWindow;
			wind.Owner.Hide();
			wind.Closed += (e, a) =>
			{
				wind.Owner.Show();
				wind.Activate();
			};

			wind.Loaded += (p, e) =>
			{
				wind.Init();
				wind.ctrProd.Init(wind.Token);
				wind.DataContext = new ProductItemsViewModel(table)
				{
					Token = wind.Token,
					Disp = wind.Dispatcher
				};
			};

			wind.Show();
		}

		public static void ShowSelectFilterWindow(string tableId, Action<string, List<CFilter>> onFilterSelected)
		{
			var wind = new wndSelectFilter();
			wind.Owner = App.Current.MainWindow;
			wind.Closed += (e, a) =>
			{
				wind.Owner.Show();
				wind.Activate();
			};

			wind.Loaded += (p, e) =>
			{
				wind.DataContext = new SelectFilterViewModel(tableId, onFilterSelected)
				{
					Token = wind.Token,
					Disp = wind.Dispatcher
				};
			};

			wind.ShowDialog();
		}
	}
}
