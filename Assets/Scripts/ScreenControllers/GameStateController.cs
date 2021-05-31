using UnityEngine;

public class GameStateController : MonoBehaviour
{
	public static GameStateController instance;

	[Header("UI Controllers")]
	[SerializeField] private IntroScreenController _introContainer;
	[SerializeField] private RestScreenController _restContainer;
	[SerializeField] private MapScreenController _mapContainer;
	[SerializeField] private WeaponsScreenController _weaponsContainer;
	[SerializeField] private PlanScreenController _planContainer;

	[SerializeField] private GameObject _gameplayUIContainer;
	[SerializeField] private GameObject _victoryContainer;
	[SerializeField] private GameObject _defeatContainer;

	[Header("Gameplay")]
	[SerializeField] private GameplayController _gameplayController;
	[SerializeField] private PlayerController _playerController;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		ChangeGameState(GameState.INTRO);
	}

	private void EnableGameScreen(GameState newState)
	{
		bool shouldShowRestScreen = newState == GameState.INTRO || newState == GameState.RESTDAY || newState == GameState.RESTNIGHT 
		|| newState == GameState.MAP || newState == GameState.WEAPONS || newState == GameState.PLAN;

		_introContainer.gameObject.SetActive(newState == GameState.INTRO);
		_restContainer.gameObject.SetActive(shouldShowRestScreen);
		_mapContainer.gameObject.SetActive(newState == GameState.MAP);
		_weaponsContainer.gameObject.SetActive(newState == GameState.WEAPONS);
		_planContainer.gameObject.SetActive(newState == GameState.PLAN);

		_gameplayUIContainer.SetActive(newState == GameState.GAMEPLAY);
		_victoryContainer.SetActive(newState == GameState.VICTORY);
		_defeatContainer.SetActive(newState == GameState.DEFEAT);
	}

	public void ChangeGameState(GameState newState)
	{
		EnableGameScreen(newState);

		//TODO: Implement each case!
		switch(newState)
		{
			case GameState.INTRO:
				//FROM: first start, DEFEAT reset
				// Show the INTRO screen
				// Title overlayed on the REST screen
				// Start button leads to REST
				// OPTIONS button

				_restContainer.ToggleAllButtons(false);
			break;
			case GameState.GAMEPLAY:
				// FROM: REST
				// Ends in VICTORY or DEFEAT

				_gameplayController.Initialize();
			break;
			case GameState.VICTORY:
				// FROM: GAMEPLAY
				// Dialogue event after current night is completed
				// Leads to REST
			break;
			case GameState.DEFEAT:
				// FROM: GAMEPLAY
				// Automatically shows if the store is overrun and the game is lost
				// Restart button leads to INTRO
				// TODO: Lead back to REST once Saves work
			break;
			case GameState.RESTDAY:
				// FROM: INTRO, VICTORY, MAP, WEAPONS
				// Show the REST screen
				// Show survivors around a campfire
				// can trigger Dialogue with survivors
				// MAP button
				// WEAPONS button
				// GAMEPLAY start button

				_restContainer.ToggleAllButtons(true);
			break;
			case GameState.RESTNIGHT:
				// FROM: PLAN

				_restContainer.SetToNight();

			break;
			case GameState.MAP:
				// FROM: REST
				// Show the MAP screen
				// Drag and drop different survivors to defensive positions
				// Returns to REST

				_restContainer.ToggleAllButtons(false);
			break;
			case GameState.WEAPONS:
				// FROM: REST
				// Show the WEAPONS screen
				// Show the different weapons that the player can use
				// Returns to REST
			break;
			case GameState.OPTIONS:
				// FROM: INTRO (for now), GAMEPLAY?
				// Allows the player to fiddle with options
				// Returns to whatever screen opened it

			break;
			default:
				Debug.LogError($"[GameStateController] - Trying to start unhandled state! {newState}");
			break;
		}
	}
}
