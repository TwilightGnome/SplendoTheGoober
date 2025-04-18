using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdManager : MonoBehaviour 
{
    public Canvas adCanvas;            // Reference to the Canvas component
    public Image adImage;             // UI Image for showing the ad
    public Button closeButton;        // Button to close the ad
    public Sprite[] adSprites;        // Your ad images
    public float minAdInterval = 10f;
    public float maxAdInterval = 20f;

    private bool isAdShowing = false;

    void Start()
    {
        adCanvas.gameObject.SetActive(false); // Make sure it starts off
        StartCoroutine(AdRoutine());
    }

    IEnumerator AdRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minAdInterval, maxAdInterval));
            ShowAd();
            yield return new WaitUntil(() => !isAdShowing);
        }
    }

    public void ShowAd()
    {
        int index = Random.Range(0, adSprites.Length);
        adImage.sprite = adSprites[index];

        adCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isAdShowing = true;

        closeButton.interactable = false;
        StartCoroutine(EnableCloseButtonAfterDelay(3f));
    }

    IEnumerator EnableCloseButtonAfterDelay(float delay)
    {
        float elapsed = 0f;
        while (elapsed < delay)
        {
            yield return null;
            elapsed += Time.unscaledDeltaTime;
        }
        closeButton.interactable = true;
    }

    public void CloseAd()
    {
        adCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isAdShowing = false;
    }
}