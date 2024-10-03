using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class foodData : ScriptableObject
{
    [SerializeField] item[] items;
}

[Serializable]
struct item
{
    [SerializeField] Food type;
    [SerializeField] GameObject prefab;
    [SerializeField] Image image;
}

public enum Food
{
    None,
    Chips,
    Burger,
    Pizza
}
