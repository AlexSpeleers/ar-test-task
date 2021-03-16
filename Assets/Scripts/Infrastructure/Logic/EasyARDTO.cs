using UnityEngine;
using easyar;
namespace Assets.Scripts.Infrastructure.Logic
{
	public class EasyARDTO : MonoBehaviour
	{
		[SerializeField] private ARSession aRSession = default;
		[SerializeField] private ImageTrackerFrameFilter imageTracker = default;
		[SerializeField] private RenderCameraController renderCameraController = default;
		public ImageTrackerFrameFilter ImageTracker => imageTracker;
		public ARSession ARSession => aRSession;
		public RenderCameraController RenderCameraController => renderCameraController;
	}
}
