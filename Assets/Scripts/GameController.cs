using UnityEngine;

namespace BoardGameQuiz
{
	public class GameController : MonoBehaviour
	{
		public GameBoard gameBoard;
		public PlayerController playerController;

		public void Start()
		{
			// TODO: Assert game board and player controller existence.
			gameBoard.Initialize();
			playerController.Initialize();
		}

		public void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				playerController.Move(3);
			}
		}
	}
}
