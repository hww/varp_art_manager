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

using UnityEditor;
using UnityEngine;

namespace VARP.VisibilityEditor.Editor
{
	public class LayersWindow : EditorWindow {

		[MenuItem("Window/Rocket/Layers")]
		public static void ShowWindow ()
		{
			GetWindow<LayersWindow>("Rocket: Layers");
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
			if (LockIcon == null)
				LockIcon = Resources.Load<Texture>("Icons/lock");
			if (UnlockIcon == null)
				UnlockIcon = Resources.Load<Texture>("Icons/unlock");		
			if (LayerImage == null)
				LayerImage = Resources.Load<Texture>("Icons/layer");

			// -- initialize all layers --
			var layers = ArtLayers.Layers;
			for (var i = 0; i < layers.Length; i++)
			{
				var layer = layers[i];
				if (layer!=null)
					LayerViews[i] = new LayerView(layer, "Icons/layer");
			}
			CountObjects();		
		}
	
		private static Texture ArrowDownIcon;
		private static Texture VisibleIcon;
		private static Texture InvisibleIcon;
		private static Texture LockIcon;
		private static Texture UnlockIcon;
		private static Texture LayerImage;
		private static Texture RegionIcon;
		private static Texture SplineIcon;
		private static Texture CameraIcon;
	 
		private const float IconWidth = 28;
		private const float IconHeight = 28;
		private readonly GUILayoutOption IconWidthOption = GUILayout.Width(IconWidth);
		private readonly GUILayoutOption IconHeightOption = GUILayout.Height(IconHeight);
		private readonly GUILayoutOption LabelWidthOption = GUILayout.Width(200);
		private readonly GUILayoutOption QuantityWidthOption = GUILayout.Width(50);
		private readonly GUILayoutOption ColorWidthOption = GUILayout.Width(30);
		private GUIStyle ButtonStyle;
	
		public static readonly LayerView[] LayerViews = new LayerView[32];
		
		void OnGUI ()
		{
			// -- render tool bar --
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Count Objects"))
				CountObjects();
			ArtLayers.ApplyColors = GUILayout.Toggle(ArtLayers.ApplyColors, "Use Layer Colors");
			GUILayout.EndHorizontal();

			EditorGUILayout.HelpBox("Reserved by Unity layers", MessageType.None);
			// -- render layers --
			for (var i = 0; i < LayerViews.Length; i++)
			{
				var layer = LayerViews[i];
				if (layer!=null)
					RenderLayer(layer);
				// draw separator after unity default layers
				if (i == 7)
				{
					GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
					EditorGUILayout.HelpBox("Reserved for Game layers", MessageType.None);
				}
			}
		}

		/// <summary>
		/// Render single line
		/// </summary>
		private void RenderLayer(LayerView layerView)
		{
			var layer = layerView.Layer;
			GUILayout.BeginHorizontal();
			// -- 1 ---------------------------------------------------
			var isVisible = layer.IsVisible;
			if (GUILayout.Button( isVisible ? VisibleIcon : InvisibleIcon, ButtonStyle, IconWidthOption, IconHeightOption))
				layer.IsVisible = !isVisible;
			var isLock = layer.IsLocked;
			if (GUILayout.Button( isLock ? LockIcon : UnlockIcon, ButtonStyle, IconWidthOption, IconHeightOption))
				layer.IsLocked = !isLock;
			// -- 2 ---------------------------------------------------
			layer.Color = EditorGUILayout.ColorField( layer.Color, ColorWidthOption);
			// -- 3 ---------------------------------------------------
			GUILayout.Box(layerView.Icon, ButtonStyle, IconWidthOption, IconHeightOption);
			// -- 4 ---------------------------------------------------
			GUILayout.Label(layer.Name, EditorStyles.largeLabel);
			// -- 5 ---------------------------------------------------
			GUILayout.Label(layerView.Quantity.ToString(), EditorStyles.boldLabel, QuantityWidthOption);
		
			GUILayout.EndHorizontal();
		}

		/// <summary>
		/// Representation for single layer
		/// </summary>
		public class LayerView
		{
			public Texture Icon;
			public ArtLayer Layer;
			public int Quantity;
			
			public LayerView(ArtLayer layer, string iconName)
			{
				Layer = layer;
				Icon = Resources.Load<Texture>(iconName);
			}
		}
		
		public static void CountObjects()
		{
			if (LayerViews == null) return;
			var counts = LayersTools.CountObjectsInAllLayers();
			for (var i = 0; i < LayerViews.Length; i++)
			{
				var layer = LayerViews[i];
				if (layer != null)
					layer.Quantity = counts[i];
			}
		}
	}
}