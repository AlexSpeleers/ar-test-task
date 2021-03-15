using UnityEngine;
using easyar;

namespace Assets.Scripts.Infrastructure.Logic
{
	public class EasyARDTO : MonoBehaviour
	{
		[SerializeField] private ImageTrackerFrameFilter imageTracker = default;
		public ImageTrackerFrameFilter ImageTracker => imageTracker;
	}
}
