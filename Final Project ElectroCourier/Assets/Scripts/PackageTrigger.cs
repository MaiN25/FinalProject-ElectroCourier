using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageTrigger : MonoBehaviour
{
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
