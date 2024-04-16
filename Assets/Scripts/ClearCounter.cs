using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void Interact()
    {
        Debug.Log("Interact Clear Counter");
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.GetPrefab(),counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
    
        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().GetObjectName());
    }
    
}
