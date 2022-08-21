using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorMode : Mode
{
    [SerializeField] private Meteor meteorPrefab;
    [SerializeField] private float period;
    private Master _master;

    private void Awake()
    {
        _master = GetComponent<Master>();
    }

    public override IEnumerator Run()
    {
        StartCoroutine(_master.timer.SetTimer(Convert.ToInt32(duration)));

        var durationTemp = duration;
        while (durationTemp > 0)
        {
            var meteor = Instantiate(meteorPrefab);
            meteor.transform.position = new Vector2(
                Random.Range(Border.Left.GetOffset(), Border.Right.GetOffset()),
                Border.Top.GetOffset() + 5f);

            yield return new WaitForSeconds(period);
            durationTemp -= period;
        }
    }
}
