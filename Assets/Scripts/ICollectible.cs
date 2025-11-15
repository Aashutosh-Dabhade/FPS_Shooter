using UnityEngine;

public interface ICollectible
{
    string ItemName { get; }
    void Collect(PlayerInventory inventory);
}
