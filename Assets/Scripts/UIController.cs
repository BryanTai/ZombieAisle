using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text AnnouncementText;

    private void Awake()
    {
        CheckComponents();
    }

    private void CheckComponents()
    {
        if (AnnouncementText == null)
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
        AnnouncementText.text = "THE STORE HAS FALLEN";
        AnnouncementText.gameObject.SetActive(true);
    }

    private IEnumerator ShowAnnouncementForSomeTime(string text, float secondsToShow)
    {
        AnnouncementText.text = text;
        AnnouncementText.gameObject.SetActive(true);

        yield return new  WaitForSeconds(secondsToShow);

        AnnouncementText.gameObject.SetActive(false);
    }
}
