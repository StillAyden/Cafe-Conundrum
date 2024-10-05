using System;
using UnityEngine;

[CreateAssetMenu]
public class drinkData : ScriptableObject
{
    public drinkItems[] items;
}

[Serializable]
public struct drinkItems
{
    public Drink type;
    public GameObject prefab;
    public Sprite image;
}

[Serializable]
public enum Drink
{
    None = -1,
    Soda,
    Coffee
}
