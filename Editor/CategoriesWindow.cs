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

		private GroupView CameraGroup;
		private GroupView PartilesGroup;
		private GroupView SoundsGroup;
		private GroupView GlobalsGroup;
		private GroupView RenderingGroup;
		private GroupView GameplayGroup;
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
				GlobalsGroup = new GroupView(GameGroups.Globals, "Icons/envBall");
			if (GameplayGroup == null)
				GameplayGroup = new GroupView(GameGroups.Gameplay, "Icons/pacman");
			if (CameraGroup == null)
				CameraGroup = new GroupView(GameGroups.Camera, "Icons/camera");
			if (SoundsGroup == null)
				SoundsGroup = new GroupView(GameGroups.Sounds, "Icons/sound");
			if (RenderingGroup == null)
				RenderingGroup = new GroupView(GameGroups.Rendering, "Icons/rendering");
			if (PartilesGroup == null)
				PartilesGroup = new GroupView(GameGroups.Partiles, "Icons/particle");

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
		private void RenderGroup(GroupView groupView)
		{
			var group = groupView.GameGroup;
			GUILayout.BeginHorizontal();
			// -- 0 ---------------------------------------------------
			GUILayout.Box(groupView.Icon, ButtonStyle, IconWidthOption, IconHeightOption);
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

			var categories = groupView.Categories;
			var count = categories.Count;
			for (var i=0; i<count; i++)
				RenderCategory(categories[i]);
		
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
		}
	

		/// <summary>
		/// Render single category
		/// </summary>
		private void RenderCategory(CategoryView categoryView)
		{
			var category = categoryView.Category;
			var isVisible = category.IsVisible;

			GUILayout.BeginHorizontal();
			// -- 0 ---------------------------------------------------
			GUILayout.Box("", ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 1 ---------------------------------------------------
			if (GUILayout.Button(category.IsVisible ? VisibleIcon : InvisibleIcon, ButtonStyle, IconWidthOption, IconHeightOption))
				category.IsVisible = !category.IsVisible;
			// -- 2 ---------------------------------------------------
			GUILayout.Box(categoryView.Icon, ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 3 ---------------------------------------------------
			GUILayout.Label(category.Name, EditorStyles.largeLabel);
			// -- 4 ---------------------------------------------------
			GUILayout.Label(category.Quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
		
			GUILayout.EndHorizontal();
		}
	}

	/// <summary>
	/// Interface to the GroupSettings
	/// </summary>
	public class GroupView
	{
		public readonly Texture Icon;
		public readonly List<CategoryView> Categories = new List<CategoryView>();
		public readonly GameGroup GameGroup;
		public GroupView(GameGroup gameGroup, string iconName)
		{
			Debug.Assert(gameGroup != null);
			GameGroup = gameGroup;
			Icon = Resources.Load<Texture>(iconName);
			
			if (gameGroup.FeatureOverlays != null)
				CreateCategoryView(gameGroup.FeatureOverlays, "Icons/overlay");
			if (gameGroup.NavShapes != null)
				CreateCategoryView(gameGroup.NavShapes, "Icons/navigation");
			if (gameGroup.Traversal != null)
				CreateCategoryView(gameGroup.Traversal, "Icons/actor");
			if (gameGroup.ActorsSpawners != null)
				CreateCategoryView(gameGroup.ActorsSpawners, "Icons/actor");
			if (gameGroup.Regions != null)
				CreateCategoryView(gameGroup.Regions, "Icons/region");
			if (gameGroup.Splines != null)
				CreateCategoryView(gameGroup.Splines, "Icons/spline");
		}

		public void CreateCategoryView(Category category, string iconName)
		{
			Debug.Assert(category != null);
			Categories.Add(new CategoryView(category, iconName));
		}
	}

	/// <summary>
	/// Interface to the CategorySettings
	/// </summary>
	public  class CategoryView
	{
		public readonly Texture Icon;
		public readonly Category Category;
		public CategoryView(Category category, string iconName)
		{
			Debug.Assert(category != null);
			Category = category;
			Icon = Resources.Load<Texture>(iconName);
		}
	}
}