using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace BoardGameQuiz
{
	public class QuizPresenter : MonoBehaviour
	{
		public TMP_Text questionField;
		public List<Button> answerButtons = new();

		private void Awake()
		{
			GetComponent<Canvas>().enabled = false;
		}

		public void DisplayQuiz(Quiz quizData)
		{
			GetComponent<Canvas>().enabled = true;

			questionField.text = quizData.Question;

			Assert.IsTrue(quizData.Answers.Count == answerButtons.Count);

			var answerCount = Math.Min(quizData.Answers.Count, answerButtons.Count);

			for (var index = 0; index < answerCount; index++)
			{
				var answer = quizData.Answers[index];
				var answerButton = answerButtons[index];
				var answerButtonTextField = answerButton.GetComponentInChildren<TextMeshProUGUI>();
				answerButtonTextField.text = answer.Text;

				var answerIndex = index;
				answerButton.onClick.AddListener(() => SelectAnswer(answerIndex));
			}
		}

		private void SelectAnswer(int answerIndex)
		{
			Debug.Log($"Selected answer index: {answerIndex}");
		}
	}
}
