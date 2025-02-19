using System.Collections.Generic;

namespace BoardGameQuiz
{
	public class GameState
	{
		public GameBoardLayout GameBoardLayout { get; set; }
		//public List<PlayerState> PlayerStates { get; set; }
		public Dictionary<string, PlayerState> PlayerStateTable { get; set; }
		public string PlayerInTurn { get; set; }
	}

	public class PlayerState
	{
		//public string ID { get; set; }
		//public int TileIndex { get; set; }
		public int StepCount { get; set; }
		public int Score { get; set; }
	}
}
