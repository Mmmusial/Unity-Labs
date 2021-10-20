using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticValues
{
    private static int frytki=0;
    public static int Frytki { get => frytki; set { frytki = value; updateResources(); } }
    private static int wInoBiale=0;
    public static int WInoBiale { get => wInoBiale; set { wInoBiale = value; updateResources(); } }
    private static int winoCzerwone = 0;
    public static int WinoCzerwone { get => winoCzerwone; set { winoCzerwone = value; updateResources(); } }
    private static int lapuszki = 0;
    public static int Lapuszki { get => lapuszki; set { lapuszki = value; updateResources(); } }
    private static int hajs = 0;
    public static int Hajs { get => hajs; set { hajs = value; updateResources(); } }

    public static event Action updateEvent;
    private static void updateResources()
    {
        updateEvent?.Invoke();
    }

}
