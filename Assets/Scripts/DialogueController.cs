using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{	
	[SerializeField] private Animator _animator;
	[SerializeField] private Image _leftFullbodyImage;
	[SerializeField] private Image _rightFullbodyImage;

	[SerializeField] private Text _speakerText;
	[SerializeField] private Text _dialogueText;

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

	public void HideDialogue() //TODO: Add Callback function parameter!!
	{
		_animator.SetTrigger("StartFadeOut");
	}

#region Animation Events
	public void OnFadeOutComplete()
	{
		this.gameObject.SetActive(false);
	}
#endregion

}
