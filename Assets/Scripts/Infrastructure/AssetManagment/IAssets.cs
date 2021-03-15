using Assets.Scripts.Data;
using Assets.Scripts.Services;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.AssetManagment
{
	public interface IAssets: IService
	{
		GameObject Instantiate(string path);
		GameObject Instantiate(string path, Transform parent);
		Task<ImageDescriptionStorage> DownloadTargets(Action<ImageDescriptionStorage> callback);
	}
}
