using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace BoardGameQuiz
{
	public class GameLogic
	{
		private List<GameState> gameStateHistory = new();

		public void Initialize(TextAsset gameBoardLayoutFile, List<string> players)
		{
			var gameBoardLayout = CreateGameBoardLayout(gameBoardLayoutFile);
			var playerStateTable = CreatePlayerStateTable(players);

			var initialGameState = new GameState
			{
				ID = 0,
				GameBoardLayout = gameBoardLayout,
				PlayerStateTable = playerStateTable
			};

			gameStateHistory.Add(initialGameState);
		}

		public GameState GetCurrentGameState()
		{
			return gameStateHistory.Last();
		}

		public void PlayTurn()
		{
			var currentGameState = gameStateHistory.Last();

			var updatedGameState = PerformGameLogic(currentGameState);

			gameStateHistory.Add(updatedGameState);
		}

		private GameBoardLayout CreateGameBoardLayout(TextAsset gameBoardLayoutFile)
		{
			var gameBoardLayout = JsonConvert.DeserializeObject<GameBoardLayout>(gameBoardLayoutFile.text);

			return gameBoardLayout;
		}

		private Dictionary<string, PlayerState> CreatePlayerStateTable(List<string> playerIDs)
		{
			var playerStateTable = new Dictionary<string, PlayerState>();

			foreach (var playerID in playerIDs)
			{
				var playerState = CreatePlayerState();
				playerStateTable.Add(playerID, playerState);
			}

			return playerStateTable;
		}

		private PlayerState CreatePlayerState()
		{
			var playerState = new PlayerState
			{
				StepCount = 0,
				Score = 0
			};

			return playerState;
		}

		private GameState PerformGameLogic(GameState gameState)
		{
			var updatedGameState = gameState.CloneJson();

			updatedGameState.ID = gameState.ID + 1;

			// Move player.
			var (activePlayerID, updatedStepCount) = GetPlayerMovement(gameState);
			updatedGameState.PlayerStateTable[activePlayerID].StepCount = updatedStepCount;

			// Check for quiz at updated position.
			var quizID = CheckForQuiz(gameState, updatedStepCount);
			updatedGameState.ActiveQuiz = quizID;

			return updatedGameState;
		}

		private (string, int) GetPlayerMovement(GameState gameState)
		{
			var activePlayerID = GetActivePlayer(gameState);
			var currentPlayerState = gameState.PlayerStateTable[activePlayerID];
			var currentStepCount = currentPlayerState.StepCount;

			// TODO: Get step count interval from settings. Eliminate magic numbers.
			var updatedStepCount = currentStepCount + Random.Range(1, 6);

			return (activePlayerID, updatedStepCount);
		}

		private string GetActivePlayer(GameState gameState)
		{
			// Mock result returning first player.
			var (playerID, playerState) = gameState.PlayerStateTable.First();

			return playerID;
		}

		private string CheckForQuiz(GameState gameState, int updatedStepCount)
		{
			var gameBoardLayout = gameState.GameBoardLayout;
			var gameBoardTileCount = gameBoardLayout.TilePositions.Count;
			var tileIndex = updatedStepCount % gameBoardTileCount;

			var quizPlacement =
				gameBoardLayout.QuizLayout.Find(quizPlacement => quizPlacement.TileIndexes.Contains(tileIndex));

			var quizID = quizPlacement?.ID;

			return quizID;
		}
	}
}
