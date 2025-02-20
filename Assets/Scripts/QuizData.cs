using System.Collections.Generic;

namespace BoardGameQuiz
{
	public class QuizData
	{
		public string ID { get; set; }
		public int QuestionType { get; set; }
		public string Question { get; set; }
		public string CustomImageID { get; set; }
		public List<QuizAnswerData> Answers { get; set; }
		public int CorrectAnswerIndex { get; set; }
	}
}
