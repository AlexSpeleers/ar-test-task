using Assets.Scripts.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using System.IO;
using Assets.Scripts.Infrastructure.ThreadDispatcher;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
	public class AssetProvider : IAssets
	{
		private IDispatcher dispatcher = default;
		private ImageDescriptionStorage imageDescriptionStorage = default;
		public AssetProvider(IDispatcher dispatcher)
		{
			this.dispatcher = dispatcher;
		}

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

		public void DownloadTargets(Action<ImageDescriptionStorage> callback) 
		{
			Task.Run(async () =>
			{
				await GetStorage(AssetPath.SOLabel);
				await GetImages(AssetPath.ImageLabel);
				dispatcher.AddInvoke(callback, imageDescriptionStorage);
			});
		}
		private async Task GetStorage(string label)
		{
			var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
			var obj = await Addressables.LoadAssetAsync<ImageDescriptionStorage>(locations[0]).Task;
			imageDescriptionStorage = obj;
		}
		private async Task GetImages(string label) 
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
