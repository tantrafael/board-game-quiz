using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class UserInterfacePresentation
	{
		private UserInterfacePanel userInterfacePanel;

		public void Initialize(UserInterfacePanel userInterfacePanel, GameState gameState)
		{
			this.userInterfacePanel = userInterfacePanel;

			InitializeUserInterfacePanel(gameState);
		}

		private void InitializeUserInterfacePanel(GameState gameState)
		{
			Assert.IsNotNull(userInterfacePanel);

			userInterfacePanel.Initialize();
		}

		public void Update(GameState gameState)
		{
			UpdateUserInterfacePanel(gameState);
		}

		private void UpdateUserInterfacePanel(GameState gameState)
		{
			var activeQuizID = gameState.ActiveQuiz;

			userInterfacePanel.UpdateUserInterface(activeQuizID);
		}
	}
}
