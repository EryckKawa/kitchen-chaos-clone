using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
	[SerializeField] private List<KitchenObjectSO> validKitchenObjectList;

	private List<KitchenObjectSO> kitchenObjectList;

	private void Awake()
	{
		kitchenObjectList = new List<KitchenObjectSO>();
	}

	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
	{
		if (!validKitchenObjectList.Contains(kitchenObjectSO))
		{
			return false;
		}
		if (kitchenObjectList.Contains(kitchenObjectSO))
		{
			return false;
		}
		else
		{
			kitchenObjectList.Add(kitchenObjectSO);
			return true;
		}
	}
}
