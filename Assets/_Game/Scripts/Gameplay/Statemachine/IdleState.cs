using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Character>
{
    public void OnEnter(Character c)
    {
        c.OnIdleEnter();
    }
    public void OnExecute(Character c)
    {
        c.OnIdleExecute();
    }
    public void OnExit(Character c)
    {

    }
}
