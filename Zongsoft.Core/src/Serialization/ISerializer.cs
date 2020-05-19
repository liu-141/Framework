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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Zongsoft.Serialization
{
	/// <summary>
	/// 提供将对象序列化到流中和从流中反序列化对象的功能。
	/// </summary>
	public interface ISerializer
	{
		/// <summary>
		/// 反序列化指定<paramref name="stream"/>包含的对象。
		/// </summary>
		/// <param name="stream">待反序列化的流。</param>
		/// <param name="settings">反序列化的设置。</param>
		/// <returns>反序列化的结果。</returns>
		object Deserialize(Stream stream, SerializationSettings settings = null);

		/// <summary>
		/// 反序列化指定<paramref name="stream"/>包含的对象。
		/// </summary>
		/// <param name="stream">待反序列化的流。</param>
		/// <param name="type">反序列化的结果类型。</param>
		/// <param name="settings">反序列化的设置。</param>
		/// <returns>反序列化的结果。</returns>
		object Deserialize(Stream stream, Type type, SerializationSettings settings = null);

		/// <summary>
		/// 反序列化指定<paramref name="stream"/>包含的对象。
		/// </summary>
		/// <typeparam name="T">指定的反序列化结果的泛类型。</typeparam>
		/// <param name="stream">待反序列化的流。</param>
		/// <param name="settings">反序列化的设置。</param>
		/// <returns>反序列化的结果。</returns>
		T Deserialize<T>(Stream stream, SerializationSettings settings = null);

		/// <summary>
		/// 反序列化指定<paramref name="stream"/>包含的对象。
		/// </summary>
		/// <param name="stream">待反序列化的流。</param>
		/// <param name="settings">反序列化的设置。</param>
		/// <param name="cancellationToken">异步取消标记。</param>
		/// <returns>反序列化的结果。</returns>
		ValueTask<object> DeserializeAsync(Stream stream, SerializationSettings settings = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// 反序列化指定<paramref name="stream"/>包含的对象。
		/// </summary>
		/// <param name="stream">待反序列化的流。</param>
		/// <param name="type">反序列化的结果类型。</param>
		/// <param name="settings">反序列化的设置。</param>
		/// <param name="cancellationToken">异步取消标记。</param>
		/// <returns>反序列化的结果。</returns>
		ValueTask<object> DeserializeAsync(Stream stream, Type type, SerializationSettings settings = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// 反序列化指定<paramref name="stream"/>包含的对象。
		/// </summary>
		/// <typeparam name="T">指定的反序列化结果的泛类型。</typeparam>
		/// <param name="stream">待反序列化的流。</param>
		/// <param name="settings">反序列化的设置。</param>
		/// <param name="cancellationToken">异步取消标记。</param>
		/// <returns>反序列化的结果。</returns>
		ValueTask<T> DeserializeAsync<T>(Stream stream, SerializationSettings settings = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// 将指定的对象序列化到指定的<seealso cref="System.IO.Stream"/>流中。
		/// </summary>
		/// <param name="stream">要将对象序列化到的流。</param>
		/// <param name="graph">待序列化的目标对象。</param>
		/// <param name="settings">序列化的选项设置。</param>
		void Serialize(Stream stream, object graph, SerializationSettings settings = null);

		/// <summary>
		/// 将指定的对象序列化到指定的<seealso cref="System.IO.Stream"/>流中。
		/// </summary>
		/// <param name="stream">要将对象序列化到的流。</param>
		/// <param name="graph">待序列化的目标对象。</param>
		/// <param name="settings">序列化的选项设置。</param>
		/// <param name="cancellationToken">异步取消标记。</param>
		/// <returns>返回的异步任务。</returns>
		Task SerializeAsync(Stream stream, object graph, SerializationSettings settings = null, CancellationToken cancellationToken = default);
	}
}