using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class PlayingPieceMover : MonoBehaviour
	{
		//public GameBoard gameBoard;
		public GameObject playingPieceAsset;
		public float tileStepDuration;

		private GameBoard gameBoard;
		private Dictionary<string, PlayingPiecePresentationState> playingPieceTable = new();

		public void Initialize(GameBoard gameBoard)
		{
			this.gameBoard = gameBoard;
		}

		public void AddPlayingPiece(string playerID, PlayerState playerState)
		{
			Assert.IsNotNull(gameBoard);
			Assert.IsNotNull(playingPieceTable);

			var playerTileIndex = playerState.StepCount % gameBoard.GetTotalTileCount();
			var startPosition = gameBoard.GetWorldPosition(playerTileIndex);
			var playingPiece = Instantiate(playingPieceAsset, startPosition, Quaternion.identity);

			var playingPiecePresentationState = new PlayingPiecePresentationState
			{
				StepCount = playerState.StepCount,
				PlayingPiece = playingPiece
			};

			playingPieceTable.Add(playerID, playingPiecePresentationState);
		}

		public void UpdatePlayingPieces(Dictionary<string, PlayerState> playerStateTable)
		{
			foreach (var (playerID, playerState) in playerStateTable)
			{
				UpdatePlayingPiece(playerID, playerState);
			}
		}

		private void UpdatePlayingPiece(string playerID, PlayerState playerState)
		{
			var isCorrectlyMoved = IsCorrectlyMoved(playerID, playerState);

			if (isCorrectlyMoved)
			{
				return;
			}

			MovePlayingPiece(playerID, playerState);
		}

		private bool IsCorrectlyMoved(string playerID, PlayerState playerState)
		{
			var playingPiecePresentationState = playingPieceTable[playerID];
			var deltaStepCount = playerState.StepCount - playingPiecePresentationState.StepCount;
			var isUpToDate = (deltaStepCount == 0);

			return isUpToDate;
		}

		private void MovePlayingPiece(string playerID, PlayerState playerState)
		{
			Assert.IsNotNull(gameBoard);

			var playingPiecePresentationState = playingPieceTable[playerID];
			var playingPiece = playingPiecePresentationState.PlayingPiece;
			var deltaStepCount = playerState.StepCount - playingPiecePresentationState.StepCount;
			var gameBoardTileCount = gameBoard.GetTotalTileCount();
			var startTileIndex = playingPiecePresentationState.StepCount % gameBoardTileCount;
			var tileIndexSequence = GetIndexSequence(startTileIndex, deltaStepCount, gameBoardTileCount);

			Animate(playingPiece, tileIndexSequence, gameBoard, tileStepDuration);

			playingPiecePresentationState.StepCount = playerState.StepCount;
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
			Assert.IsNotNull(gameBoard);
			Assert.IsNotNull(playingPiece);

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
