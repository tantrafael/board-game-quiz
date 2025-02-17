using System.Collections.Generic;

namespace BoardGameQuiz
{
	public class Quiz
	{
		public string ID { get; set; }
		public string QuestionType { get; set; }
		public string Question { get; set; }
		public string CustomImageID { get; set; }
		public List<QuizAnswer> Answers { get; set; }
		public string CorrectAnswerIndex { get; set; }
	}
}
