using UnityEngine;
namespace Assets.Scripts.Boot
{
	public class EntryPoint: MonoBehaviour
	{
		public GameObject GameBootstrapper = default;
		private void Awake()
		{
			var bootStrapper = FindObjectOfType<GameBootStrapper>();
			if (bootStrapper == null)
			{
				Instantiate(GameBootstrapper);
			}
		}
	}
}
