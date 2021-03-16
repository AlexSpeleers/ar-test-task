using Assets.Scripts.Data;
using Assets.Scripts.Services;
using System;

namespace Assets.Scripts.Infrastructure.ThreadDispatcher
{
	public interface IDispatcher : IService
	{
		void AddInvoke(Action<ImageDescriptionStorage> fn, ImageDescriptionStorage imageDescription);
	}
}