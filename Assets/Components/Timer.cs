using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time = 0f;
    private float init_time = 0f;

    public bool running = false;
    public bool stopped = false;


    void Awake() {
        time = 0f;
        running = false;
        stopped = true;
    }

    void FixedUpdate() {
        if (time > 0)
        {
            time = Mathf.Max(0, time - Time.deltaTime);
            if (time == 0) {
                running = false;
                stopped = true;
            }
        }
    }

    public void Reset() {
        time = init_time;
        running = true;
        stopped = false;
    }

    public void Clear() {
        time = 0f;
        running = false;
        stopped = true;
    }

    public static Timer New(GameObject obj, float time) {
        var timer = obj.AddComponent<Timer>();
        timer.init_time = time;
        return timer;
    }
}


