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

using System;
using System.Collections.Generic;
using VARP.ArtPrimitives;
using UnityEditor;
using UnityEngine;

namespace VARP.VisibilityEditor.Editor
{
	public class CategoriesWindow : EditorWindow {

	
		private static Texture ArrowDownIcon;
		private static Texture VisibleIcon;
		private static Texture InvisibleIcon;
	
		private const float ICON_WIDTH = 22;
		private const float ICON_HEIGHT = 22;
		private readonly GUILayoutOption IconWidthOption = GUILayout.Width(ICON_WIDTH);
		private readonly GUILayoutOption IconHeightOption = GUILayout.Height(ICON_HEIGHT);
		private readonly GUILayoutOption LabelWidhtOption = GUILayout.Width(200);
		private readonly GUILayoutOption QuantityWidthOption = GUILayout.Width(50);
		private readonly GUILayoutOption ColorWidthOption = GUILayout.Width(50);

		private GUIStyle ButtonStyle;
		private GroupView[] GroupViews = new GroupView[(int)ArtGroupTag.ArtGroupsCount];
				
		[MenuItem("Window/Rocket/Categories")]
		public static void ShowWindow ()
		{
			GetWindow<CategoriesWindow>("Rocket: Categories");
		}

		void OnEnable()
		{
			EditorApplication.hierarchyChanged -= CountObjects;
			EditorApplication.hierarchyChanged += CountObjects;
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
			
			ArtGroups.Initialize();
			
			CreateGroupView(ArtGroups.Globals, "Icons/envBall");
			CreateGroupView(ArtGroups.Gameplay, "Icons/pacman");
			CreateGroupView(ArtGroups.Camera, "Icons/camera");
			CreateGroupView(ArtGroups.Sounds, "Icons/sound");
			CreateGroupView(ArtGroups.Rendering, "Icons/rendering");
			CreateGroupView(ArtGroups.Particles, "Icons/particle");
			CountObjects();
		}

		private GroupView CreateGroupView(ArtGroup artGroup, string iconName)
		{
			var groupView = new GroupView(artGroup, iconName);	
			GroupViews[(int)artGroup.artGroupTag] = groupView;
			return groupView;
		}
		
		private void OnDisable()
		{
			for (var i = 0; i < GroupViews.Length; i++)
				GroupViews[i] = null;
		}

		void OnGUI ()
		{
			// -- render tool bar --
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Count Objects"))
				CountObjects();
			GUILayout.EndHorizontal();
			for (var i = 0; i < GroupViews.Length; i++)
				RenderGroup(GroupViews[i]);
		}
	
		private void CountObjects()
		{
			for (var i = 0; i < GroupViews.Length; i++)
				GroupViews[i].PreCountArtObjects();
            
			var objects = FindObjectsOfType<ArtPrimitive>();
            
			for (var i = 0; i < objects.Length; i++)
			{
				var obj = objects[i];
				if (obj == null)
					continue;
				CountObject(obj);
			}
			
			// now update counters of all groups
			for (var i = 0; i < GroupViews.Length; i++)
				GroupViews[i].PostCountArtObjects();
		}
        
		private void CountObject(ArtPrimitive obj)
		{
			var group = GetGroup(obj.artGroupTag);
			group.CountArtObject(obj);
		}
		
		private GroupView GetGroup(ArtGroupTag artGroupTag)
		{
			return GroupViews[(int) artGroupTag];
		}

		/// <summary>
		/// Settings for single group
		/// </summary>
		private void RenderGroup(GroupView groupView)
		{
			var group = groupView.artGroup;
			GUILayout.BeginHorizontal();
			// -- 0 ---------------------------------------------------
			GUILayout.Box(groupView.icon, ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 1 ---------------------------------------------------
			var isVisible = group.IsVisible;
			if (GUILayout.Button( isVisible ? VisibleIcon : InvisibleIcon, ButtonStyle, IconWidthOption, IconHeightOption))
				group.IsVisible = !isVisible;
			// -- 2 ---------------------------------------------------
			GUILayout.Box("", ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 3 ---------------------------------------------------
			GUILayout.Label(group.artGroupTag.ToString(), EditorStyles.boldLabel);
			// -- 4 ---------------------------------------------------
			GUILayout.Label(groupView.Quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
			GUILayout.EndHorizontal();

			var categories = groupView.categories;
			var count = categories.Length;
			for (var i = 0; i < count; i++)
			{
				var category = categories[i];
				if (category.isOptional && category.quantity == 0)
					continue;
				RenderCategory(category);
			}
		
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
		}
	

		/// <summary>
		/// Render single category
		/// </summary>
		private void RenderCategory(CategoryView categoryView)
		{
			var category = categoryView.category;

			GUILayout.BeginHorizontal();
			// -- 0 ---------------------------------------------------
			GUILayout.Box("", ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 1 ---------------------------------------------------
			if (GUILayout.Button(category.IsVisible ? VisibleIcon : InvisibleIcon, ButtonStyle, IconWidthOption, IconHeightOption))
				category.IsVisible = !category.IsVisible;
			// -- 2 ---------------------------------------------------
			GUILayout.Box(categoryView.icon, ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 3 ---------------------------------------------------
			GUILayout.Label(category.artCategoryTag.ToString(), EditorStyles.largeLabel);
			// -- 4 ---------------------------------------------------
			GUILayout.Label(categoryView.quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
		
			GUILayout.EndHorizontal();
		}
		
	}

	/// <summary>
	/// Interface to the GroupSettings
	/// </summary>
	public class GroupView
	{
		public readonly Texture icon;
		public readonly CategoryView[] categories = new CategoryView[(int)ArtCategoryTag.Count];
		public readonly ArtGroup artGroup;
		public int Quantity;
		
		private readonly CategoryView featureOverlays;
		private readonly CategoryView navShapes;
		private readonly CategoryView traversal;
		private readonly CategoryView actorsSpawners;
		private readonly CategoryView regions;
		private readonly CategoryView splines;
		
		public GroupView(ArtGroup artGroup, string iconName)
		{
			Debug.Assert(artGroup != null);
			this.artGroup = artGroup;
			icon = Resources.Load<Texture>(iconName);
			
			featureOverlays = CreateCategoryView(artGroup.FeatureOverlays, "Icons/overlay");
			navShapes = CreateCategoryView(artGroup.NavShapes, "Icons/navigation");
			traversal = CreateCategoryView(artGroup.Traversal, "Icons/actor");
			actorsSpawners = CreateCategoryView(artGroup.ActorsSpawners, "Icons/actor");
			regions = CreateCategoryView(artGroup.Regions, "Icons/region");
			splines = CreateCategoryView(artGroup.Splines, "Icons/spline");
		}

		private CategoryView CreateCategoryView(ArtCategory artCategory, string iconName)
		{
			if (artCategory == null)
				return null;
			var categoryView = new CategoryView(artCategory, iconName);
			categories[(int)categoryView.category.artCategoryTag] = categoryView;
			return categoryView;
		}
		
		        
		public void PreCountArtObjects()
		{
			for (var i = 0; i < categories.Length; i++)
				categories[i].quantity = 0;
		}
		
		public void CountArtObject(ArtPrimitive obj)
		{
			switch (obj.artCategoryTag)
			{
				case ArtCategoryTag.ActorsSpawners:
					actorsSpawners.quantity++;
					break;
				case ArtCategoryTag.NavShapes:
					navShapes.quantity++;
					break;
				case ArtCategoryTag.Splines:
					splines.quantity++;
					break;
				case ArtCategoryTag.Regions:
					regions.quantity++;
					break;
				case ArtCategoryTag.Traversal:
					traversal.quantity++;
					break;
				case ArtCategoryTag.FeatureOverlays:
					featureOverlays.quantity++;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void PostCountArtObjects()
		{
			Quantity = 0;
			var count = categories.Length;
			for (var i = 0; i < count; i++)
			{
				var category = categories[i];
				if (category != null)
					Quantity += category.quantity;
			}
		}
	}

	/// <summary>
	/// Interface to the CategorySettings
	/// </summary>
	public  class CategoryView
	{
		public readonly Texture icon;
		public readonly ArtCategory category;
		public readonly bool isOptional;
		public int quantity;
		
		public CategoryView(ArtCategory category, string iconName)
		{
			this.category = category;
			isOptional = category.isOptional;
			icon = Resources.Load<Texture>(iconName);
		}
	}
}