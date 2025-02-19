using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class PlayingPieceLocalState
	{
		//public int TileIndex { get; set; }
		public int StepCount { get; set; }
		public GameObject PlayingPiece { get; set; }
	}

	public class PlayingPieceMover : MonoBehaviour
	{
		public GameBoard gameBoard;
		public GameObject playingPieceAsset;
		public float tileStepDuration;

		//private List<GameObject> playingMarkers = new();
		private Dictionary<string, PlayingPieceLocalState> playingPieceTable = new();

		//public void AddPlayingPiece(PlayerState playerState)
		public void AddPlayingPiece(string playerID, PlayerState playerState)
		{
			var playerTileIndex = playerState.StepCount % gameBoard.GetTotalTileCount();
			var startPosition = gameBoard.GetWorldPosition(playerTileIndex);
			var playingPiece = Instantiate(playingPieceAsset, startPosition, Quaternion.identity);

			//Assert.IsNotNull(playingMarkers);
			//playingMarkers.Add(playingMarker);

			var playingPieceLocalState = new PlayingPieceLocalState
			{
				StepCount = playerState.StepCount,
				PlayingPiece = playingPiece
			};

			Assert.IsNotNull(playingPieceTable);
			playingPieceTable.Add(playerID, playingPieceLocalState);
		}

		public void UpdatePlayingPieces(Dictionary<string, PlayerState> playerStateTable)
		{
			//foreach (var playerStateTableElement in playerStateTable)
			foreach (var (playerID, playerState) in playerStateTable)
			{
				//UpdatePlayingPiece(playerStateTableElement);
				UpdatePlayingPiece(playerID, playerState);
			}
		}

		//private void UpdatePlayingPiece(KeyValuePair<string, PlayerState> playerStateTableElement)
		private void UpdatePlayingPiece(string playerID, PlayerState playerState)
		{
			//var playerID = playerStateTableElement.Key;
			//var playerState = playerStateTableElement.Value;
			var playingPieceLocalState = playingPieceTable[playerID];
			var deltaStepCount = playerState.StepCount - playingPieceLocalState.StepCount;
			var isUpToDate = (deltaStepCount == 0);

			if (isUpToDate)
			{
				return;
			}

			var playingPiece = playingPieceLocalState.PlayingPiece;
			var gameBoardTileCount = gameBoard.GetTotalTileCount();
			var startTileIndex = playingPieceLocalState.StepCount % gameBoardTileCount;
			var tileIndexSequence = GetIndexSequence(startTileIndex, deltaStepCount, gameBoardTileCount);

			Animate(playingPiece, tileIndexSequence, gameBoard, tileStepDuration);

			//return destinationTileIndex;

			// TODO: Create new local state.
			playingPieceLocalState.StepCount = playerState.StepCount;
		}

		private void MovePlayingPiece(PlayingPieceLocalState playingPieceLocalState, PlayerState playerState)
		{
			var playingPiece = playingPieceLocalState.PlayingPiece;
			var gameBoardTileCount = gameBoard.GetTotalTileCount();
			var deltaStepCount = playerState.StepCount - playingPieceLocalState.StepCount;
			var startTileIndex = playingPieceLocalState.StepCount % gameBoardTileCount;
			var tileIndexSequence = GetIndexSequence(startTileIndex, deltaStepCount, gameBoardTileCount);

			Animate(playingPiece, tileIndexSequence, gameBoard, tileStepDuration);

			//return destinationTileIndex;

			// TODO: Create new local state.
			playingPieceLocalState.StepCount = playerState.StepCount;
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

		private void Animate(GameObject playingPiece, List<int> tileIndexSequence, GameBoard gameBoard, float tileStepDuration)
		{
			// TODO: Assert playing piece existence.
			Sequence sequence = DOTween.Sequence();

			foreach (var tileIndex in tileIndexSequence)
			{
				var tileWorldPosition = gameBoard.GetWorldPosition(tileIndex);
				Tweener tweener = playingPiece.transform.DOMove(tileWorldPosition, tileStepDuration);
				sequence.Append(tweener);
			}
		}
	}
}
