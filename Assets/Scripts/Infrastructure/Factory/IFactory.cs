using Assets.Scripts.Data;
using Assets.Scripts.Services;
using Assets.Scripts.UI;
using System;

namespace Assets.Scripts.Infrastructure.Factory
{
	public interface IFactory : IService
	{
		void CreateCanvas();
		ImageTargetDTO CreateImageTarget();
		void GetImageDescriptionStorage(Action<ImageDescriptionStorage> callback);
	}
}
