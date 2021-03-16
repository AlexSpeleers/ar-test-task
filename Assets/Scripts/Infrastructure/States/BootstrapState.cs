using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.ThreadDispatcher;
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
		private readonly IDispatcher dispatcher = default;

		public BootstrapState(ApplicationStateMachine applicationStateMachine, SceneLoader sceneLoader, AllServices services, IDispatcher dispatcher)
		{
			this.applicationStateMachine = applicationStateMachine;
			this.sceneLoader = sceneLoader;
			this.services = services;
			this.dispatcher = dispatcher;
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
			services.RegisterSingle(dispatcher);
			services.RegisterSingle<IAssets>(new AssetProvider(services.Single<IDispatcher>()));
			services.RegisterSingle<IFactory>(new AssetFactory(services.Single<IAssets>()));
		}
		private void EnterLoadLevel() 
		{
			applicationStateMachine.Enter<LoadMainScene, string>(AplicationScene); 
		}
	}
}
