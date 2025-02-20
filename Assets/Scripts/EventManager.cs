using UnityEngine.Events;

namespace BoardGameQuiz
{
	public static class EventManager
	{
		public static event UnityAction PlayClicked;

		public static void OnPlayClicked()
		{
			PlayClicked?.Invoke();
		}
	}
}
