using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollection : MonoBehaviour
{
    public int packages = 0;
    public bool isCanvasText = false;
    public TextMeshProUGUI packageText;
    private SoundControl sc;

    private void Start()
    {
        if (isCanvasText)
        {
            packageText.text = "Packages : 0";
        }
        sc = GameObject.FindObjectOfType<SoundControl>();
    }

    // When activated by the PackageTrigger script, the package number is updated, a tune plays, and the UI is updated
    public void SetPackagesNumber(int count)
    {
        packages = count;
        sc.PickupSFX();
        UpdatePackageUI();
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
