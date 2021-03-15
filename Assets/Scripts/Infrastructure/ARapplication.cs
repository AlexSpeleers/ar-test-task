using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Services;
using Assets.Scripts.UI;

namespace Assets.Scripts.Infrastructure
{
	public class ARapplication
	{
		public ApplicationStateMachine StateMachine { get; private set; }
		public ARapplication(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
		{
			StateMachine = new ApplicationStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
		}
	}
}
