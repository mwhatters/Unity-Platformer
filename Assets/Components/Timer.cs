using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time = 0f;
    private float initTime = 0f;

    public bool running = false;
    public bool stopped = true;

    void Awake()
    {
        time = 0f;
        Stop();
    }

    void FixedUpdate()
    {
        if (time > 0)
        {
            time = Mathf.Max(0, time - Time.deltaTime);
            if (time == 0) 
            {
                Stop();
            }
        }
    }

    public void Reset()
    {
        time = initTime;
        Start();
    }

    public void Clear()
    {
        time = 0f;
        Stop();
    }

    private void Stop() {
        running = false;
        stopped = true;
    }

    private void Start() {
        running = true;
        stopped = false;
    }

    public static Timer New(GameObject obj, float time)
    {
        var timer = obj.AddComponent<Timer>();
        timer.initTime = time;
        return timer;
    }
}


