﻿using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbExport.Common.Interfaces
{
	public interface IObjectBase
	{
		string Id { get; set; }
		Status Status { get; set; }
		bool Save();
		bool Save(SqlCeTransaction tr);
	}
}
