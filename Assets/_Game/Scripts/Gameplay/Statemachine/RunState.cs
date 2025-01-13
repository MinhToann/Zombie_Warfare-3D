using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState<Character>
{
    public void OnEnter(Character c)
    {
        c.OnRunEnter();
    }
    public void OnExecute(Character c)
    {
        c.OnRunExecute();
    }
    public void OnExit(Character c)
    {

    }
}
