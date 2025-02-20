using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BoardGameQuiz
{
	public class UserInterfacePanel : MonoBehaviour
	{
		private UIDocument document;
		private Button button;
		private Label label;

		public void Awake()
		{
			Debug.Log("TestUIToolkit");

			document = GetComponent<UIDocument>();

			button = document.rootVisualElement.Q("TestButton") as Button;
			button.RegisterCallback<ClickEvent>(TestButtonClick);

			label = document.rootVisualElement.Q("Question") as Label;
		}

		public void Initialize()
		{
		}

		public void UpdateUserInterface(string activeQuizID)
		{
			label.text = activeQuizID;
		}

		/*
		public void DisplayQuestion(string question)
		{
			label.text = question;
		}
		*/

		private void OnDisable()
		{
			button.UnregisterCallback<ClickEvent>(TestButtonClick);
		}

		private void TestButtonClick(ClickEvent evt)
		{
			Debug.Log("Test button");
			//DisplayQuestion("Question");
		}
	}
}
