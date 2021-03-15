using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Logic;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class UIContainer: MonoBehaviour
	{
		[SerializeField] private MainPanel mainPanel = default;
		public MainPanel MainPanel => mainPanel;

		public void Construct(IFactory factory, EasyARDTO easyARDTO)
		{
			mainPanel.Construct(factory, easyARDTO);
		}
	}
}
