
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.ThreadDispatcher;
using Assets.Scripts.Services;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.States
{
	public class ApplicationStateMachine
	{
		private readonly Dictionary<Type, IExitableState> states;
		private IExitableState activeState;
		public ApplicationStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services, IDispatcher dispatcher)
		{
			states = new Dictionary<Type, IExitableState>()
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, dispatcher),
				[typeof(LoadMainScene)] = new LoadMainScene(this, sceneLoader, loadingCurtain, services.Single<IFactory>()),
				[typeof(GameLoopState)] = new GameLoopState(this)
			};
		}
		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}
		public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
		{
			TState state = ChangeState<TState>();
			state.Enter(payLoad);
		}
		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			activeState?.Exit();
			TState state = GetState<TState>();
			activeState = state;
			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
		states[typeof(TState)] as TState;
	}
}
