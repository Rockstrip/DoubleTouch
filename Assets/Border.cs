using System;
using UnityEngine;

public enum Border
{
    Left,
    Right,
    Top,
    Bottom
}

public static class BorderEx
{
    public static float GetOffset(this Border border)
    {
        var camera = Camera.main;
        return border switch
        {
            Border.Left   => camera.ScreenToWorldPoint(Vector3.zero).x,
            Border.Right  => camera.ScreenToWorldPoint(Vector3.right * camera.pixelWidth).x,
            Border.Top    => camera.ScreenToWorldPoint(Vector3.up * camera.pixelHeight).y,
            Border.Bottom => camera.ScreenToWorldPoint(Vector3.zero).y,
            _             => throw new ArgumentOutOfRangeException(nameof(border), border, null)
        };
    }
}