using TMPro;
using UnityEngine;

namespace BoardGameQuiz
{
	public class QuizPresenter : MonoBehaviour
	{
		public TMP_Text questionField;

		private void Awake()
		{
			//GetComponent<QuizPresenter>().enabled = false;
			GetComponent<Canvas>().enabled = false;
		}

		public void DisplayQuiz(Quiz quizData)
		{
			//enabled = true;
			//GetComponent<QuizPresenter>().enabled = true;
			GetComponent<Canvas>().enabled = true;

			questionField.text = quizData.Question;
		}
	}
}
