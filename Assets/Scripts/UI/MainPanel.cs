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
			SetUpEaseARSession();
		}

		private void SetUpEaseARSession() 
		{
			easyARDTO.RenderCameraController.TargetCamera = cashedCam;
			easyARDTO.ARSession.Assembly.Camera = cashedCam;
			easyARDTO.ARSession.Assembly.CameraRoot = cashedCam.transform;
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
				factory.GetImageDescriptionStorage(PopulateTargets);
			}
		}

		private void PopulateTargets(ImageDescriptionStorage imageDescriptionStorage) 
		{
			this.imageDescriptionStorage = imageDescriptionStorage;
			if (imageDescriptionStorage == null)
			{
				EnableButton();
			}
			else
			{
				easyARDTO.ImageTracker.SimultaneousNum = imageDescriptionStorage.ImageDescriptions.Count;
				foreach (var image in imageDescriptionStorage.ImageDescriptions)
				{
					var target = factory.CreateImageTarget();
					target.Construct(cashedCam, image.ModelName, image.ModelDescription, image.Path, easyARDTO.ImageTracker);
					imageTargetDTOs.Add(target);
				}
			}
		}
		private void DisableButton() => uploadButton.gameObject.SetActive(false);
		private void EnableButton() => uploadButton.gameObject.SetActive(true);
	}
}
