// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VARP.VisibilityEditor.Editor
{
	public class CategoriesWindow : EditorWindow {

		[MenuItem("Window/Rocket/Categories")]
		public static void ShowWindow ()
		{
			GetWindow<CategoriesWindow>("Rocket: Categories");
		}
	
		private static Texture ArrowDownIcon;
		private static Texture VisibleIcon;
		private static Texture InvisibleIcon;
	
		private const float IconWidth = 22;
		private const float IconHeight = 22;
		private readonly GUILayoutOption IconWidthOption = GUILayout.Width(IconWidth);
		private readonly GUILayoutOption IconHeightOption = GUILayout.Height(IconHeight);
		private readonly GUILayoutOption LabelWidthOption = GUILayout.Width(200);
		private readonly GUILayoutOption QuantityWidthOption = GUILayout.Width(50);
		private readonly GUILayoutOption ColorWidthOption = GUILayout.Width(50);

		private GroupSettings CameraGroup;
		private GroupSettings PartilesGroup;
		private GroupSettings SoundsGroup;
		private GroupSettings GlobalsGroup;
		private GroupSettings RenderingGroup;
		private GroupSettings GameplayGroup;
		private GUIStyle ButtonStyle;
	
		void OnGUI ()
		{
			if (ButtonStyle == null)
			{
				ButtonStyle = new GUIStyle();
				ButtonStyle.padding = new RectOffset(1,1,1,1);
			}
			if (ArrowDownIcon == null)
				ArrowDownIcon = Resources.Load<Texture>("Icons/arrowDownBlack");
			if (VisibleIcon == null)
				VisibleIcon = Resources.Load<Texture>("Icons/visible");
			if (InvisibleIcon == null)
				InvisibleIcon = Resources.Load<Texture>("Icons/invisible");
		
			if (GlobalsGroup == null)
			{
				GlobalsGroup = new GroupSettings("Global", "Icons/envBall");
				GlobalsGroup.CreateCategory("FeatureOverlays", "Icons/overlay", Color.white);
				GlobalsGroup.CreateCategory("NavShapes", "Icons/navigation", Color.white);
				GlobalsGroup.CreateCategory("Traversal", "Icons/actor", Color.white);
			}
			if (GameplayGroup == null)
			{
				GameplayGroup = new GroupSettings("Gameplay", "Icons/pacman");
				GameplayGroup.CreateCategory("ActorsSpawners", "Icons/actor", Color.white);
				GameplayGroup.CreateCategory("Regions", "Icons/region", Color.white);
				GameplayGroup.CreateCategory("Splines", "Icons/spline", Color.white);
			}
			if (CameraGroup == null)
			{
				CameraGroup = new GroupSettings("Camera", "Icons/camera");
				CameraGroup.CreateCategory("ActorsSpawners", "Icons/actor", Color.white);
				CameraGroup.CreateCategory("Regions", "Icons/region", Color.white);
				CameraGroup.CreateCategory("Splines", "Icons/spline", Color.white);
			}
			if (SoundsGroup == null)
			{
				SoundsGroup = new GroupSettings("Sounds", "Icons/sound");
				SoundsGroup.CreateCategory("ActorsSpawners", "Icons/actor", Color.white);
				SoundsGroup.CreateCategory("Regions", "Icons/region", Color.white);
				SoundsGroup.CreateCategory("Splines", "Icons/spline", Color.white);
			}
			if (RenderingGroup == null)
			{
				RenderingGroup = new GroupSettings("Rendering", "Icons/rendering");
				RenderingGroup.CreateCategory("ActorsSpawners", "Icons/actor", Color.white);
				RenderingGroup.CreateCategory("Regions", "Icons/region", Color.white);
			}
			if (PartilesGroup == null)
			{
				PartilesGroup = new GroupSettings("Particles", "Icons/particle");
				PartilesGroup.CreateCategory("ActorsSpawners", "Icons/actor", Color.white);
				PartilesGroup.CreateCategory("Regions", "Icons/region", Color.white);
				PartilesGroup.CreateCategory("Splines", "Icons/spline", Color.white);
			}

			RenderGroup(GlobalsGroup);
			RenderGroup(GameplayGroup);
			RenderGroup(CameraGroup);
			RenderGroup(SoundsGroup);
			RenderGroup(RenderingGroup);
			RenderGroup(PartilesGroup);
		}
	
		private void CountObjects()
		{
			// TODO Implement metrics for all categories
		}

		/// <summary>
		/// Settings for single group
		/// </summary>
		private void RenderGroup(GroupSettings group)
		{
			GUILayout.BeginHorizontal();
			// -- 0 ---------------------------------------------------
			GUILayout.Box(group.Icon, ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 1 ---------------------------------------------------
			var isVisible = group.IsVisible;
			if (GUILayout.Button( isVisible ? VisibleIcon : InvisibleIcon, ButtonStyle, IconWidthOption, IconHeightOption))
				group.IsVisible = !isVisible;
			// -- 2 ---------------------------------------------------
			GUILayout.Box("", ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 3 ---------------------------------------------------
			GUILayout.Label(group.Name, EditorStyles.boldLabel);
			// -- 4 ---------------------------------------------------
			GUILayout.Label(group.Quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
			GUILayout.EndHorizontal();

			var categories = group.Categories;
			var count = categories.Count;
			for (var i=0; i<count; i++)
				RenderCategory(categories[i]);
		
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
		}
	

		/// <summary>
		/// Render single category
		/// </summary>
		private void RenderCategory(CategorySettings category)
		{
			var isVisible = category.IsVisible;

			GUILayout.BeginHorizontal();
			// -- 0 ---------------------------------------------------
			GUILayout.Box("", ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 1 ---------------------------------------------------
			if (GUILayout.Button(category.IsVisible ? VisibleIcon : InvisibleIcon, ButtonStyle, IconWidthOption, IconHeightOption))
				category.IsVisible = !category.IsVisible;
			// -- 2 ---------------------------------------------------
			GUILayout.Box(category.Icon, ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 3 ---------------------------------------------------
			GUILayout.Label(category.Name, EditorStyles.largeLabel);
			// -- 4 ---------------------------------------------------
			GUILayout.Label(category.Quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
		
			GUILayout.EndHorizontal();
		}
		
		/// <summary>
		/// Settings for single group
		/// </summary>
		private class GroupSettings
		{
			public readonly string Name;
			public readonly Texture Icon;
			public readonly List<CategorySettings> Categories = new List<CategorySettings>();
		
			public GroupSettings(string name, string iconName)
			{
				Name = name;
				Icon = Resources.Load<Texture>(iconName);
			}

			public CategorySettings CreateCategory(string name, string iconName, Color defaultColor)
			{
				var category = new CategorySettings(Name, name, iconName, defaultColor);
				Categories.Add(category);
				return category;
			}

			public int Quantity
			{
				get
				{
					var quantity = 0;
					var count = Categories.Count;
					for (var i = 0; i < count; i++)
						quantity += Categories[i].Quantity;
					return quantity;
				}	
			}
			public bool IsVisible
			{
				get
				{
					var count = Categories.Count;
					for (var i = 0; i < count; i++)
					{
						var category = Categories[i];
						if (category.IsVisible)
							return true;
					}
					return false;
				}
				set
				{
					var count = Categories.Count;
					for (var i = 0; i < count; i++)
						Categories[i].IsVisible = value;
				}
			}
		}
	
		/// <summary>
		/// Settings for single category
		/// </summary>
		private class CategorySettings
		{
			public readonly string GroupName;
			public readonly string Name;
			public readonly Texture Icon;

			public int Quantity;

			private readonly string visiblePreferenceName;
			private readonly string colorPreferenceNameR;
			private readonly string colorPreferenceNameG;
			private readonly string colorPreferenceNameB;
		
			public CategorySettings(string groupName, string name, string iconName, Color defaultColor)
			{
				GroupName = groupName;
				Name = name;
				Icon = Resources.Load<Texture>(iconName);
				visiblePreferenceName = "CategoriesWindowVisible" + groupName + name;
				colorPreferenceNameR = "CategoriesWindowColorR" + groupName + name;
				colorPreferenceNameG = "CategoriesWindowColorG" + groupName + name;
				colorPreferenceNameB = "CategoriesWindowColorB" + groupName + name;
				isVisible = GetVisibleInternal(true);
				color = GetColorInternal(defaultColor);
			}

			private bool isVisible;
			public bool IsVisible
			{
				get { return isVisible; }
				set { isVisible = value; SetVisibleInternal(value); }
			}
		
			private bool GetVisibleInternal(bool defaultValue)
			{
#if UNITY_EDITOR
				return EditorPrefs.GetBool(visiblePreferenceName, defaultValue);
#else
				return defaultValue;
#endif
			}
	
			private void SetVisibleInternal(bool value)
			{
#if UNITY_EDITOR
				EditorPrefs.SetBool(visiblePreferenceName, value);
#endif
			}

			private Color color;
			public Color Color
			{
				get { return color; }
				set { color = value; SetColorInternal(value); }
			}
		
			private Color GetColorInternal(Color defaultValue)
			{
#if UNITY_EDITOR
				var r = EditorPrefs.GetFloat(colorPreferenceNameR, defaultValue.r);
				var g = EditorPrefs.GetFloat(colorPreferenceNameR, defaultValue.g);
				var b = EditorPrefs.GetFloat(colorPreferenceNameR, defaultValue.b);
				return new Color(r, g, b);
#else
				return defaultValue;
#endif
			}
	
			private void SetColorInternal(Color value)
			{
#if UNITY_EDITOR
				EditorPrefs.SetFloat(colorPreferenceNameR, value.r);
				EditorPrefs.SetFloat(colorPreferenceNameG, value.g);
				EditorPrefs.SetFloat(colorPreferenceNameB, value.b);
#endif
			}
		}
	}
}