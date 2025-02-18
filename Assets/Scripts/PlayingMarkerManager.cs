using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;

namespace BoardGameQuiz
{

	public class PlayingMarkerLocalState
	{
		//public int TileIndex { get; set; }
		public int StepCount { get; set; }
		public GameObject PlayingMarker { get; set; }
	}

	public class PlayingMarkerManager : MonoBehaviour
	{
		public GameBoard gameBoard;
		public GameObject playerMarkerAsset;
		public float tileStepDuration;

		//private List<GameObject> playingMarkers = new();
		private Dictionary<string, PlayingMarkerLocalState> playingMarkerTable = new();

		public void AddPlayingMarker(PlayerState playerState)
		{
			var playerID = playerState.ID;
			//var playerTileIndex = playerState.TileIndex;
			var playerTileIndex = playerState.StepCount % gameBoard.GetTotalTileCount();
			var startPosition = gameBoard.GetWorldPosition(playerTileIndex);
			var playingMarker = Instantiate(playerMarkerAsset, startPosition, Quaternion.identity);

			//Assert.IsNotNull(playingMarkers);
			//playingMarkers.Add(playingMarker);

			var playingMarkerLocalState = new PlayingMarkerLocalState
			{
				StepCount = playerState.StepCount,
				PlayingMarker = playingMarker
			};

			Assert.IsNotNull(playingMarkerTable);
			playingMarkerTable.Add(playerID, playingMarkerLocalState);
		}


		public void UpdatePlayingMarkers(List<PlayerState> playerStates)
		{
			foreach (var playerState in playerStates)
			{
				UpdatePlayingMarker(playerState);
			}
		}

		private void UpdatePlayingMarker(PlayerState playerState)
		{
			var playerID = playerState.ID;
			var playingMarkerLocalState = playingMarkerTable[playerID];

			//var playingMarkerWorldPosition = gameBoard.GetWorldPosition(playerTileIndex);
			//playingMarker.transform.position = playingMarkerWorldPosition;

			var deltaStepCount = playerState.StepCount - playingMarkerLocalState.StepCount;
			var isUpToDate = (deltaStepCount == 0);

			if (isUpToDate == false)
			{
				//var startTileIndex = playingMarkerLocalState.TileIndex;
				//MovePlayingMarker(playingMarker, startTileIndex, deltaStepCount);
				//MovePlayingMarker(playingMarkerLocalState, deltaStepCount);
				MovePlayingMarker(playingMarkerLocalState, playerState.StepCount);
			}
		}

		//public int MovePlayingMarker(GameObject playingMarker, int startTileIndex, int stepCount)
		//private int MovePlayingMarker(GameObject playingMarker, int startTileIndex, int stepCount)
		private void MovePlayingMarker(PlayingMarkerLocalState playingMarkerLocalState, int stepCount)
		{
			var playingMarker = playingMarkerLocalState.PlayingMarker;
			var gameBoardTileCount = gameBoard.GetTotalTileCount();
			var deltaStepCount = stepCount - playingMarkerLocalState.StepCount;
			var startTileIndex = playingMarkerLocalState.StepCount % gameBoardTileCount;
			var tileIndexSequence = GetIndexSequence(startTileIndex, deltaStepCount, gameBoardTileCount);

			Animate(playingMarker, tileIndexSequence, gameBoard, tileStepDuration);

			//return destinationTileIndex;

			// TODO: Create new local state.
			playingMarkerLocalState.StepCount = stepCount;
		}

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

		private void Animate(GameObject playingMarker, List<int> tileIndexSequence, GameBoard gameBoard, float tileStepDuration)
		{
			// TODO: Assert playing marker existence.
			Sequence sequence = DOTween.Sequence();

			foreach (var tileIndex in tileIndexSequence)
			{
				var tileWorldPosition = gameBoard.GetWorldPosition(tileIndex);
				Tweener tweener = playingMarker.transform.DOMove(tileWorldPosition, tileStepDuration);
				sequence.Append(tweener);
			}
		}
	}
}
