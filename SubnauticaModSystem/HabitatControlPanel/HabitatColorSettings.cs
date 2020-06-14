﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HabitatControlPanel
{
	class HabitatColorSettings : MonoBehaviour
	{
		private bool hasPower = false;

		public RectTransform rectTransform;
		public Action onClick = delegate { };

		[SerializeField]
		private HabitatControlPanel target;
		[SerializeField]
		private ColoredIconButton activeButton;

		private void Awake()
		{
			rectTransform = transform as RectTransform;
		}

		private void Initialize(HabitatControlPanel controlPanel, Text textPrefab, string label)
		{
			target = controlPanel;

			activeButton = ColoredIconButton.Create(transform, HabitatControlPanel.ScreenContentColor, textPrefab, label, 150, 15);
			activeButton.text.supportRichText = true;
		}

		internal void SetInitialValue(Color color)
		{
			SetColor(color);
			activeButton.onClick += OnClick;
		}

		internal void SetColor(Color color)
		{
			activeButton.Initialize("Circle.png", color);
		}

		private void OnClick()
		{
			onClick();
		}

		private void Update()
		{
			if (Mod.config.RequireBatteryToUse)
			{
				hasPower = target != null && target.GetPower() > 0;
				activeButton.isEnabled = hasPower;
			}
		}


		///////////////////////////////////////////////////////////////////////////////////////////
		public static HabitatColorSettings Create(HabitatControlPanel controlPanel, Transform parent, string label)
		{
			var lockerPrefab = Resources.Load<GameObject>("Submarine/Build/SmallLocker");
			var textPrefab = Instantiate(lockerPrefab.GetComponentInChildren<Text>());
			textPrefab.fontSize = 12;
			textPrefab.color = HabitatControlPanel.ScreenContentColor;

			var habitatColorSettings = new GameObject("HabitatColorSettings", typeof(RectTransform)).AddComponent<HabitatColorSettings>();
			var rt = habitatColorSettings.gameObject.transform as RectTransform;
			RectTransformExtensions.SetParams(rt, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), parent);
			habitatColorSettings.Initialize(controlPanel, textPrefab, label);

			return habitatColorSettings;
		}

		internal void SetInitialValue(object interiorColor)
		{
			throw new NotImplementedException();
		}
	}
}
