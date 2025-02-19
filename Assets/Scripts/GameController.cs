using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class GameController : MonoBehaviour
	{
		public TextAsset gameBoardLayoutFile;
		public GameBoard gameBoard;
		public PlayingPieceMover playingPieceMover;

		private GameState gameState;
		private List<GameState> gameStateHistory;

		public void Start()
		{
			Initialize();
		}

		public void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				TakeTurn();
			}
		}

		private void Initialize()
		{
			gameStateHistory = new List<GameState>();

			// Create game state.
			gameState = new GameState
			{
				PlayerStateTable = new Dictionary<string, PlayerState>()
			};

			AddGameBoard();
			AddPlayers();
			DeterminePlayerInTurn();

			gameStateHistory.Add(gameState);
		}

		private void AddGameBoard()
		{
			var gameBoardLayout = GetGameBoardLayout();
			gameState.GameBoardLayout = gameBoardLayout;

			Assert.IsNotNull(gameBoard);
			gameBoard.Initialize(gameBoardLayout);
		}

		private GameBoardLayout GetGameBoardLayout()
		{
			Assert.IsNotNull(gameBoardLayoutFile);

			var gameBoardLayoutFileContents = gameBoardLayoutFile.text;
			var gameBoardLayout = JsonConvert.DeserializeObject<GameBoardLayout>(gameBoardLayoutFileContents);

			return gameBoardLayout;
		}

		private void AddPlayers()
		{
			var playerIDs = new List<string>
			{
				"d1589de2-d929-418a-acec-13552a6ed1a"
			};

			foreach (var playerID in playerIDs)
			{
				AddPlayer(playerID);
			}
		}

		private void AddPlayer(string playerID)
		{
			var playerState = new PlayerState
			{
				StepCount = 0,
				Score = 0
			};

			Assert.IsNotNull(gameState.PlayerStateTable);
			gameState.PlayerStateTable.Add(playerID, playerState);

			Assert.IsNotNull(playingPieceMover);
			playingPieceMover.AddPlayingPiece(playerID, playerState);
		}

		private void DeterminePlayerInTurn()
		{
			var playerStateTableElement = gameState.PlayerStateTable.First();
			var playerID = playerStateTableElement.Key;
			gameState.PlayerInTurn = playerID;
		}

		private void TakeTurn()
		{
			// Update state.
			var playerID = gameState.PlayerInTurn;
			var playerState = gameState.PlayerStateTable[playerID];

			var stepCount = Random.Range(1, 6);
			Debug.Log(stepCount);

			// TODO: Create updated state.
			playerState.StepCount += stepCount;

			// Update presentation.
			playingPieceMover.UpdatePlayingPieces(gameState.PlayerStateTable);
		}
	}
}
