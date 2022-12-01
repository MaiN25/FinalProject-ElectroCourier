using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageTrigger : MonoBehaviour
{
    // When the player walks into a package, the player's ItemCollection script is found, the number of packages increases by one, the UI is updated, and the gameobject is disabled
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ItemCollection itemCollection = collision.GetComponent<ItemCollection>();
            if(itemCollection != null)
            {
                int packagesCount = itemCollection.packages + 1;
                itemCollection.SetPackagesNumber(packagesCount);
                gameObject.SetActive(false);
            }
        }
    }
}
