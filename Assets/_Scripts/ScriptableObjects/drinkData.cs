using System;
using UnityEngine;

[CreateAssetMenu]
public class drinkData : MonoBehaviour
{
    public drinkItems[] items;
}

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
