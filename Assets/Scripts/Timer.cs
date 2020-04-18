using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeLimit = 90.0f;
    float timeLeft = 0;

    UnityEvent timesUp;

    private void Awake()
    {
        timeLeft = timeLimit;
    }

    public void Reset()
    {
        timeLeft = timeLimit;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0.0f)
        {
            timesUp?.Invoke();
        }
    }
}
