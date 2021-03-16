using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ThreadDispatcher
{
	public class Dispatcher : MonoBehaviour, IDispatcher
	{
		private List<Action<ImageDescriptionStorage>> pending = new List<Action<ImageDescriptionStorage>>();
		private List<ImageDescriptionStorage> desriptions = new List<ImageDescriptionStorage>();

		public void AddInvoke(Action<ImageDescriptionStorage> fn, ImageDescriptionStorage imageDescription)
		{
			lock (this.pending)
			{
				this.pending.Add(fn);
			}
		}

		private void InvokePending()
		{
			lock (this.pending)
			{
				for (int i = 0; i < pending.Count; i++)
				{
					var action = pending[i];
					action(desriptions[i]);
				}
				this.pending.Clear();
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
