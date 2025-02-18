using UnityEngine;
using DG.Tweening;

namespace BoardGameQuiz
{
	public class PlayerController : MonoBehaviour
	{
		public GameBoard gameBoard;
		public GameObject marker;

		private GameObject markerInstance;
		private int tileIndex;

		public void Initialize()
		{
			//var startPosition = gameBoard.GetStartPosition();
			var startPosition = gameBoard.GetWorldPosition(tileIndex);
			markerInstance = Instantiate(marker, startPosition, Quaternion.identity);
			tileIndex = Move(6);
		}

		private int Move(int tileCount)
		{
			var destinationTileIndex = tileIndex + tileCount;
			var destinationPosition = gameBoard.GetWorldPosition(destinationTileIndex);

			// TODO: Assert marker instance existence.
			//markerInstance.transform.position = destinationPosition;
			DOTween.To(() => markerInstance.transform.position, x => markerInstance.transform.position = x, destinationPosition, 4.0f);
			//markerInstance.transform.DOMove(destinationPosition, 4.0f);

			return destinationTileIndex;
		}
	}
}
