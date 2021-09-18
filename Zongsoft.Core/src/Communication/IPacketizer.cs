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

namespace Zongsoft.Communication
{
	/// <summary>
	/// 提供通讯协议包有关打包与拆包功能的接口。
	/// </summary>
	/// <typeparam name="TPackage">通讯协议包的类型。</typeparam>
	public interface IPacketizer<TPackage>
	{
		/// <summary>获取或设置协议名称。</summary>
		string Name { get; }

		/// <summary>
		/// 打包，将通讯包对象序列化成字节流。
		/// </summary>
		/// <param name="package">待打包的通讯包对象。</param>
		/// <param name="data">打包成功的字节数组。</param>
		/// <returns>如果打包成功则返回真(True)，否则返回假(False)。</returns>
		bool TryPack(TPackage package, out byte[] data);

		/// <summary>
		/// 拆包，将字节流反序列化成通讯包对象。
		/// </summary>
		/// <param name="data">待拆包的字节流。</param>
		/// <param name="result">拆包成功的通讯包对象。</param>
		/// <returns>如果拆包成功则返回真(True)，否则返回假(False)。</returns>
		bool TryUnpack(ReadOnlySpan<byte> data, out TPackage result);
	}
}
