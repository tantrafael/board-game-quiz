using System.Collections.Generic;
using UnityEngine;

namespace Board
{
	public enum QuizType
	{
		Question,
		Flag
	}

	public class BoardLayout
	{
		public string ID { get; set; }
		public List<Vector2Int> TilePositions{ get; set; }
		public List<Quiz> Quizzes{ get; set; }
	}

	public class Quiz
	{
		public string ID{ get; set; }
		//public string Type{ get; set; }
		public QuizType Type{ get; set; }
		public List<int> TileIndexes{ get; set; }
	}
}
