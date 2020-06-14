﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HabitatControlPanel
{
	class HabitatNameController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private bool hover;

		public RectTransform rectTransform;

		[SerializeField]
		private HabitatControlPanel target;
		[SerializeField]
		private Text habitatNameText;

		private void Awake()
		{
			rectTransform = transform as RectTransform;
		}

		private void Initialize(HabitatControlPanel controlPanel, Text textPrefab)
		{
			target = controlPanel;

			habitatNameText = GameObject.Instantiate(textPrefab);
			habitatNameText.fontSize = 16;
			habitatNameText.gameObject.name = "HabitatNameText";
			habitatNameText.rectTransform.SetParent(transform, false);
			RectTransformExtensions.SetSize(habitatNameText.rectTransform, 140, 50);

			habitatNameText.text = target.HabitatLabel;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			uGUI.main.userInput.RequestString("Habitat Name", "Submit", target.HabitatLabel, 25, new uGUI_UserInput.UserInputCallback(SetLabel));
		}

		public void SetLabel(string newLabel)
		{
			target.HabitatLabel = newLabel;
			habitatNameText.text = newLabel;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			hover = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			hover = false;
		}

		private void Update()
		{
			if (hover)
			{
				HandReticle.main.SetIcon(HandReticle.IconType.Rename);
				HandReticle.main.SetInteractTextRaw("Set Habitat Name", "");
			}
		}



		///////////////////////////////////////////////////////////////////////////////////////////
		public static HabitatNameController Create(HabitatControlPanel controlPanel, Transform parent)
		{
			var lockerPrefab = Resources.Load<GameObject>("Submarine/Build/SmallLocker");
			var textPrefab = Instantiate(lockerPrefab.GetComponentInChildren<Text>());
			textPrefab.fontSize = 12;
			textPrefab.color = HabitatControlPanel.ScreenContentColor;

			var habitatNameController = new GameObject("HabitatNameController", typeof(RectTransform)).AddComponent<HabitatNameController>();
			var rt = habitatNameController.gameObject.transform as RectTransform;
			RectTransformExtensions.SetParams(rt, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), parent);
			habitatNameController.Initialize(controlPanel, textPrefab);

			return habitatNameController;
		}
	}
}
