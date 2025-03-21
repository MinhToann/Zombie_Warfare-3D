using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState : IState<Character>
{
    public void OnEnter(Character c)
    {
        c.OnStandUpEnter();
    }
    public void OnExecute(Character c)
    {
        c.OnStandUpExecute();
    }
    public void OnExit(Character c)
    {

    }
}
