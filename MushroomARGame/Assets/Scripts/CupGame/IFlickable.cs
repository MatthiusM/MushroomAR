using System;
using System.Collections;
using UnityEngine;

public interface IFlickable
{
    void Flick(Action onComplete);
}