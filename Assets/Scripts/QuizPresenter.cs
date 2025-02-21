using UnityEngine;

namespace BoardGameQuiz
{
	public class QuizPresenter : MonoBehaviour
	{
		public GameObject questionField;

		public void DisplayQuiz(Quiz quizData)
		{
			Debug.Log($"QuizPresenter::DisplayQuiz()");
			Debug.Log(quizData.Question);

			//enabled = true;
		}
	}
}
