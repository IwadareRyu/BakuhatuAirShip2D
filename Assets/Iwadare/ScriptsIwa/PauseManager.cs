using System;
using UnityEngine;

public class PauseManager
{
    static bool _pause;

    public static event Action<bool> OnPauseResume;

    public static void PauseResume()
    {
        _pause = !_pause;
        OnPauseResume(_pause);
    }
}
