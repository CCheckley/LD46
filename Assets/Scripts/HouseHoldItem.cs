using UnityEngine;

public enum ChargerType
{
    None,
    A,
    B,
    C
}

[CreateAssetMenu(menuName = "HouseHoldItem", fileName = "New House Hold Item")]
public class HouseHoldItem : ScriptableObject
{
    public Sprite sprite;
    public bool isChargerPart;
    public bool isCable;
    public ChargerType chargerType;
}
