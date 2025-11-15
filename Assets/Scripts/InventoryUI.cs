using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Parent where images will be added
    public Transform ItemContainer;

    // Prefab of the Image to instantiate
    public Image ItemImagePrefab;

    // Example sprite to assign (can be passed from elsewhere)
    public Sprite DefaultSprite;

    // Adds a new image as a child every time called
    public void AddImage(Sprite sprite = null)
    {
        if (ItemContainer == null || ItemImagePrefab == null)
        {
            Debug.LogWarning("ItemContainer or ItemImagePrefab is not assigned!");
            return;
        }

        // Instantiate a new image under the container
        Image newImage = Instantiate(ItemImagePrefab, ItemContainer);

        // Assign the sprite (use default if none provided)
        newImage.sprite = sprite != null ? sprite : DefaultSprite;

        // Make sure the image is active
        newImage.gameObject.SetActive(true);

        // Optionally, you can adjust size, position, etc.
        newImage.rectTransform.localScale = Vector3.one;
    }

    public void ClearImages()
    {
        if (ItemContainer == null) return;
        
        foreach (Transform child in ItemContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
