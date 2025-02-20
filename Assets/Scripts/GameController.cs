using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardGameQuiz
{
	public class GameController : MonoBehaviour
	{
		public TextAsset gameBoardLayoutFile;
		public GameBoard gameBoard;
		public PlayingPieceMover playingPieceMover;

		public UserInterfacePanel userInterfacePanel;
		//public GameObject testUIDocument;

		private GameLogic gameLogic = new();
		private GamePresentation gamePresentation = new();
		private UserInterfacePresentation userInterfacePresentation = new();

		public void Start()
		{
			// Mock users.
			var userIDs = new List<string>
			{
				"d1589de2-d929-418a-acec-13552a6ed1a"
			};

			gameLogic.Initialize(gameBoardLayoutFile, userIDs);
			//gamePresentation.Initialize(gameBoard, playingPieceMover);

			var initialGameState = gameLogic.GetCurrentGameState();
			//gamePresentation.Initialize(gameBoard, playingPieceMover, initialGameState);
			gamePresentation.Initialize(gameBoard, playingPieceMover, userInterfacePanel, initialGameState);

			userInterfacePresentation.Initialize(userInterfacePanel, initialGameState);
		}

		public void TakeTurn()
		{
			gameLogic.PlayTurn();

			var currentGameState = gameLogic.GetCurrentGameState();
			gamePresentation.Update(currentGameState);

			userInterfacePresentation.Update(currentGameState);
		}

		public void Test()
		{
			//SceneManager.LoadScene("Test", LoadSceneMode.Single);
			SceneManager.LoadScene("Test", LoadSceneMode.Additive);
		}
	}
}
