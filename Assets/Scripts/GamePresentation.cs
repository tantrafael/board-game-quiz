using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class GamePresentation
	{
		private GameBoard gameBoard;
		private PlayingPieceMover playingPieceMover;
		private UserInterfacePanel userInterfacePanel;

		/*
		public void Initialize(GameBoard gameBoard, PlayingPieceMover playingPieceMover, GameState gameState)
		{
			this.gameBoard = gameBoard;
			this.playingPieceMover = playingPieceMover;

			InitializeGameBoard(gameState);
			InitializePlayingPieceMover(gameState);
		}
		*/
		public void Initialize(GameBoard gameBoard, PlayingPieceMover playingPieceMover, UserInterfacePanel userInterfacePanel, GameState gameState)
		{
			this.gameBoard = gameBoard;
			this.playingPieceMover = playingPieceMover;
			this.userInterfacePanel = userInterfacePanel;

			InitializeGameBoard(gameState);
			//InitializePlayingPieceMover(gameState);
			InitializePlayingPieceMover(gameBoard, gameState);
			InitializeUserInterfacePanel(gameState);
		}

		/*
		public void Update(GameState gameState)
		{
			UpdatePlayingPieceMover(gameState);
			//UpdateQuizPresenter(gameState);
		}
		*/
		public void Update(GameState gameState)
		{
			UpdatePlayingPieceMover(gameState);
			UpdateUserInterfacePanel(gameState);
		}

		private void InitializeGameBoard(GameState gameState)
		{
			Assert.IsNotNull(gameBoard);

			var gameBoardLayout = gameState.GameBoardLayout;

			gameBoard.Initialize(gameBoardLayout);
		}

		/*
		private void InitializePlayingPieceMover(GameState gameState)
		{
			Assert.IsNotNull(playingPieceMover);

			foreach (var (playerID, playerState) in gameState.PlayerStateTable)
			{
				playingPieceMover.AddPlayingPiece(playerID, playerState);
			}
		}
		*/
		private void InitializePlayingPieceMover(GameBoard gameBoard, GameState gameState)
		{
			Assert.IsNotNull(playingPieceMover);

			this.gameBoard = gameBoard;

			playingPieceMover.Initialize(gameBoard);

			foreach (var (playerID, playerState) in gameState.PlayerStateTable)
			{
				playingPieceMover.AddPlayingPiece(playerID, playerState);
			}
		}

		private void InitializeUserInterfacePanel(GameState gameState)
		{
			Assert.IsNotNull(userInterfacePanel);

			userInterfacePanel.Initialize();
		}

		private void UpdatePlayingPieceMover(GameState gameState)
		{
			Assert.IsNotNull(playingPieceMover);

			var playerStateTable = gameState.PlayerStateTable;

			playingPieceMover.UpdatePlayingPieces(playerStateTable);
		}

		private void UpdateUserInterfacePanel(GameState gameState)
		{
			var activeQuizID = gameState.ActiveQuiz;

			userInterfacePanel.UpdateUserInterface(activeQuizID);
		}
	}
}
