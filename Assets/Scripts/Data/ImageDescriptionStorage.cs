using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
	[CreateAssetMenu(fileName = "DescriptionStorage", menuName = "ScriptableObjects/Desription")]
	public class ImageDescriptionStorage : ScriptableObject
	{
		[SerializeField] private List<ImageDescription> imageDescriptions = default;
		public List<ImageDescription> ImageDescriptions => imageDescriptions;
		public string GetPathByModelName(string name) => imageDescriptions.Find(x => x.ModelName == name).Path;
	}

	[System.Serializable]
	public class ImageDescription 
	{
		[SerializeField] private string modelName;
		[SerializeField] private string modelDecription;
		[SerializeField] private string path;
		public string ModelName => modelName;
		public string ModelDescription => modelDecription;
		public string Path => path;
	}
}
