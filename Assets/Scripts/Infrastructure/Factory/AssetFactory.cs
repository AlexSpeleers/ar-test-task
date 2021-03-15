using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Logic;
using Assets.Scripts.UI;
using System;

namespace Assets.Scripts.Infrastructure.Factory
{
	public class AssetFactory : IFactory
	{
		private readonly IAssets assets;

		public AssetFactory(IAssets assets)
		{
			this.assets = assets;
		}
		public void CreateCanvas()
		{
			assets.Instantiate(AssetPath.UIContainer).GetComponent<UIContainer>().Construct(this, CreateImageTracker());
		}

		public ImageTargetDTO CreateImageTarget()
		{
			return assets.Instantiate(AssetPath.ImageTarget).GetComponent<ImageTargetDTO>();
		}

		public ImageDescriptionStorage GetImageDescriptionStorage(Action<ImageDescriptionStorage> callback) 
		{
			return assets.DownloadTargets(callback).Result;
		}
		private EasyARDTO CreateImageTracker()
		{
			return assets.Instantiate(AssetPath.EasyARTrracker).GetComponent<EasyARDTO>();
		}

		public void Dispose()
		{
		}
	}
}
