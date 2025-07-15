using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float messageSpacing = 0.5f;

    private Queue<string> messageQueue = new Queue<string>();
    private bool isShowingMessage = false;

    private void Awake()
    {
        messageText.alpha = 0f;
    }

    public void ShowMessage(string message)
    {
        messageQueue.Enqueue(message);

        if (!isShowingMessage)
        {
            StartCoroutine(ProcessMessageQueue());
        }
    }

    private IEnumerator ProcessMessageQueue()
    {
        isShowingMessage = true;

        while (messageQueue.Count > 0)
        {
            string currentMessage = messageQueue.Dequeue();
            messageText.text = currentMessage;

            float fadeInTime = 0.3f;
            float timer = 0f;
            while (timer < fadeInTime)
            {
                timer += Time.deltaTime;
                messageText.alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);
                yield return null;
            }
            messageText.alpha = 1f;

            yield return new WaitForSeconds(displayDuration);

            timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                messageText.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                yield return null;
            }
            messageText.alpha = 0f;

            yield return new WaitForSeconds(messageSpacing);
        }

        isShowingMessage = false;
    }

    private void OnDisable()
    {
        messageQueue.Clear();
        StopAllCoroutines();
        messageText.alpha = 0f;
        isShowingMessage = false;
    }

}
