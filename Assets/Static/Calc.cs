using UnityEngine;

public static class Calc
{
    public static float Approach(float val, float target, float maxMove)
    {
        if (!(val > target))
        {
            return Mathf.Min(val + maxMove, target);
        }
        return Mathf.Max(val - maxMove, target);
    }
}
