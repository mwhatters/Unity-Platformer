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

    public void UpdateBuffer()
    {
        if (Held())
        {
            ResetBuffer();
        }
    }

    public bool Pressed()
    { 
        return buffer.running; 
    }

    public bool Held()
    { 
        return inputValue == 1; 
    }

    public void ClearBuffer()
    { 
        buffer.Clear(); 
    }

    public void ResetBuffer()
    { 
        buffer.Reset(); 
    }
 }