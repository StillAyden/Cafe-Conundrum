using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class foodData : ScriptableObject
{
    public item[] items;
}

[Serializable]
public struct item
{
    public Food type;
    public GameObject prefab;
    public Sprite image;
}

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
