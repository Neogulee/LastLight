using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public interface IPauseController
{
    public void pause();
    public void unpause();
}


public class PauseController: IPauseController
{
    Object pause_lock = new Object();
    int pause_cnt = 0;
    public void pause()
    {
        lock (pause_lock)
        {
            pause_cnt++;
            Time.timeScale = 0.0f;
        }
    }

    public void unpause()
    {
        lock (pause_lock)
        {
            pause_cnt = Mathf.Max(0, pause_cnt - 1);
            pause_cnt = 0; // 
            if (pause_cnt == 0)
                Time.timeScale = 1.0f;
        }
    }
}