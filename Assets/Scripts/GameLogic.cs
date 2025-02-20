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
			//var playerInTurn = GetPlayerInTurn(playerStateTable);

			var initialGameState = new GameState
			{
				ID = 0,
				GameBoardLayout = gameBoardLayout,
				PlayerStateTable = playerStateTable
				//PlayerInTurn = playerInTurn
			};

			gameStateHistory.Add(initialGameState);

			/*
			foreach (var gameState in gameStateHistory)
			{
				Debug.Log(gameState.ID);
			}
			*/
		}

		public GameState GetCurrentGameState()
		{
			return gameStateHistory.Last();
		}

		//public void Update()
		public void PlayTurn()
		{
			var currentGameState = gameStateHistory.Last();

			var updatedGameState = PerformGameLogic(currentGameState);

			gameStateHistory.Add(updatedGameState);

			/*
			foreach (var gameState in gameStateHistory)
			{
				var (playerID, playerState) = gameState.PlayerStateTable.First();
				var stepCount = playerState.StepCount;
				Debug.Log($"Game state ID: {gameState.ID}, Player ID: {playerID}, Step count: {stepCount}");
			}

			Debug.Log("--------------------------------------------------------------------------------");
			*/
		}

		private GameBoardLayout CreateGameBoardLayout(TextAsset gameBoardLayoutFile)
		{
			var gameBoardLayoutFileContents = gameBoardLayoutFile.text;
			var gameBoardLayout = JsonConvert.DeserializeObject<GameBoardLayout>(gameBoardLayoutFileContents);

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
		/*

		private GameState PerformGameLogic(GameState inGameState)
		{
			var outGameState = inGameState.CloneJson();

			// Make updates.
			outGameState.ID = inGameState.ID + 1;

			// Determine active player.
			var activePlayerID = inGameState.PlayerStateTable.First().Key;

			var currentPlayerState = inGameState.PlayerStateTable[activePlayerID];
			var currentStepCount = currentPlayerState.StepCount;
			var updatedStepCount = currentStepCount + Random.Range(1, 6);
			outGameState.PlayerStateTable[activePlayerID].StepCount = updatedStepCount;

			return outGameState;
		}
		*/
		private GameState PerformGameLogic(GameState gameState)
		{
			var updatedGameState = gameState.CloneJson();

			updatedGameState.ID = gameState.ID + 1;

			// Move player.
			//MovePlayer(ref updatedGameState);
			var (activePlayerID, updatedStepCount) = GetPlayerMovement(gameState);
			updatedGameState.PlayerStateTable[activePlayerID].StepCount = updatedStepCount;

			// Check special tiles.
			var gameBoardLayout = gameState.GameBoardLayout;
			var gameBoardTileCount = gameBoardLayout.TilePositions.Count;
			var tileIndex = updatedStepCount % gameBoardTileCount;

			var quizPlacement = gameBoardLayout.QuizLayout.Find(quizPlacement => quizPlacement.TileIndexes.Contains(tileIndex));

			if (quizPlacement != null)
			{
				Debug.Log($"Quiz: {quizPlacement.ID}");
				updatedGameState.ActiveQuiz = quizPlacement.ID;
			}

			return updatedGameState;
		}

		private string GetActivePlayer(GameState gameState)
		{
			var (playerID, playerState) = gameState.PlayerStateTable.First();

			return playerID;
		}

		/*
		private int MovePlayer(string playerID, GameState inGameState)
		{
			var currentPlayerState = inGameState.PlayerStateTable[playerID];
			var currentStepCount = currentPlayerState.StepCount;
			var updatedStepCount = currentStepCount + Random.Range(1, 6);

			return updatedStepCount;
		}
		*/
		/*
		private void MovePlayer(ref GameState gameState)
		{
			var activePlayerID = GetActivePlayer(gameState);
			var currentPlayerState = gameState.PlayerStateTable[activePlayerID];
			var currentStepCount = currentPlayerState.StepCount;
			var updatedStepCount = currentStepCount + Random.Range(1, 6);

			gameState.PlayerStateTable[activePlayerID].StepCount = updatedStepCount;
		}
		*/

		private (string, int) GetPlayerMovement(GameState gameState)
		{
			var activePlayerID = GetActivePlayer(gameState);
			var currentPlayerState = gameState.PlayerStateTable[activePlayerID];
			var currentStepCount = currentPlayerState.StepCount;
			// TODO: Get step count interval from settings. Eliminate magic numbers.
			var updatedStepCount = currentStepCount + Random.Range(1, 6);

			return (activePlayerID, updatedStepCount);
		}
	}
}
