using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class GameController : MonoBehaviour
	{
		public TextAsset gameBoardLayoutFile;
		public GameBoard gameBoard;
		public PlayerController playerController;

		private GameState gameState;

		public void Start()
		{
			Initialize();
		}

		private void Initialize()
		{
			// Create game state.
			gameState = new GameState
			{
				PlayerStates = new List<PlayerState>()
			};

			// Get game board.
			var gameBoardLayout = GetGameBoardLayout(gameBoardLayoutFile);

			// Add game board.
			gameState.GameBoardLayout = gameBoardLayout;

			Assert.IsNotNull(gameBoard);
			gameBoard.Initialize(gameBoardLayout);

			// Add a player.
			var playerState = new PlayerState
			{
				ID = "d1589de2-d929-418a-acec-13552a6ed1a4",
				TileIndex = 0,
				Score = 0
			};

			Assert.IsNotNull(gameState.PlayerStates);
			gameState.PlayerStates.Add(playerState);

			Assert.IsNotNull(playerController);
			//playerController.Initialize();
			playerController.AddPlayer(playerState);
		}

		public void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				//playerController.Move(3);

				// Update state.
				var playerID = 0;
				var playerState = gameState.PlayerStates[playerID];
				var currentTileIndex = playerState.TileIndex;
				var stepCount = Random.Range(1, 6);
				var tileCount = gameState.GameBoardLayout.TilePositions.Count;
				var updatedTileIndex = (currentTileIndex + stepCount) % tileCount;
				playerState.TileIndex = updatedTileIndex;

				// Update presentation.
				playerController.UpdatePlayerMarkers(gameState.PlayerStates);
			}
		}

		private GameBoardLayout GetGameBoardLayout(TextAsset gameBoardLayoutFile)
		{
			Assert.IsNotNull(gameBoardLayoutFile);

			var gameBoardLayoutFileContents = gameBoardLayoutFile.text;
			var gameBoardLayout = JsonConvert.DeserializeObject<GameBoardLayout>(gameBoardLayoutFileContents);

			return gameBoardLayout;
		}
	}
}
