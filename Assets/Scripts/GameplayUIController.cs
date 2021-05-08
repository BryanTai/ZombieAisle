using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
	[SerializeField] private Text _announcementText;
	[SerializeField] private Text _ammoCount;

	[Header("Interact Text Elements")]
	[SerializeField] private GameObject _interactContainer;
	[SerializeField] private Text _interactText;

#region Setup
	private void Awake()
	{
		CheckComponents();

		SetInteractTextActive(false);
		SetAnnouncementActive(false);
	}

	private void CheckComponents()
	{
		if (_announcementText == null)
		{
			Debug.LogError("[GameplayUIController] Cannot find AnnouncementText!");
		}
	}
#endregion

#region Announcement
	public void ShowAnnouncement(string text, float secondsToShow)
	{
		StartCoroutine(ShowAnnouncementForSomeTime(text, secondsToShow));
	}

	private void SetAnnouncementActive(bool isActive)
	{
		_announcementText.gameObject.SetActive(isActive);
		//TODO: Trigger UI Animations here?
	}

	public void ShowGameOverText()
	{
		_announcementText.text = "THE STORE HAS FALLEN";
		SetAnnouncementActive(true);
	}

	private IEnumerator ShowAnnouncementForSomeTime(string text, float secondsToShow)
	{
		_announcementText.text = text;
		SetAnnouncementActive(true);

		yield return new  WaitForSeconds(secondsToShow);

		SetAnnouncementActive(false);
	}
#endregion

#region Combat HUD
	public void SetAmmoCountText(string countText, Color textColor)
	{
		_ammoCount.text = countText;
		_ammoCount.color = textColor;
	}
#endregion

#region Interact
	public void SetInteractTextActive(bool isActive)
	{
		_interactContainer.SetActive(isActive);
	}

	public void SetInteractText(string newText)
	{
		_interactText.text = newText;
	}
#endregion
	
}
