using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticValues
{
    public static int Frytki { get =>Frytki; set { Frytki = value; updateResources(); } }
    public static int WInoBiale=0;
    public static int WinoCzerwone=0;
    public static int Lapuszki=0;
    public static int Hajs=0;

    public static event Action updateEvent;
    public static void updateResources()
    {
        updateEvent?.Invoke();
    }

}
