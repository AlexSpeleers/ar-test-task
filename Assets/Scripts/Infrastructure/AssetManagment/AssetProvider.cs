using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using System.IO;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
	public class AssetProvider : IAssets
	{
		private List<string> imagePathes = default;
		private ImageDescriptionStorage imageDescriptionStorage = default;
		public GameObject Instantiate(string path)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab);
		}
		public GameObject Instantiate(string path, Transform parent)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, parent);
		}

		public async Task<ImageDescriptionStorage> DownloadAddresables(Action<ImageDescriptionStorage> callback)
		{
			if (imagePathes != null || imagePathes.Count > 0)
			{
				return imageDescriptionStorage;
			}
			
			var locations1 = await Addressables.LoadResourceLocationsAsync(AssetPath.ImageLabel).Task;
			foreach (var location in locations1)
			{
				var obj = await Addressables.LoadAssetAsync<Sprite>(location).Task;
				using (StreamWriter sw = new StreamWriter($"{AssetPath.SaveImagePath}{obj.name}.jpg", false, System.Text.Encoding.Default))
				{
					var imagearray = obj.texture.EncodeToJPG();
					
				}
			}

			var locations2 = await Addressables.LoadResourceLocationsAsync(AssetPath.SOLabel).Task;
			foreach (var location in locations2)
			{
				var obj = await Addressables.LoadAssetAsync<ImageDescriptionStorage>(location).Task;
				imageDescriptionStorages.Add(obj);
			}
			if (imageDescriptionStorages != null || imageDescriptionStorages.Count > 0) 
			{
				callback?.Invoke(imageDescriptionStorages[0]);
			}
			return  imageDescriptionStorages[0];
		}


		public void Dispose()
		{

		}
	}
}
