using UnityEngine;
using TMPro;
using easyar;

namespace Assets.Scripts.UI
{
	public class ImageTargetDTO : MonoBehaviour
	{
		[SerializeField] private ImageTargetController itc = default;
		[SerializeField] private TextMeshProUGUI modelTXT = default;
		[SerializeField] private TextMeshProUGUI description = default;
		[SerializeField] private Canvas canvas = default;

		private void OnEnable()
		{
			itc.TargetFound += OnFound;
			itc.TargetLost += OnLost;
		}

		private void OnDisable()
		{
			itc.TargetFound -= OnFound;
			itc.TargetLost -= OnLost;
		}
		public void Construct(Camera cam, string model, string description, string path) 
		{
			canvas.worldCamera = cam;
			itc.ImageFileSource.Path = path;
			SetValues(model, description);
		}
		public void SetValues(string model, string description) 
		{
			modelTXT.text = model;
			this.description.text = description;
		}
		private void OnFound() => canvas.enabled = true;
		private void OnLost() => canvas.enabled = false;
	}
}
