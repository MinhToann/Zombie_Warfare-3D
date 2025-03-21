using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBackState : IState<Character>
{
    public void OnEnter(Character c)
    {
        c.OnFlyingBackDeathEnter();
    }
    public void OnExecute(Character c)
    {
        c.OnFlyingBackDeathExecute();
    }
    public void OnExit(Character c)
    {

    }
}
