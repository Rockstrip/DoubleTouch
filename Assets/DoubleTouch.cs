using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoubleTouch : MonoBehaviour
{
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    
    private EdgeCollider2D _edgeCollider2D;
    private LineRenderer _lineRenderer;
    private Camera _camera;
    private Master _master;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _camera = Camera.main;
        _edgeCollider2D = GetComponent<EdgeCollider2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _master = FindObjectOfType<Master>();
    }

    private void Update()
    {
        if (Input.touchCount >= 2)
        {
            var firstTouch = _camera.ScreenToWorldPoint(Input.touches[0].position);
            var secondTouch = _camera.ScreenToWorldPoint(Input.touches[1].position);
            firstTouch.z = 0;
            secondTouch.z = 0;

            player1.transform.position = firstTouch;
            player2.transform.position = secondTouch;
        }
    }

    private void LateUpdate()
    {
        var points2D = new List<Vector2>() {player1.transform.position, player2.transform.position};
        _edgeCollider2D.SetPoints(points2D);
        _lineRenderer.SetPositions(points2D.Select(x => (Vector3) x).ToArray());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _master.matchEnd.EndMatch(MatchEnd.Result.Defeat);
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
    }
}