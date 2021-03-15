using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.AssetManagment;
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
			assets.Instantiate(AssetPath.UIContainer).GetComponent<UIContainer>().Construct(this);
		}

		public ImageTargetDTO CreateImageTarget()
		{
			return assets.Instantiate(AssetPath.ImageTarget).GetComponent<ImageTargetDTO>();
		}

		public ImageDescriptionStorage GetImageDescriptionStorage(Action<ImageDescriptionStorage> callback) 
		{
			return assets.DownloadAddresables(callback).Result;
		}

		public void Dispose()
		{
		}
	}
}
