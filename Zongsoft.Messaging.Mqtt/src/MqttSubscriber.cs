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
 * Copyright (C) 2010-2021 Zongsoft Studio <http://www.zongsoft.com>
 *
 * This file is part of Zongsoft.Messaging.Mqtt library.
 *
 * The Zongsoft.Messaging.Mqtt is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Messaging.Mqtt is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Messaging.Mqtt library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Zongsoft.Messaging.Mqtt
{
	public class MqttSubscriber : IMessageTopicSubscriber<MessageTopicMessage>, IEquatable<MqttSubscriber>
	{
		#region 成员字段
		private readonly MqttQueue _queue;
		#endregion

		#region 构造函数
		public MqttSubscriber(MqttQueue queue, string filter, IEnumerable<string> tags = null)
		{
			_queue = queue ?? throw new ArgumentNullException(nameof(queue));
			this.Filter = filter;
			this.Tags = tags == null ? null : tags.ToArray();
		}
		#endregion

		#region 公共属性
		public MqttQueue Queue { get => _queue; }
		public string Filter { get; }
		public string[] Tags { get; }
		#endregion

		#region 公共方法
		public void Unsubscribe() => _queue.UnsubscribeAsync(this.Filter).GetAwaiter().GetResult();
		public ValueTask UnsubscribeAsync() => _queue.UnsubscribeAsync(this.Filter);
		#endregion

		#region 重写方法
		public bool Equals(MqttSubscriber other) => string.Equals(this.Filter, other.Filter) && string.Equals(this.Tags, other.Tags);
		public override bool Equals(object obj) => obj is MqttSubscriber subscriber && this.Equals(subscriber);
		public override int GetHashCode() => HashCode.Combine(this.Filter, this.Tags);
		public override string ToString() => this.Tags != null && this.Tags.Length > 0 ? $"{this.Filter}:{string.Join(',', this.Tags)}" : this.Filter;
		#endregion

		#region 显式实现
		string IMessageSubscriber.Name => _queue.Name;
		IMessageTopic<MessageTopicMessage> IMessageTopicSubscriber<MessageTopicMessage>.Topic => _queue;
		#endregion
	}
}
