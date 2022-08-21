using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RopeCutter : MonoBehaviour, ICircle
{
    public float Radius { get; set; } = 0.5f;
    public float RadiusOffset { get; set; } = 0.8f;
}