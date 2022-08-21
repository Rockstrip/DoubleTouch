using System;
using System.Collections;
using UnityEngine;

public abstract class Mode : MonoBehaviour
{
    [SerializeField] protected float duration;
    public string modeName;

    public abstract IEnumerator Run();
}
