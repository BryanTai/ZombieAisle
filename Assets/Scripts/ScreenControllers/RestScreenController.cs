
using UnityEngine;
using UnityEngine.UI;

public class RestScreenController : MonoBehaviour
{
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _mapButton;
	[SerializeField] private Button _weaponsButton;

	private void Start()
	{
		_startButton.onClick.AddListener(OnStartButtonClicked);
		_mapButton.onClick.AddListener(OnMapButtonClicked);
	}

	public void ToggleButtons(bool enabled)
	{
		_startButton.gameObject.SetActive(enabled);
		_mapButton.gameObject.SetActive(enabled);
		//_weaponsButton.gameObject.SetActive(enabled);
	}

	private void OnStartButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.GAMEPLAY);
	}

	private void OnMapButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.MAP);
	}
}
