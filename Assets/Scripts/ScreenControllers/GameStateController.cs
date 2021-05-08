using UnityEngine;

public class GameStateController : MonoBehaviour
{
	public static GameStateController instance;

	[SerializeField] private IntroScreenController _introContainer;
	[SerializeField] private RestScreenController _restContainer;
	[SerializeField] private GameObject _gameplayUIContainer;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		ChangeGameState(GameState.INTRO);
	}

	public void ChangeGameState(GameState newState)
	{
		//TODO: Implement each case!
		switch(newState)
		{
			case GameState.INTRO:
				//FROM: first start, DEFEAT reset
				// Show the INTRO screen
				// Title overlayed on the REST screen
				// Start button leads to REST
				// OPTIONS button

				_introContainer.gameObject.SetActive(true);
				_restContainer.gameObject.SetActive(true);
				_gameplayUIContainer.SetActive(false);
			break;
			case GameState.GAMEPLAY:
				// FROM: REST
				// Ends in VICTORY or DEFEAT

				_introContainer.gameObject.SetActive(false);
				_restContainer.gameObject.SetActive(false);
				_gameplayUIContainer.SetActive(true);
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
			case GameState.REST:
				// FROM: INTRO, VICTORY, MAP, WEAPONS
				// Show the REST screen
				// Show survivors around a campfire
				// can trigger Dialogue with survivors
				// MAP button
				// WEAPONS button
				// GAMEPLAY start button

				_introContainer.gameObject.SetActive(false);
				_restContainer.gameObject.SetActive(true);
				_gameplayUIContainer.SetActive(false);
			break;
			case GameState.MAP:
				// FROM: REST
				// Show the MAP screen
				// Drag and drop different survivors to defensive positions
				// Returns to REST
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
