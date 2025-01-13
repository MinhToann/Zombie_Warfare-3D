using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState<Character>
{
    public void OnEnter(Character c)
    {
        c.OnDeathEnter();
    }
    public void OnExecute(Character c)
    {
        c.OnDeathExecute();
    }
    public void OnExit(Character c)
    {

    }
}
