using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
	[SerializeField] private CuttingRecipeSO[] cutRecipeSOArray;
	private int cuttingProgress;

	public override void Interact(Player player)
	{
		if (!HasKitchenObject())
		{
			if (player.HasKitchenObject())
			{
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
				{
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;
				}
			}
		}
		else
		{
			//Player carregando objeto
			if (player.HasKitchenObject())
			{

			}
			else
			{
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}

	public override void InteractAlternate(Player player)
	{
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
		{
			cuttingProgress++;

			CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

			KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

			GetKitchenObject().DestroySelf();

			KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
		}
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
	{
		CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
		return cuttingRecipeSO != null;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
	{
		CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
		if (cuttingRecipeSO != null)
		{
			return cuttingRecipeSO.output;
		}
		else
		{
			return null;
		}
	}

	private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
	{
	
		foreach (CuttingRecipeSO cuttingRecipeSO in cutRecipeSOArray)
		{
			if (cuttingRecipeSO.input == inputKitchenObjectSO)
			{
				return cuttingRecipeSO;
			}
		}
		return null;
	}
}