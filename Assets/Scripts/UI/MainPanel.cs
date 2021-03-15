using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Factory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class MainPanel : MonoBehaviour
	{
		[SerializeField] private Button uploadButton = default;
		[SerializeField] private List<ImageTargetDTO> imageTargetDTOs = default;
		private ImageDescriptionStorage imageDescriptionStorage = default;
		private IFactory factory = default;
		private Camera cashedCam = default;
		private void Awake()
		{
			cashedCam = Camera.main;
		}
		public void Construct(IFactory factory)
		{
			this.factory = factory;
			PrepareButtons();
		}

		public void CreateTargets(string model, string description)
		{
			for (int i = 0; i < 5; i++)
			{
				var target = factory.CreateImageTarget();
				imageTargetDTOs.Add(target);
			}
		}

		private void PrepareButtons() 
		{
			uploadButton.onClick.RemoveAllListeners();
			uploadButton.onClick.AddListener(() =>
			{
				Upload();
			});
		}

		private void Upload() 
		{
			if (imageDescriptionStorage != null) 
			{
				PopulateTargets(imageDescriptionStorage);
			}
			if (Application.internetReachability == NetworkReachability.NotReachable) 
			{
				return;
			}
			imageDescriptionStorage = factory.GetImageDescriptionStorage(PopulateTargets);
		}

		private void PopulateTargets(ImageDescriptionStorage imageDescriptionStorage) 
		{
			uploadButton.gameObject.SetActive(false);
			foreach (var image in imageDescriptionStorage.ImageDescriptions)
			{
				var target = factory.CreateImageTarget();
				target.Construct(cashedCam, image.ModelDescription, image.ModelName);
			}
		}
	}
}
