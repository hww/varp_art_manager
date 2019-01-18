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
using Plugins.VARP.ArtPrimitives;
using Plugins.VARP.Splines;
using UnityEditor;
using UnityEngine;

namespace Plugins.VARP.VisibilityEditor.Editor
{
	public class CategoriesWindow : EditorWindow {

	
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
		
			if (GlobalsGroup == null)
				GlobalsGroup = new GroupView(ArtGroups.Globals, "Icons/envBall");
			if (GameplayGroup == null)
				GameplayGroup = new GroupView(ArtGroups.Gameplay, "Icons/pacman");
			if (CameraGroup == null)
				CameraGroup = new GroupView(ArtGroups.Camera, "Icons/camera");
			if (SoundsGroup == null)
				SoundsGroup = new GroupView(ArtGroups.Sounds, "Icons/sound");
			if (RenderingGroup == null)
				RenderingGroup = new GroupView(ArtGroups.Rendering, "Icons/rendering");
			if (PartilesGroup == null)
				PartilesGroup = new GroupView(ArtGroups.Particles, "Icons/particle");
			CountObjects();
		}
	
		void OnGUI ()
		{
			// -- render tool bar --
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Count Objects"))
				CountObjects();
			GUILayout.EndHorizontal();
			RenderGroup(GlobalsGroup);
			RenderGroup(GameplayGroup);
			RenderGroup(CameraGroup);
			RenderGroup(SoundsGroup);
			RenderGroup(RenderingGroup);
			RenderGroup(PartilesGroup);
		}
	
		private void CountObjects()
		{
			CameraGroup.PreCountArtObjects();
			GlobalsGroup.PreCountArtObjects();
			GlobalsGroup.PreCountArtObjects();
			RenderingGroup.PreCountArtObjects();
			GameplayGroup.PreCountArtObjects();
            
			var objects = FindObjectsOfType<ArtPrimitive>();
            
			for (var i = 0; i < objects.Length; i++)
			{
				var obj = objects[i];
				if (obj == null)
					continue;
				CountObject(obj);
			}
			
			// now update counters of all groups
			CameraGroup.PostCountArtObjects();
			GlobalsGroup.PostCountArtObjects();
			GlobalsGroup.PostCountArtObjects();
			RenderingGroup.PostCountArtObjects();
			GameplayGroup.PostCountArtObjects();
		}
        
		private void CountObject(ArtPrimitive obj)
		{
			switch (obj.ArtGroup)
			{
				case EArtGroup.Camera:
					CameraGroup.CountArtObject(obj);
					break;
				case EArtGroup.Sounds:
					GlobalsGroup.CountArtObject(obj);
					break;
				case EArtGroup.Globals:
					GlobalsGroup.CountArtObject(obj);
					break;
				case EArtGroup.Rendering:
					RenderingGroup.CountArtObject(obj);
					break;
				case EArtGroup.Gameplay:
					GameplayGroup.CountArtObject(obj);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}


		/// <summary>
		/// Settings for single group
		/// </summary>
		private void RenderGroup(GroupView groupView)
		{
			var group = groupView.artGroup;
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
			GUILayout.Label(groupView.Quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
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
			var category = categoryView.artCategory;
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
			GUILayout.Label(categoryView.Quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
		
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
			Icon = Resources.Load<Texture>(iconName);
			
			if (artGroup.FeatureOverlays != null)
				featureOverlays = CreateCategoryView(artGroup.FeatureOverlays, "Icons/overlay");
			if (artGroup.NavShapes != null)
				navShapes = CreateCategoryView(artGroup.NavShapes, "Icons/navigation");
			if (artGroup.Traversal != null)
				traversal = CreateCategoryView(artGroup.Traversal, "Icons/actor");
			if (artGroup.ActorsSpawners != null)
				actorsSpawners = CreateCategoryView(artGroup.ActorsSpawners, "Icons/actor");
			if (artGroup.Regions != null)
				regions = CreateCategoryView(artGroup.Regions, "Icons/region");
			if (artGroup.Splines != null)
				splines = CreateCategoryView(artGroup.Splines, "Icons/spline");
		}

		private CategoryView CreateCategoryView(ArtCategory artCategory, string iconName)
		{
			Debug.Assert(artCategory != null);
			var categoryView = new CategoryView(artCategory, iconName);
			Categories.Add(categoryView);
			return categoryView;
		}
		
		        
		public void PreCountArtObjects()
		{
			for (var i = 0; i < Categories.Count; i++)
				Categories[i].Quantity = 0;
		}
            
		public void CountArtObject(ArtPrimitive obj)
		{
			if (obj is Spawner)
				actorsSpawners.Quantity++;
			else if (obj is BezierSpline)
				splines.Quantity++;
			//TODO!
			//else if (obj is Spawner)
			//	featureOverlays.Quantity++;
			//else if (obj is Spawner)
			//	navShapes.Quantity++;
			//else if (obj is Spawner)
			//	traversal.Quantity++;
			//else if (obj is Spawner)
			//	actorsSpawners.Quantity++;
			//else if (obj is Spawner)
			//	regions.Quantity++;

		}

		public void PostCountArtObjects()
		{
			Quantity = 0;
			var count = Categories.Count;
			for (var i = 0; i < count; i++)
			{
				var category = Categories[i];
				if (category != null)
					Quantity += category.Quantity;
			}
		}
	}

	/// <summary>
	/// Interface to the CategorySettings
	/// </summary>
	public  class CategoryView
	{
		public readonly Texture Icon;
		public readonly ArtCategory artCategory;
		public int Quantity;
		
		public CategoryView(ArtCategory artCategory, string iconName)
		{
			Debug.Assert(artCategory != null);
			this.artCategory = artCategory;
			Icon = Resources.Load<Texture>(iconName);
		}
	}
}