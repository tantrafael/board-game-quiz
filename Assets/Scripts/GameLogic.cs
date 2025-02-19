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

			foreach (var gameState in gameStateHistory)
			{
				Debug.Log(gameState.ID);
			}
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

			foreach (var gameState in gameStateHistory)
			{
				Debug.Log(gameState.ID);
			}
		}

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

		private string GetPlayerInTurn(GameState gameState)
		{
			var playerStateTableElement = gameState.PlayerStateTable.First();
			var playerID = playerStateTableElement.Key;

			return playerID;
		}
	}
}
