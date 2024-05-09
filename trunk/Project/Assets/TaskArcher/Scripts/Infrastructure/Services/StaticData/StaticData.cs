using UnityEngine;

namespace TaskArcher.Infrastructure.Services.StaticData
{
	public class StaticData : MonoBehaviour
	{
		public static StaticData Instance;

		[SerializeField] private GameStaticData gameStaticData;
		
		public GameStaticData GameStaticData => gameStaticData;
		
		private void Awake()
		{
			if (Instance != null) 
			{
				return;
			}
			Instance = this;
		}
	}
}
