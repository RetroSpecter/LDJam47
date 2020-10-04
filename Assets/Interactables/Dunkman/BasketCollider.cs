using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DunkmanItem>()) {
            collision.GetComponent<DunkmanNPC>().Dunked();
            Player.Instance.RemoveItem();
            // have character land
        }
    }
}
