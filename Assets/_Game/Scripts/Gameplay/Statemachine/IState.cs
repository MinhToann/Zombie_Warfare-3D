using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> where T : MonoBehaviour
{
    public void OnEnter(T t);
    public void OnExecute(T t);
    public void OnExit(T t);
}
