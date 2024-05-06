using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
	[SerializeField] private StoveCounter stoveCounter;
	[SerializeField] private GameObject stoveOnGameObject;
	[SerializeField] private GameObject particlesGameObject;

	private void Start()
	{
		stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
	}

	private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedArgs onStateChangedArgs)
	{
		bool showVisual = onStateChangedArgs.state == StoveCounter.State.Frying || onStateChangedArgs.state == StoveCounter.State.Fried;
		
		stoveOnGameObject.SetActive(showVisual);
		particlesGameObject.SetActive(showVisual);
	}
}
