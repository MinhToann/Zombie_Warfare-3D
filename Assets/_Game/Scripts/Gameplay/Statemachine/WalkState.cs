using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState<Character>
{
    public void OnEnter(Character c)
    {
        c.OnWalkEnter();
    }
    public void OnExecute(Character c)
    {
        c.OnWalkExecute();
    }
    public void OnExit(Character c)
    {

    }
}
