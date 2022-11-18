using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollection : MonoBehaviour
{
    public int packages = 0;
    public bool isCanvasText = false;
    public TextMeshProUGUI packageText;

    private void Start()
    {
        if (isCanvasText)
        {
            packageText.text = "Packages : 0";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            Destroy(collision.gameObject);
            packages++;
            UpdatePackageUI();
        }
    }
    public void LogPackages()
    {
        Debug.Log("Package number: " + packages);
    }

    public void UpdatePackageUI()
    {
        if (isCanvasText)
        {
            packageText.text = "Packages : " + packages;
        }
    }
}
