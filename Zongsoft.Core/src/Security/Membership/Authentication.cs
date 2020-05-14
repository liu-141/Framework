﻿/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@gmail.com>
 *
 * Copyright (C) 2010-2020 Zongsoft Studio <http://www.zongsoft.com>
 *
 * This file is part of Zongsoft.Core library.
 *
 * The Zongsoft.Core is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Core is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Core library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Zongsoft.Common;
using Zongsoft.Services;

namespace Zongsoft.Security.Membership
{
	/// <summary>
	/// 提供身份验证的平台类。
	/// </summary>
	[System.Reflection.DefaultMember(nameof(Authenticators))]
	public class Authentication
	{
		#region 单例字段
		public static readonly Authentication Instance = new Authentication();
		#endregion

		#region 构造函数
		private Authentication()
		{
			var authenticators = new ObservableCollection<IAuthenticator>();
			authenticators.CollectionChanged += OnCollectionChanged;

			this.Authenticators = authenticators;
			this.Filters = new List<IExecutionFilter>();
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取身份验证器的集合。
		/// </summary>
		public ICollection<IAuthenticator> Authenticators
		{
			get;
		}

		/// <summary>
		/// 获取一个身份验证的过滤器集合，该过滤器包含对身份验证的响应处理。
		/// </summary>
		public ICollection<IExecutionFilter> Filters
		{
			get;
		}

		/// <summary>
		/// 获取或设置命名空间提供程序。
		/// </summary>
		public INamespaceProvider Namespaces
		{
			get; set;
		}
		#endregion

		#region 事件响应
		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch(args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					for(int i=args.NewStartingIndex; i< args.NewItems.Count; i++)
					{
						((IAuthenticator)args.NewItems[i]).Authenticated += OnAuthenticated;
						((IAuthenticator)args.NewItems[i]).Authenticating += OnAuthenticating;
					}

					break;
				case NotifyCollectionChangedAction.Reset:
				case NotifyCollectionChangedAction.Remove:
					for(int i = args.OldStartingIndex; i < args.OldItems.Count; i++)
					{
						((IAuthenticator)args.OldItems[i]).Authenticated -= OnAuthenticated;
						((IAuthenticator)args.OldItems[i]).Authenticating -= OnAuthenticating;
					}

					break;
				case NotifyCollectionChangedAction.Replace:
					for(int i = args.OldStartingIndex; i < args.OldItems.Count; i++)
					{
						((IAuthenticator)args.OldItems[i]).Authenticated -= OnAuthenticated;
						((IAuthenticator)args.OldItems[i]).Authenticating -= OnAuthenticating;
					}

					for(int i = args.NewStartingIndex; i < args.NewItems.Count; i++)
					{
						((IAuthenticator)args.NewItems[i]).Authenticated += OnAuthenticated;
						((IAuthenticator)args.NewItems[i]).Authenticating += OnAuthenticating;
					}

					break;
			}
		}

		private void OnAuthenticating(object sender, AuthenticatingEventArgs args)
		{
			foreach(var filter in this.Filters)
			{
				filter.OnFiltering(args);
			}
		}

		private void OnAuthenticated(object sender, AuthenticatedEventArgs args)
		{
			foreach(var filter in this.Filters)
			{
				filter.OnFiltered(args);
			}
		}
		#endregion
	}
}
