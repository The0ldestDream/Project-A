using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform Player; 

    public void Init(Transform player)
    {
        Player = player;       
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentPos = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        transform.position = currentPos;



    }
}
