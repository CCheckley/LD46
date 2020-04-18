using UnityEngine;

public class Searchable : MonoBehaviour
{
    public bool isSearched = false;
    [SerializeField] HouseHoldItem[] items;

    public void Search()
    {
        foreach (HouseHoldItem item in items)
        {
            PlayerController.items.Add(item);
        }
        isSearched = true;
    }
}
