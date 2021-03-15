using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Services;
using Assets.Scripts.UI;
using System;

namespace Assets.Scripts.Infrastructure.States
{
	public class LoadMainScene : IPayLoadedState<string>
	{
		private readonly ApplicationStateMachine applicationStateMachine = default;
		private readonly SceneLoader sceneLoader = default;
		private readonly LoadingCurtain curtain = default;
		private readonly IFactory factory = default;

		public LoadMainScene(ApplicationStateMachine applicationStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IFactory factory)
		{
			this.applicationStateMachine = applicationStateMachine;
			this.sceneLoader = sceneLoader;
			this.curtain = curtain;
			this.factory = factory;
		}
		public void Enter(string sceneName)
		{
			curtain.Show();
			sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit()
		{
			curtain.Hide();
		}
		private void OnLoaded()
		{
			PopulateHud();
			applicationStateMachine.Enter<GameLoopState>();
		}

		private void PopulateHud()
		{
			factory.CreateCanvas();
		}
	}
}
