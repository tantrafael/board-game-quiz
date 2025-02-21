using UnityEngine.Events;

namespace BoardGameQuiz
{
	public static class EventManager
	{
		public static event UnityAction<int> AnswerSelected;

		public static void OnAnswerSelected(int answerIndex)
		{
			AnswerSelected?.Invoke(answerIndex);
		}
	}
}
