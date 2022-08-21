using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Meteor : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
