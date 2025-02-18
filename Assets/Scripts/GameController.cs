using UnityEngine;

namespace BoardGameQuiz
{
	public class GameController : MonoBehaviour
	{
		public GameBoard gameBoard;
		public PlayerController playerController;

		void Start()
		{
			// TODO: Assert game board and player controller existence.
			gameBoard.Initialize();
			playerController.Initialize();
		}
	}
}
