using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SaveZoneMode : Mode
{
    [SerializeField] private SaveZone saveZonePrefab;
    private Master _master;
    private SaveZone _saveZone1;
    private SaveZone _saveZone2;

    private void Awake()
    {
        _master = FindObjectOfType<Master>();
        _saveZone1 = Instantiate(saveZonePrefab);
        _saveZone2 = Instantiate(saveZonePrefab);
    }

    public override IEnumerator Run()
    {
        var cutters = FindObjectsOfType<RopeCutter>();

        restartFirst:
        var first = new Vector2(Random.Range(Border.Left.GetOffset(), Border.Right.GetOffset()),
            Random.Range(Border.Top.GetOffset(), Border.Bottom.GetOffset()));
        foreach (var item in cutters)
        {
            if (Vector2.Distance(item.transform.position, first) < _saveZone1.RadiusOffset)
            {
                goto restartFirst;
            }
        }

        restartSecond:
        var second = new Vector2(Random.Range(Border.Left.GetOffset(), Border.Right.GetOffset()),
            Random.Range(Border.Top.GetOffset(), Border.Bottom.GetOffset()));
        foreach (var item in cutters)
        {
            if (Vector2.Distance(first, second) < _saveZone1.RadiusOffset ||
                Vector2.Distance(item.transform.position, first) < _saveZone1.RadiusOffset ||
                FindDistanceToSegment(item.transform.position, first, second) < item.RadiusOffset)
            {
                goto restartSecond;
            }
        }

        _saveZone1.transform.position = first;
        _saveZone2.transform.position = second;
        _saveZone1.gameObject.SetActive(true);
        _saveZone2.gameObject.SetActive(true);

        yield return StartCoroutine(_master.timer.SetTimer(Convert.ToInt32(duration)));

        var player1Loose = !InZone(_saveZone1, _master.player1) && !InZone(_saveZone2, _master.player1);
        var player2Loose = !InZone(_saveZone1, _master.player2) && !InZone(_saveZone2, _master.player2);
        if (player1Loose || player2Loose)
        {
            _master.matchEnd.EndMatch(MatchEnd.Result.Defeat);
            yield break;
        }

        _saveZone1.gameObject.SetActive(false);
        _saveZone2.gameObject.SetActive(false);
    }

    private static bool InZone(SaveZone zone, Player player) =>
        player.Radius + Vector2.Distance(zone.transform.position, player.transform.position) < zone.Radius;
    
    private static double FindDistanceToSegment(Vector2 pt, Vector2 p1, Vector2 p2)
    {
        Vector2 closest;
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        if ((dx == 0) && (dy == 0))
        {
            closest = p1;
            dx = pt.x - p1.x;
            dy = pt.y - p1.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
 
        float t = ((pt.x - p1.x) * dx + (pt.y - p1.y) * dy) /
                  (dx * dx + dy * dy);
        
        if (t < 0)
        {
            closest = new Vector2(p1.x, p1.y);
            dx = pt.x - p1.x;
            dy = pt.y - p1.y;
        }
        else if (t > 1)
        {
            closest = new Vector2(p2.x, p2.y);
            dx = pt.x - p2.x;
            dy = pt.y - p2.y;
        }
        else
        {
            closest = new Vector2(p1.x + t * dx, p1.y + t * dy);
            dx = pt.x - closest.x;
            dy = pt.y - closest.y;
        }
 
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
