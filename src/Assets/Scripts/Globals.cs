using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    static Globals()
    {
        PlayerOne = new PlayerState { Score = 0, NumberOfLives = 3 };

        PlayerTwo = new PlayerState { Score = 0, NumberOfLives = 3 };
    }

	public static bool TwoPlayer { get; set; }

    public static PlayerState PlayerOne { get; set; }

    public static PlayerState PlayerTwo { get; set; }
}
