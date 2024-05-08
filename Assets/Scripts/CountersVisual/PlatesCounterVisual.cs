using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
	[SerializeField] private PlatesCounter platesCounter;
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualPrefab;

	private List<GameObject> platesVisualGameObjectsList;

	private void Awake()
	{
		platesVisualGameObjectsList = new List<GameObject>();
	}

	private void Start()
	{
		platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
	}

	private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
	{
		GameObject plateGameObject = platesVisualGameObjectsList[platesVisualGameObjectsList.Count - 1];
		platesVisualGameObjectsList.Remove(plateGameObject);
		Destroy(plateGameObject);
	}

	private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
	{
		Transform platesVisualTransfom = Instantiate(plateVisualPrefab, counterTopPoint);

		float platesOffsetY = .1f;
		platesVisualTransfom.localPosition = new Vector3(0, platesOffsetY * platesVisualGameObjectsList.Count, 0);

		platesVisualGameObjectsList.Add(platesVisualTransfom.gameObject);
	}

}
