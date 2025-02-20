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
		//public UserInterfacePanel userInterfacePanel;

		private GameLogic gameLogic = new();
		private GamePresentation gamePresentation = new();
		//private UserInterfacePresentation userInterfacePresentation = new();

		/*
		private void OnEnable()
		{
			EventManager.PlayClicked += EventManagerOnPlayClicked;
		}

		private void OnDisable()
		{
			EventManager.PlayClicked -= EventManagerOnPlayClicked;
		}
		*/

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
			gamePresentation.Initialize(gameBoard, playingPieceMover, quizPresenter, initialGameState);
			//gamePresentation.Initialize(gameBoard, playingPieceMover, userInterfacePanel, initialGameState);

			//userInterfacePresentation.Initialize(userInterfacePanel, initialGameState);
		}

		public void PlayTurn()
		{
			Debug.Log("GameController::PLayTurn()");

			gameLogic.PlayTurn();

			var currentGameState = gameLogic.GetCurrentGameState();
			gamePresentation.Update(currentGameState);

			//userInterfacePresentation.Update(currentGameState);
		}

		/*
		private void EventManagerOnPlayClicked()
		{
			Debug.Log("EventManagerOnPlayClicked()");

			PlayTurn();
		}
		*/

		/*
		public void Test()
		{
			//SceneManager.LoadScene("Test", LoadSceneMode.Single);
			SceneManager.LoadScene("Test", LoadSceneMode.Additive);
		}
		*/
	}
}
