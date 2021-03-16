using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Infrastructure.ThreadDispatcher;
using Assets.Scripts.UI;
using UnityEngine;
namespace Assets.Scripts.Boot
{
	public class GameBootStrapper : MonoBehaviour, ICoroutineRunner
	{
		public LoadingCurtain LoadingCurtainPrefab = default;
		public Dispatcher dispatcher = default;
		private ARapplication aRapplication = default;
		private void Awake()
		{
			aRapplication = new ARapplication(this, Instantiate(LoadingCurtainPrefab), Instantiate(dispatcher).GetComponent<IDispatcher>());
			aRapplication.StateMachine.Enter<BootstrapState>();
			DontDestroyOnLoad(this);
		}
	}
}