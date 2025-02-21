using System;
using UnityEngine;

namespace BoardGameQuiz
{
	public class CameraDirector : MonoBehaviour
	{
		private PlayingPieceMover playingPieceMover;
		private GameObject activePlayingPiece;

		// TODO: Get offset from settings.
		private Vector3 offset = new Vector3( 1.0f, Mathf.Sqrt(2), -1.0f ) * 2;

		public void Initialize(PlayingPieceMover playingPieceMover)
		{
			this.playingPieceMover = playingPieceMover;
			activePlayingPiece = playingPieceMover.GetActivePlayingPiece();
		}

		private void Update()
		{
			transform.position = activePlayingPiece.transform.position + offset;
		}
	}
}
