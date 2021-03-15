using Assets.Scripts.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using System.IO;

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
			var locations1 = await Addressables.LoadResourceLocationsAsync(AssetPath.SOLabel).Task;
			foreach (var location in locations1)
			{
				var obj = await Addressables.LoadAssetAsync<ImageDescriptionStorage>(location).Task;
				imageDescriptionStorage = obj;
			}
			var locations2 = await Addressables.LoadResourceLocationsAsync(AssetPath.ImageLabel).Task;
			DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
			if (dir.GetFiles().Length < locations2.Count) 
			{
				foreach(FileInfo file in dir.GetFiles())
				{
					file.Delete();
				}
				for (int i = 0; i < locations2.Count; i++)
				{
					var obj = await Addressables.LoadAssetAsync<Sprite>(locations2[i]).Task;
					var imageArray = obj.texture.EncodeToJPG();
					SaveBytesToFile($"{Application.streamingAssetsPath}{imageDescriptionStorage.GetPathByModelName(obj.name)}", imageArray);
				}
			}
			return imageDescriptionStorage;
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
