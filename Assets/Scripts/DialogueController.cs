using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{	
	public delegate void OnHideCallback();

	[SerializeField] private Animator _animator;
	[SerializeField] private Image _leftFullbodyImage;
	[SerializeField] private Image _rightFullbodyImage;

	[SerializeField] private Text _speakerText;
	[SerializeField] private Text _dialogueText;

	private OnHideCallback _onHideCallback;

	private void Start()
	{
		this.gameObject.SetActive(false);
	}

	public void ShowDialogue(string title, string dialogue)
	{
		this.gameObject.SetActive(true);
		_animator.SetTrigger("StartFadeIn");
		_speakerText.text = title;
		_dialogueText.text = dialogue;
	}

	public void HideDialogue(OnHideCallback callback = null)
	{
		_animator.SetTrigger("StartFadeOut");
		_onHideCallback = callback;
	}

#region Animation Events
	public void OnFadeOutComplete()
	{
		Debug.Log("[DialogueController] - Fade out COMPLETE!");
		this.gameObject.SetActive(false);
		if(_onHideCallback != null)
		{
			_onHideCallback.Invoke();
		}
	}
#endregion

}
