using UnityEngine;

public class ItemController : MonoBehaviour
{

    public Item myItem;

    void Update()
    {
        UpdateVisualPosition();
    }

    public void Init(Item item)
    {
        myItem = item;

        item.OnPickedUp -= ItemPickedUp;
        item.OnPickedUp += ItemPickedUp;
    }


    public void ItemPickedUp()
    {
        myItem.SetTile(myItem.gridPos, false);
        Destroy(gameObject);
    }

    public void UpdateVisualPosition()
    {

        if (myItem != null)
        {
            transform.position = myItem.gridPos.worldPos;
        }

    }

    private void OnDisable()
    {
        myItem.OnPickedUp -= ItemPickedUp;
    }
}
