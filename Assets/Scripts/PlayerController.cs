using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace BoardGameQuiz
{
	public class PlayerController : MonoBehaviour
	{
		public GameBoard gameBoard;
		public GameObject marker;
		public float tileStepDuration;

		private GameObject markerInstance;
		private int tileIndex;

		public void Initialize()
		{
			var startPosition = gameBoard.GetWorldPosition(tileIndex);
			markerInstance = Instantiate(marker, startPosition, Quaternion.identity);
		}

		public int Move(int stepCount)
		{
			var totalTileCount = gameBoard.GetTotalTileCount();
			var tileIndexSequence = GetIndexSequence(tileIndex, stepCount, totalTileCount);
			//var destinationTileIndex = tileIndexSequence[^1];
			var destinationTileIndex = tileIndexSequence.Last();

			Animate(markerInstance, gameBoard, tileIndexSequence, 0.5f);

			tileIndex = destinationTileIndex;

			return destinationTileIndex;
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

		private void Animate(GameObject markerInstance, GameBoard gameBoard, List<int> tileIndexSequence, float stepDuration)
		{
			// TODO: Assert marker instance existence.
			Sequence sequence = DOTween.Sequence();

			foreach (var tileIndex in tileIndexSequence)
			{
				var tileWorldPosition = gameBoard.GetWorldPosition(tileIndex);
				Tweener tweener = markerInstance.transform.DOMove(tileWorldPosition, stepDuration);
				sequence.Append(tweener);
			}
		}
	}
}
