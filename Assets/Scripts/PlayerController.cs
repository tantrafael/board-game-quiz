using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class PlayerController : MonoBehaviour
	{
		public GameBoard gameBoard;
		public GameObject playerMarkerAsset;
		public float tileStepDuration;

		/*
		private GameState gameState;
		private GameObject playerMarker;
		private int tileIndex;
		*/

		private List<GameObject> playerMarkers = new();

		/*
		public void Initialize()
		{
			var startPosition = gameBoard.GetWorldPosition(tileIndex);
			playerMarker = Instantiate(playerMarkerAsset, startPosition, Quaternion.identity);
		}
		*/
		/*
		public void Initialize(GameState gameState)
		{
			this.gameState = gameState;
			var startPosition = gameBoard.GetWorldPosition(tileIndex);
			playerMarker = Instantiate(playerMarkerAsset, startPosition, Quaternion.identity);
		}
		*/

		public void AddPlayer(PlayerState playerState)
		{
			var startPosition = gameBoard.GetWorldPosition(playerState.TileIndex);
			var playerMarker = Instantiate(playerMarkerAsset, startPosition, Quaternion.identity);

			Assert.IsNotNull(playerMarkers);
			playerMarkers.Add(playerMarker);
		}

		public void UpdatePlayerMarkers(List<PlayerState> playerStates)
		{
			foreach (var playerState in playerStates)
			{
				UpdatePlayerMarker(playerState);
			}
		}

		private void UpdatePlayerMarker(PlayerState playerState)
		{
			var playerID = 0;
			var playerMarker = playerMarkers[playerID];
			var playerMarkerWorldPosition = gameBoard.GetWorldPosition(playerState.TileIndex);

			playerMarker.transform.position = playerMarkerWorldPosition;
		}

		/*
		public int Move(int stepCount)
		{
			var totalTileCount = gameBoard.GetTotalTileCount();
			var tileIndexSequence = GetIndexSequence(tileIndex, stepCount, totalTileCount);
			//var destinationTileIndex = tileIndexSequence[^1];
			var destinationTileIndex = tileIndexSequence.Last();

			Animate(playerMarker, gameBoard, tileIndexSequence, tileStepDuration);

			tileIndex = destinationTileIndex;

			return destinationTileIndex;
		}
		*/

		private List<int> GetIndexSequence(int startIndex, int stepCount, int totalIndexCount)
		{
			var indexSequence = new List<int>();

			for (var i = 0; i < stepCount; i++)
			{
				var index = (startIndex + 1 + i) % totalIndexCount;
				indexSequence.Add(index);
			}

			return indexSequence;
		}

		private void Animate(GameObject markerInstance, GameBoard gameBoard, List<int> tileIndexSequence, float tileStepDuration)
		{
			// TODO: Assert marker instance existence.
			Sequence sequence = DOTween.Sequence();

			foreach (var tileIndex in tileIndexSequence)
			{
				var tileWorldPosition = gameBoard.GetWorldPosition(tileIndex);
				Tweener tweener = markerInstance.transform.DOMove(tileWorldPosition, tileStepDuration);
				sequence.Append(tweener);
			}
		}
	}
}
