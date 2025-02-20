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
		//private void OnEnable()
		{
			document = GetComponent<UIDocument>();

			button = document.rootVisualElement.Q("TestButton") as Button;
			button.RegisterCallback<ClickEvent>(OnPlayButtonClicked);

			label = document.rootVisualElement.Q("Question") as Label;
		}

		private void OnDisable()
		{
			button.UnregisterCallback<ClickEvent>(OnPlayButtonClicked);
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

		private void OnPlayButtonClicked(ClickEvent evt)
		{
			EventManager.OnPlayClicked();
		}
	}
}
