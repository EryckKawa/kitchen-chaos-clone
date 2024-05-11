using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
	[Serializable]
	public struct KitchenObjectSO_GameObject
	{
		public KitchenObjectSO kitchenObjectSO;
		public GameObject gameObject;
	}
	[SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectsList;

	private void Start()
	{
		plateKitchenObject.OnIngredientAdded += PlateKitchen_OnIngrendientAdded;

		foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectsList)
		{
			kitchenObjectSO_GameObject.gameObject.SetActive(false);
		}
	}

	private void PlateKitchen_OnIngrendientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
	{
		foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectsList)
		{
			if (kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO)
			{
				kitchenObjectSO_GameObject.gameObject.SetActive(true);
			}
		}
	}
}