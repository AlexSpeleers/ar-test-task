using Assets.Scripts.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using System.IO;
using Assets.Scripts.Infrastructure.ThreadDispatcher;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
	public class AssetProvider : IAssets
	{
		private List<IResourceLocation> remotes;
		private IDispatcher dispatcher = default;
		private ImageDescriptionStorage imageDescriptionStorage = default;
		private Action<ImageDescriptionStorage> callback = default;
		private int assetCounter = 0;
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
			assetCounter = 0;
			this.callback = callback;
			GetStorage(AssetPath.SOLabel);
		}
		private void GetStorage(string label)
		{
			Addressables.LoadResourceLocationsAsync(label).Completed += LocationLoaded;
		}
		private void LocationLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj) 
		{
			remotes = new List<IResourceLocation>(obj.Result);
			Addressables.LoadAssetAsync<ImageDescriptionStorage>(remotes[0]).Completed += StorageAssetLoaded;
		}
		private void StorageAssetLoaded(AsyncOperationHandle<ImageDescriptionStorage> resource) 
		{
			imageDescriptionStorage = resource.Result;
			GetImages(AssetPath.ImageLabel);
		}
		private void GetImages(string label) 
		{
			Addressables.LoadResourceLocationsAsync(label).Completed += ImageLocationLoaded;
		}
		private void ImageLocationLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj) 
		{
			remotes = new List<IResourceLocation>(obj.Result);
			if (!Directory.Exists(Path.GetDirectoryName($"{Application.persistentDataPath}{AssetPath.ImageDirectory}/sss")))
				Directory.CreateDirectory($"{Application.persistentDataPath}{AssetPath.ImageDirectory}");
			DirectoryInfo dir = new DirectoryInfo($"{Application.persistentDataPath}{AssetPath.ImageDirectory}");
			if (dir.GetFiles().Length < remotes.Count)
			{
				foreach (FileInfo file in dir.GetFiles())
				{
					file.Delete();
				}
				for (int i = 0; i < remotes.Count; i++)
				{
					Addressables.LoadAssetAsync<Texture2D>(remotes[i]).Completed += ImageAssetloaded;
				}
			}
			else 
			{
				dispatcher.AddInvoke(callback, imageDescriptionStorage);
			}
		}
		private void ImageAssetloaded(AsyncOperationHandle<Texture2D> resource)
		{
			assetCounter++;
			var image = resource.Result;
			var imageArray = image.EncodeToJPG();
			SaveBytesToFile($"{Application.persistentDataPath}{AssetPath.ImageDirectory}/{imageDescriptionStorage.GetPathByModelName(image.name)}", imageArray);
			if (assetCounter == imageDescriptionStorage.ImageDescriptions.Count - 1)
			{
				dispatcher.AddInvoke(callback, imageDescriptionStorage);
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
