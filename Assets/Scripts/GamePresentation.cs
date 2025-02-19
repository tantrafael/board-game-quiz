namespace BoardGameQuiz
{
	public class GamePresentation
	{
		private GameBoard gameBoard;
		private PlayingPieceMover playingPieceMover;

		/*
		public void Initialize(GameBoard gameBoard, PlayingPieceMover playingPieceMover)
		{
			this.gameBoard = gameBoard;
			this.playingPieceMover = playingPieceMover;
		}
		*/
		public void Initialize(GameBoard gameBoard, PlayingPieceMover playingPieceMover, GameState gameState)
		{
			this.gameBoard = gameBoard;
			this.playingPieceMover = playingPieceMover;

			InitializeGameBoard(gameState);
			InitializePlayingPieceMover(gameState);
		}

		public void Update(GameState gameState)
		{
			UpdatePlayingPieceMover(gameState);
		}

		private void InitializeGameBoard(GameState gameState)
		{
			var gameBoardLayout = gameState.GameBoardLayout;

			gameBoard.Initialize(gameBoardLayout);
		}

		private void InitializePlayingPieceMover(GameState gameState)
		{
			foreach (var (playerID, playerState) in gameState.PlayerStateTable)
			{
				playingPieceMover.AddPlayingPiece(playerID, playerState);
			}
		}

		private void UpdatePlayingPieceMover(GameState gameState)
		{
			var playerStateTable = gameState.PlayerStateTable;

			playingPieceMover.UpdatePlayingPieces(playerStateTable);
		}
	}
}
