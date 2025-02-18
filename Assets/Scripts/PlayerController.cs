using UnityEngine;

namespace BoardGameQuiz
{
	public class PlayerController : MonoBehaviour
	{
		public GameBoard gameBoard;
		public GameObject marker;

		private GameObject markerInstance;
		private int tileIndex;

		/*
		void Start()
		{
			var startPosition = gameBoard.GetStartPosition();
			var markerInstance = Instantiate(marker, startPosition, Quaternion.identity);
		}
		*/

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
			markerInstance.transform.position = destinationPosition;

			return destinationTileIndex;
		}
	}
}
