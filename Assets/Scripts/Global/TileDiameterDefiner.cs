using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDiameterDefiner : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    
    
    public static float TileDiameter { get; private set; }

    
    private void Awake()
    {
        TileDiameter = _tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }
}
