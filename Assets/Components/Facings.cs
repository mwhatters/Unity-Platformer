using UnityEngine;

public class Facings
{
    public static int Right = 1;
    public static int Left = -1;

    public int current;
     
    public Facings(int defaultFacing = 1)
    {
        this.current = defaultFacing;
    }

    public void Flip()
    {
        current *= -1;
    }
    public int Opposite()
    {
        return current * -1;
    }
}


