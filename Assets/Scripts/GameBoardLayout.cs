using System.Collections.Generic;
using UnityEngine;

namespace BoardGameQuiz
{
	public class GameBoardLayout
	{
		public string ID { get; set; }
		public List<Vector2Int> TilePositions{ get; set; }
		public List<QuizPlacement> QuizLayout{ get; set; }
	}
}
