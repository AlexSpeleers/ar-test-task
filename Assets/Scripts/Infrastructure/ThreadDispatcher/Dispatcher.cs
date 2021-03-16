using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ThreadDispatcher
{
	public class Dispatcher : MonoBehaviour, IDispatcher
	{
		private Queue<Action<ImageDescriptionStorage>> pending = new Queue<Action<ImageDescriptionStorage>>();
		private Queue<ImageDescriptionStorage> desriptions = new Queue<ImageDescriptionStorage>();

		public void AddInvoke(Action<ImageDescriptionStorage> fn, ImageDescriptionStorage imageDescription)
		{
			lock (this.pending)
			{
				this.pending.Enqueue(fn);
			}
		}

		private void InvokePending()
		{
			lock (this.pending)
			{
				if (pending.Count > 0)
				{
					var action = pending.Dequeue();
					action(desriptions.Dequeue());
				}
			}
		}

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		private void Update()
		{
			this.InvokePending();
		}

		public void Dispose()
		{

		}
	}
}
