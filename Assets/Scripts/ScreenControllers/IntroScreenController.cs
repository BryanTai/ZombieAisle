using UnityEngine;
using UnityEngine.UI;

public class IntroScreenController : MonoBehaviour
{
	[SerializeField] private Button _startButton;

	private void Start()
	{
		_startButton.onClick.AddListener(OnStartButtonClicked);
	}

	private void OnStartButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.REST);
	}
}
