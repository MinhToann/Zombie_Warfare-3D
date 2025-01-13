using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Character>
{
    public void OnEnter(Character c)
    {
        c.OnAttackEnter();
    }
    public void OnExecute(Character c)
    {
        c.OnAttackExecute();
    }
    public void OnExit(Character c)
    {

    }
}
