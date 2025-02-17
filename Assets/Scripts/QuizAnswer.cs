using JetBrains.Annotations;

namespace BoardGameQuiz
{
	public class QuizAnswer
	{
		[CanBeNull] public string ImageID{ get; set; }
		[CanBeNull] public string Text{ get; set; }
	}
}
