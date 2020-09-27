using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Text _announcementText;

	private void Awake()
	{
		CheckComponents();
	}

	private void CheckComponents()
	{
		if (_announcementText == null)
		{
			Debug.LogError("[UIController] Cannot find AnnouncementText!");
		}
	}

	public void ShowAnnouncement(string text, float secondsToShow)
	{
		StartCoroutine(ShowAnnouncementForSomeTime(text, secondsToShow));
	}

	public void ShowGameOverText()
	{
		_announcementText.text = "THE STORE HAS FALLEN";
		_announcementText.gameObject.SetActive(true);
	}

	private IEnumerator ShowAnnouncementForSomeTime(string text, float secondsToShow)
	{
		_announcementText.text = text;
		_announcementText.gameObject.SetActive(true);

		yield return new  WaitForSeconds(secondsToShow);

		_announcementText.gameObject.SetActive(false);
	}
}
