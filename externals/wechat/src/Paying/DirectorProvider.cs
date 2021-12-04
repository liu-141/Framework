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
 * This file is part of Zongsoft.Externals.WeChat library.
 *
 * The Zongsoft.Externals.WeChat is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Externals.WeChat is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Externals.WeChat library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

using Zongsoft.Services;

namespace Zongsoft.Externals.Wechat.Paying
{
	public class DirectorProvider : IServiceProvider<Director>
	{
		#region 单例字段
		public static readonly DirectorProvider Instance = new DirectorProvider();
		#endregion

		#region 私有变量
		private readonly IDictionary<string, Director> _directors = new Dictionary<string, Director>(StringComparer.OrdinalIgnoreCase);
		#endregion

		#region 私有构造
		private DirectorProvider() { }
		#endregion

		#region 公共方法
		public Director GetDirector(string name)
		{
			if(string.IsNullOrEmpty(name))
				return null;

			if(_directors.TryGetValue(name, out var director))
				return director;

			lock(_directors)
			{
				if(_directors.TryGetValue(name, out director))
					return director;

				return _directors.TryAdd(name, director = CreateDirector(name)) ? director : _directors[name];
			}

			static Director CreateDirector(string name)
			{
				var authority = AuthorityProvider.GetAuthority(name);
				return authority == null ? null : new Director(authority);
			}
		}
		#endregion

		#region 显式实现
		Director IServiceProvider<Director>.GetService(string name) => this.GetDirector(name);
		#endregion
	}
}
