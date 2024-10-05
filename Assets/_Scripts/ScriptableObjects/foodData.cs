using System;
using UnityEngine;

[CreateAssetMenu]
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
    None = -1,
    Pizza,
    Sandwich,
    BreadSticks,
    Burrito,
    Muffin,
    Cake,
    Cookie
}
