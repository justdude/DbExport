﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using DbExport.Common;
using DbExport.Common.Interfaces;
using DbExport.CSV;
using DbExport.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace DBExport.Main.ViewModel
{
	public class TablesHandleViewModel : BindableBase
	{
		private readonly DelegateCommand mvAddCommand;
		private readonly DelegateCommand mvDeleteCommand;
		private readonly DelegateCommand mvSaveCommand;
		private readonly DelegateCommand mvRefreshCommand;
		private readonly DelegateCommand mvCloseCommand;
		private bool mvIsBlocked;
		private bool mvIsHasError;
		private TableViewModel mvSelectedTable;

		public TablesHandleViewModel()
		{
			Tables = new ObservableCollection<TableViewModel>();

			mvAddCommand = new DelegateCommand(OnAdd, CanAdd);
			mvDeleteCommand = new DelegateCommand(OnDeleteSelected, CanDelete);
			mvSaveCommand = new DelegateCommand(OnSaveSelected, CanSave);
			mvRefreshCommand = new DelegateCommand(OnRefresh, CanRefresh);
			mvCloseCommand = new DelegateCommand(OnClose);
			//eventAggregator.GetEvent<StateChangedEvent>().Subscribe(OnDataChanged);
			//eventAggregator.GetEvent<ItemChangedEvent>().Subscribe(OnSelectedChanged, ThreadOption.PublisherThread, true, Filter);
			LoadData();
		}

		public ObservableCollection<TableViewModel> Tables { get; set; }


		public TableViewModel SelectedTable
		{
			get
			{
				return mvSelectedTable;
			}
			set
			{
				if (mvSelectedTable == value)
					return;

				mvSelectedTable = value;

				this.OnPropertyChanged(() => this.SelectedTable);
				this.OnPropertyChanged(() => this.SelectedTableView);
			}
		}

		public DataView SelectedTableView
		{
			get
			{
				if (!IsSelected || !SelectedTable.IsExist)
					return null;

				return GetItems(SelectedTable.Current.Data, 0, 20).DefaultView;
			}
		}

		public DataTable GetItems(DataTable table, int offset, int count)
		{
			DataTable tableRes = new DataTable();

			tableRes.TableName = table.TableName;
			//tableRes.Columns.AddRange(table.Columns.Cast<DataColumn>().ToArray());

			foreach (DataColumn coll in table.Columns)
			{
				string name = coll.ColumnName;
				Type type = coll.DataType;
				DataColumn newColl = new DataColumn(name, type);
				tableRes.Columns.Add(newColl);
			}

			int start = offset;
			int length = Math.Min(table.Rows.Count - count, start + count);

			for (int i = start; i < length; i++)
			{
				object[] data = table.Rows[i].ItemArray;
				tableRes.Rows.Add(data);
			}
			return tableRes;
		}

		public string SelectedItemName
		{
			get
			{
				if (IsSelected == false)
					return string.Empty;

				return SelectedTable.Name;
			}
			set
			{
				if (!IsSelected || SelectedTable.Name == value)
					return;

				SelectedTable.Name = value;

				RaiseRefresh();

				this.OnPropertyChanged(() => this.SelectedItemName);
			}
		}

		public bool IsEnabled
		{
			get
			{
				return mvIsBlocked;
			}
			set
			{
				if (mvIsBlocked == value)
					return;

				mvIsBlocked = value;

				this.OnPropertyChanged(() => this.IsEnabled);
			}
		}

		//public bool IsChanged
		//{
		//	get
		//	{
		//		return SelectedTable != null && SelectedTable.IsChanged;
		//	}
		//}

		public bool IsHasError
		{
			get
			{
				return mvIsHasError;
			}
			set
			{
				if (mvIsHasError == value)
					return;

				mvIsHasError = value;

				RefreshCommands();

				this.OnPropertyChanged(() => this.IsHasError);
			}
		}

		public bool IsSelected
		{
			get
			{
				return SelectedTable != null && SelectedTable.Current != null;
			}
		}

		private void RaisePropertyesChanged()
		{
			if (IsSelected)
				SelectedTable.RaisePropertyesChanged();

			this.OnPropertyChanged(() => this.IsHasError);
			this.OnPropertyChanged(() => this.IsSelected);
			this.OnPropertyChanged(() => this.IsEnabled);
		}

		#region Члены IDataButtons

		public DelegateCommand AddCommand
		{
			get
			{
				return mvAddCommand;
			}
		}

		public DelegateCommand DeleteCommand
		{
			get
			{
				return mvDeleteCommand;
			}
		}

		public DelegateCommand SaveCommand
		{
			get
			{
				return mvSaveCommand;
			}
		}

		public DelegateCommand RefreshCommand
		{
			get
			{
				return mvRefreshCommand;
			}
		}

		public DelegateCommand CloseCommand
		{
			get
			{
				return mvCloseCommand;
			}
		}

		#endregion


		#region Methods

		private void LoadData()
		{
			IsEnabled = false;

			if (Tables == null)
			{
				Tables = new ObservableCollection<TableViewModel>();
			}

			Tables.Clear();

			var countries = Engine.Instance.LoadTables().Select(p => new TableViewModel(p));

			foreach (var item in countries)
			{
				Tables.Add(item);
			}

			IsEnabled = true;
		}

		private void LoadTable(TableViewModel target)
		{

			var country = Tables.FirstOrDefault(p => p.Id == target.Id);
			if (country != null)
			{
				SelectedTable = country;
				//eventAggregator.GetEvent<ComboChangeEvent>().Publish(SelectedCountry);
			}
		}

		private void OnDataChanged(object obj)
		{
			//EmployesItemsViewModel vm = obj as EmployesItemsViewModel;
			//if (vm != null && vm == this)
			//{
			//	this.OnPropertyChanged(() => IsHasError);
			//	RefreshCommands();
			//}
		}

		private void OnSelectedChanged(object obj)
		{
			RefreshCommands();
		}

		//private bool Filter(object obj)
		//{
		//	EmployeeViewModel empl = obj as EmployeeViewModel;
		//	return empl != null && Employers.Contains(empl);
		//}

		private void RefreshCommands()
		{
			RefreshCommand.RaiseCanExecuteChanged();
			SaveCommand.RaiseCanExecuteChanged();
			DeleteCommand.RaiseCanExecuteChanged();
			AddCommand.RaiseCanExecuteChanged();
			CloseCommand.RaiseCanExecuteChanged();
		}

		private bool CanRefresh()
		{
			return IsEnabled;
		}

		private bool CanSave()
		{
			return IsEnabled && !IsHasError && IsSelected;
		}

		private bool CanDelete()
		{
			return IsEnabled && IsSelected;
		}

		private bool CanAdd()
		{
			return IsEnabled;
		}

		private void OnClose()
		{
			//var mess = CEventSystem.Current.GetEvent<Events.CloseWindowEvent>();
		}

		public void OnStateChanged()
		{
			RaiseRefresh();
		}

		private void OnRefresh()
		{
			SelectedTable = null;
			RaiseRefresh();

			LoadData();

			RaiseRefresh();
		}

		private void RaiseRefresh()
		{
			RaisePropertyesChanged();
			RefreshCommands();
		}

		private void OnSaveSelected()
		{
			if (SelectedTable.Current.Status == Status.Normal)
			{
				SelectedTable.Current.Status = Status.Updated;
			}

			//SelectedTable.Current.CountryID = SelectedCountry.Current.Id;
			SelectedTable.Current.Save();
			SelectedTable = null;

			RaiseRefresh();
		}

		private void OnDeleteSelected()
		{
			SelectedTable.Current.Status = Status.Deleted;
			SelectedTable.Current.Save();

			this.Tables.Remove(SelectedTable);
			SelectedTable = null;

			RaiseRefresh();
		}

		private void OnAdd()
		{
			CParserGenericAdapter proccesor = new CParserGenericAdapter();
			string path = CFileHelper.GetPathFromDialog();
			if (string.IsNullOrWhiteSpace(path))
				return;

			DataTable data = proccesor.GetDataTabletFromCSVFile(path);
			CTable table = CTable.Create(data);
			SelectedTable = new TableViewModel(table);

			SelectedTable.Current.Status = Status.Added;

			this.Tables.Add(SelectedTable);
			RaiseRefresh();
			this.OnPropertyChanged(() => this.SelectedTableView);
		}

		private void ExportDataFrom()
		{

		}

		#endregion


	}
}