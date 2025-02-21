using System.Collections.Generic;
using UnityEngine;

namespace BoardGameQuiz
{
	public class GameController : MonoBehaviour
	{
		public TextAsset gameBoardLayoutFile;
		public GameBoard gameBoard;
		public PlayingPieceMover playingPieceMover;
		public QuizPresenter quizPresenter;
		public CameraDirector cameraDirector;

		private GameLogic gameLogic = new();
		private GamePresentation gamePresentation = new();

		public void Start()
		{
			// Mock users.
			var userIDs = new List<string>
			{
				"d1589de2-d929-418a-acec-13552a6ed1a"
			};

			gameLogic.Initialize(gameBoardLayoutFile, userIDs);

			var initialGameState = gameLogic.GetCurrentGameState();
			gamePresentation.Initialize(gameBoard, playingPieceMover, quizPresenter, cameraDirector, initialGameState);
		}

		public void PlayTurn()
		{
			gameLogic.PlayTurn();

			var currentGameState = gameLogic.GetCurrentGameState();
			gamePresentation.Update(currentGameState);
		}
	}
}
