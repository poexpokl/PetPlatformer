using UnityEngine;

public static class PauseManager
{
    static private float lastTimeScale;
    static public void Pause()
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    static public void Unpause()
    {
        Time.timeScale = lastTimeScale;
    }
}
