using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICircle
{
    public float Radius { get; set; } = 0.25f;
    public float RadiusOffset { get; set; } = 0.25f;
}
