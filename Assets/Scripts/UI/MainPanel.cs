using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Logic;
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
		private EasyARDTO easyARDTO = default;
		private void Awake()
		{
			cashedCam = Camera.main;
		}
		public void Construct(IFactory factory, EasyARDTO easyARDTO)
		{
			this.factory = factory;
			this.easyARDTO = easyARDTO;
			PrepareButtons();
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
			if (Application.internetReachability != NetworkReachability.NotReachable)
			{
				if (imageDescriptionStorage != null)
				{
					PopulateTargets(imageDescriptionStorage);
					DisableButton();
					return;
				}
				DisableButton();
				imageDescriptionStorage = factory.GetImageDescriptionStorage(PopulateTargets);
			}
		}

		private void PopulateTargets(ImageDescriptionStorage imageDescriptionStorage) 
		{
			if (imageDescriptionStorage == null) 
			{
				EnableButton();
				return;
			}
			foreach (var image in imageDescriptionStorage.ImageDescriptions)
			{
				var target = factory.CreateImageTarget();
				target.Construct(cashedCam, image.ModelDescription, image.ModelName, image.Path, easyARDTO.ImageTracker);
			}
		}
		private void DisableButton() => uploadButton.enabled= false;
		private void EnableButton() => uploadButton.enabled = true;
	}
}
