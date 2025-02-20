using System.Collections.Generic;

namespace BoardGameQuiz
{
	public class GameState
	{
		public int ID { get; set; }
		public GameBoardLayout GameBoardLayout { get; set; }
		//public List<PlayerState> PlayerStates { get; set; }
		public Dictionary<string, PlayerState> PlayerStateTable { get; set; }
		//public string PlayerInTurn { get; set; }
		//public string ActivePlayer { get; set; }
		public string ActiveQuiz { get; set; }
	}
}
