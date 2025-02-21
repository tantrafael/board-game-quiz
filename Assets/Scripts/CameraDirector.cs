using System;
using UnityEngine;

namespace BoardGameQuiz
{
	public class CameraDirector : MonoBehaviour
	{
		private PlayingPieceMover playingPieceMover;
		private GameObject activePlayingPiece;
		private Vector3 offset = new Vector3( 1.0f, Mathf.Sqrt(2), -1.0f ) * 2;

		public void Initialize(PlayingPieceMover playingPieceMover)
		{
			this.playingPieceMover = playingPieceMover;

			//var activePlayingPiece = playingPieceMover.GetActivePlayingPiece();
			activePlayingPiece = playingPieceMover.GetActivePlayingPiece();

			//offset = activePlayingPiece.transform.position - activePlayingPiece.transform.position;

			//transform.LookAt(activePlayingPiece.transform);
		}

		private void Update()
		{
			//transform.LookAt(activePlayingPiece.transform);
			transform.position = activePlayingPiece.transform.position + offset;
		}
	}
}
