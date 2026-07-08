using UnityEngine;

public class ItemController : MonoBehaviour
{

    public Item myItem;


    public void Init(Item item)
    {
        myItem = item;

        item.OnPickedUp += ItemPickedUp;
    }


    public void ItemPickedUp()
    {
        Destroy(gameObject);
    }



    private void OnDisable()
    {
        myItem.OnPickedUp -= ItemPickedUp;
    }
}
