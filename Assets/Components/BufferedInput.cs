public class BufferedInput
{
    public Timer buffer;

    private float inputValue = 0;

    public BufferedInput(Timer buffer)
    {
        this.buffer = buffer;
    }

    public void SetValue(float value)
    {
        inputValue = value;
    }

    public bool Pressed()
    { 
        return buffer.running; 
    }

    public bool Held()
    { 
        return inputValue == 1; 
    }

    public void Clear()
    { 
        buffer.Clear(); 
    }

    public void Reset()
    { 
        buffer.Reset(); 
    }
 }