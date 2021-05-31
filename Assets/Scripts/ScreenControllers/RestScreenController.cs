
using UnityEngine;
using UnityEngine.UI;

public class RestScreenController : MonoBehaviour
{
	private enum DayCycle
	{
		DAY,
		NIGHT,
	}

	[SerializeField] private DialogueController _dialogueController;

	[Header("Buttons")]
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _mapButton;
	[SerializeField] private Button _weaponsButton;
	[SerializeField] private Button _chatButton;
	[SerializeField] private Button _planButton;

	private DayCycle _currentDayCycle;

	private void Start()
	{
		_startButton.onClick.AddListener(OnStartButtonClicked);
		_mapButton.onClick.AddListener(OnMapButtonClicked);
		SetToDay();
	}

	public void ToggleAllButtons(bool enabled)
	{
		_startButton.gameObject.SetActive(enabled);
		_mapButton.gameObject.SetActive(enabled);
		_weaponsButton.gameObject.SetActive(enabled);
	}

	public void SetToDay()
	{
		SetToDayCycle(DayCycle.DAY);
	}

	public void SetToNight()
	{
		SetToDayCycle(DayCycle.NIGHT);
	}

	private void SetToDayCycle(DayCycle currentCycle)
	{
		//These buttons are active all the time
		_mapButton.gameObject.SetActive(true);
		_weaponsButton.gameObject.SetActive(true);
		_chatButton.gameObject.SetActive(true);

		_startButton.gameObject.SetActive(currentCycle == DayCycle.NIGHT);
		_planButton.gameObject.SetActive(currentCycle == DayCycle.DAY);
	}

#region Button Events
	private void OnStartButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.GAMEPLAY);
	}

	private void OnMapButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.MAP);
	}

	private void OnWeaponButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.WEAPONS);
	}

	private void OnPlanButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.PLAN);
	}

	private void OnChatButtonClicked()
	{
		_dialogueController.ShowDialogue("Survivor", "You got this, buddy");
	}


	#endregion
}
