﻿using System.Collections.Generic;
using System.Web.Security;
using PagedList;

namespace Zarabizi.Models.UserAdministration
{
	public class IndexViewModel
	{
		public IPagedList<MembershipUser> Users { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}