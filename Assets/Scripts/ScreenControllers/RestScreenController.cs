
using UnityEngine;
using UnityEngine.UI;


public enum RestState
{
	NONE,
	MAP,
	WEAPON,
	PLAN,
	CHAT
}

public class RestScreenController : MonoBehaviour
{
	private enum DayCycle
	{
		DAY,
		NIGHT,
	}

	[Header("Screen Controllers")]
	[SerializeField] private DialogueController _dialogueController;
	[SerializeField] private MapScreenController _mapContainer;
	[SerializeField] private WeaponsScreenController _weaponsContainer;
	[SerializeField] private PlanScreenController _planContainer;

	[Header("Buttons")]
	[SerializeField] private GameObject _buttonContainer;
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _mapButton;
	[SerializeField] private Button _weaponsButton;
	[SerializeField] private Button _chatButton;
	[SerializeField] private Button _planButton;

	[Header("Day Elements")]
	[SerializeField] private GameObject _dayTimeWindow;

	[Header("Night Elements")]
	[SerializeField] private GameObject _nightTimeWindow;
	[SerializeField] private GameObject _campFire;
	[SerializeField] private GameObject _nightFilter;

	private DayCycle _currentDayCycle;
	private RestState _currentRestState;

	private void Start()
	{
		_startButton.onClick.AddListener(OnStartButtonClicked);
		_mapButton.onClick.AddListener(OnMapButtonClicked);
		_planButton.onClick.AddListener(OnPlanButtonClicked);
		_weaponsButton.onClick.AddListener(OnWeaponButtonClicked);
		_chatButton.onClick.AddListener(OnChatButtonClicked);
		SetToDay();
	}

	public void SwitchRestState(RestState currentState)
	{
		_currentRestState = currentState;

		_mapContainer.gameObject.SetActive(currentState == RestState.MAP);
		_weaponsContainer.gameObject.SetActive(currentState == RestState.WEAPON);
		_planContainer.gameObject.SetActive(currentState == RestState.PLAN);
	}

	public void ToggleAllButtons(bool enabled)
	{
		_buttonContainer.SetActive(enabled);
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
		_currentDayCycle = currentCycle;

		bool isDay = currentCycle == DayCycle.DAY;
		bool isNight = currentCycle == DayCycle.NIGHT;

		_dayTimeWindow.SetActive(isDay);
		_nightTimeWindow.SetActive(isNight);

		_campFire.SetActive(isNight);
		_nightFilter.SetActive(isNight);

		//These buttons are active all the time
		_mapButton.gameObject.SetActive(true);
		_weaponsButton.gameObject.SetActive(true);
		_chatButton.gameObject.SetActive(true);

		_startButton.gameObject.SetActive(isNight);
		_planButton.gameObject.SetActive(isDay);

		SwitchRestState(RestState.NONE);
	}

#region Button Events
	private void OnStartButtonClicked()
	{
		GameStateController.instance.ChangeGameState(GameState.GAMEPLAY);
	}

	private void OnMapButtonClicked()
	{
		SwitchRestState(RestState.MAP);
	}

	private void OnWeaponButtonClicked()
	{
		SwitchRestState(RestState.WEAPON);
	}

	private void OnPlanButtonClicked()
	{
		SwitchRestState(RestState.PLAN);
	}

	private void OnChatButtonClicked()
	{
		_dialogueController.ShowDialogue("Survivor", "You got this, buddy");
	}

	#endregion
}
