using System;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class foodData : ScriptableObject
{
    public foodItems[] items;
}

[Serializable]
public struct foodItems
{
    public Food type;
    public GameObject prefab;
    public Sprite image;
}

[Serializable]
public enum Food
{
    None,
    Pizza,
    Sandwich,
    BreadSticks,
    Burrito,
    Muffin,
    Cake,
    Cookie
}
