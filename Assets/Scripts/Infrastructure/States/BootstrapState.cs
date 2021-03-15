using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Services;

namespace Assets.Scripts.Infrastructure.States
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private const string AplicationScene = "Main";
		private readonly ApplicationStateMachine applicationStateMachine = default;
		private readonly SceneLoader sceneLoader = default;
		private readonly AllServices services = default;

		public BootstrapState(ApplicationStateMachine applicationStateMachine, SceneLoader sceneLoader, AllServices services)
		{
			this.applicationStateMachine = applicationStateMachine;
			this.sceneLoader = sceneLoader;
			this.services = services;
			RegisterServices();
		}

		public void Enter()
		{
			sceneLoader.Load(Initial, EnterLoadLevel);
		}

		public void Exit()
		{
			
		}
		private void RegisterServices()
		{
			services.RegisterSingle<IAssets>(new AssetProvider());
			services.RegisterSingle<IFactory>(new AssetFactory(services.Single<IAssets>()));
		}
		private void EnterLoadLevel() 
		{
			applicationStateMachine.Enter<LoadMainScene, string>(AplicationScene); 
		}
	}
}
