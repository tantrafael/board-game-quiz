using System.Collections.Generic;
using UnityEngine;

namespace BoardGameQuiz
{
	public class GameController : MonoBehaviour
	{
		public TextAsset gameBoardLayoutFile;
		public GameBoard gameBoard;
		public PlayingPieceMover playingPieceMover;

		private GameLogic gameLogic = new();
		private GamePresentation gamePresentation = new();

		public void Start()
		{
			var userIDs = new List<string>
			{
				"d1589de2-d929-418a-acec-13552a6ed1a"
			};

			gameLogic.Initialize(gameBoardLayoutFile, userIDs);
			//gamePresentation.Initialize(gameBoard, playingPieceMover);

			var initialGameState = gameLogic.GetCurrentGameState();
			gamePresentation.Initialize(gameBoard, playingPieceMover, initialGameState);
		}

		public void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				gameLogic.PlayTurn();

				var currentGameState = gameLogic.GetCurrentGameState();
				gamePresentation.Update(currentGameState);
			}
		}
	}
}
