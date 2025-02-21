using UnityEngine;

namespace BoardGameQuiz
{
	public class QuizPresenter : MonoBehaviour
	{
		void Awake()
		{
			Debug.Log("QuizPresenter::Awake()");
		}

		//public void DisplayQuiz(QuizData quizData)
		public void DisplayQuiz(Quiz quizData)
		{
			Debug.Log($"QuizPresenter::DisplayQuiz()");
			Debug.Log(quizData.Question);
		}
	}
}
