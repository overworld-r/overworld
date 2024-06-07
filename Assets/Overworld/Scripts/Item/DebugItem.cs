using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItem : MonoBehaviour, IItem
{
    public string name => "DebugItem";
    public string description => "This is a debug item.";
    public int price => 100;
    
    public bool isStack => false;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
