namespace Assets.Scripts.Infrastructure.States
{
	public class GameLoopState : IState
	{
		private ApplicationStateMachine applicationStateMachine = default;
		public GameLoopState(ApplicationStateMachine applicationStateMachine) => this.applicationStateMachine = applicationStateMachine;
		public void Enter()
		{

		}

		public void Exit()
		{

		}
	}
}