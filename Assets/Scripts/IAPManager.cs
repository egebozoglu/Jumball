using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{

    public string productName = "com.bosegames.premium";

    public void PurchaseCompelete(Product product)
    {
        if (product.definition.id == productName)
        {
            Debug.Log("Purhase Compeleted");
            PlayerPrefs.SetInt("Premium", 1);
        }
    }

    public void PurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase Failed.");
    }
}
