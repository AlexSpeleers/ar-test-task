using Assets.Scripts.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using System.IO;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
	public class AssetProvider : IAssets
	{
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

		public async Task<ImageDescriptionStorage> DownloadTargets(Action<ImageDescriptionStorage> callback)
		{
			imageDescriptionStorage = GetStorage(AssetPath.SOLabel).Result;
			await GetImages(AssetPath.ImageLabel);
			callback?.Invoke(imageDescriptionStorage);
			return imageDescriptionStorage;
		}
		public async Task<ImageDescriptionStorage> GetStorage(string label)
		{
			var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
			var obj = await Addressables.LoadAssetAsync<ImageDescriptionStorage>(locations[0]).Task;
			return obj;
		}
		public async Task GetImages(string label) 
		{
			var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
			DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
			if (dir.GetFiles().Length < locations.Count)
			{
				foreach (FileInfo file in dir.GetFiles())
				{
					file.Delete();
				}
				for (int i = 0; i < locations.Count; i++)
				{
					var obj = await Addressables.LoadAssetAsync<Sprite>(locations[i]).Task;
					var imageArray = obj.texture.EncodeToJPG();
					SaveBytesToFile($"{Application.streamingAssetsPath}{imageDescriptionStorage.GetPathByModelName(obj.name)}", imageArray);
				}
			}
		}
		public void SaveBytesToFile(string filename, byte[] bytesToWrite)
		{
			if (filename != null && filename.Length > 0 && bytesToWrite != null)
			{
				if (!Directory.Exists(Path.GetDirectoryName(filename)))
					Directory.CreateDirectory(Path.GetDirectoryName(filename));
				FileStream file = File.Create(filename);
				file.Write(bytesToWrite, 0, bytesToWrite.Length);
				file.Close();
			}
		}

		public void Dispose()
		{

		}
	}
}
