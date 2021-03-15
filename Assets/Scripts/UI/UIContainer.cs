using Assets.Scripts.Infrastructure.Factory;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class UIContainer: MonoBehaviour
	{
		[SerializeField] private MainPanel mainPanel = default;
		public MainPanel MainPanel => mainPanel;

		public void Construct(IFactory factory)
		{
			mainPanel.Construct(factory);
		}
	}
}
