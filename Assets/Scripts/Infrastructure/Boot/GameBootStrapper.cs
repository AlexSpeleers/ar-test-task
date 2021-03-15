using Assets.Scripts.Infrastructure;
using Assets.Scripts.UI;
using UnityEngine;
namespace Assets.Scripts.Boot
{
	public class GameBootStrapper : MonoBehaviour, ICoroutineRunner
	{
		public LoadingCurtain LoadingCurtainPrefab = default;
		private ARapplication aRapplication = default;
		private void Awake()
		{
			aRapplication = new ARapplication(this, Instantiate(LoadingCurtainPrefab));
		}
	}
}