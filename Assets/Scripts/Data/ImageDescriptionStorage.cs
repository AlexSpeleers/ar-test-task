using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
	[CreateAssetMenu(fileName = "DescriptionStorage", menuName = "ScriptableObjects/Desription")]
	public class ImageDescriptionStorage : ScriptableObject
	{
		[SerializeField] private List<ImageDescription> imageDescriptions = default;
		public List<ImageDescription> ImageDescriptions => imageDescriptions;
	}

	[System.Serializable]
	public class ImageDescription 
	{
		[SerializeField] private string modelName = "";
		[SerializeField] private string modelDecription = "";
		[SerializeField] private Sprite sprite = default;
		public string ModelName => modelName;
		public string ModelDescription => modelDecription;
		public Sprite Sprite => sprite;
	}
}
