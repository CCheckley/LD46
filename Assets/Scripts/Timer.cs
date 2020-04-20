using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeLimit = 90.0f;
        
    float timeLeft = 0;

    public UnityEvent timesUp;

    TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();

        timeLeft = timeLimit;
        tmpText.text = $"Time: {timeLeft.ToString("F2")}";
    }

    public void Reset()
    {
        timeLeft = timeLimit;
        tmpText.text = $"Time: {timeLeft.ToString("F2")}";
    }

    private void Update()
    {
        if(GameManager.hasEnded == true) { return; }

        if (timeLeft <= 0.0f)
        {
            timesUp?.Invoke();
            return;
        }

        timeLeft -= Time.deltaTime;
        tmpText.text = $"Time: {timeLeft.ToString("F2")}";
    }
}
