using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        this.kitchenObjectParent?.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Essa bancada já tem um Objeto!");
        }

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetClearCounter()
    {
        return kitchenObjectParent;
    }
    
}
