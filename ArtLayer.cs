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


#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace VARP.VisibilityEditor
{
    /// <summary>
    ///     Representation for single layer
    /// </summary>
    public class ArtLayer
    {
        public int Index;
        public int Mask;
        public string Name;

        private readonly string colorPreferenceNameR;
        private readonly string colorPreferenceNameG;
        private readonly string colorPreferenceNameB;

        /// <summary>
        ///     Construct new layer
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="defaultColor"></param>
        public ArtLayer(int index, string name, Color defaultColor)
        {
            Name = name;
            Index = index;
            Mask = 1 << index;
            colorPreferenceNameR = "LayersWindowColorR" + name;
            colorPreferenceNameG = "LayersWindowColorG" + name;
            colorPreferenceNameB = "LayersWindowColorB" + name;
            Color = GetColorInternal(defaultColor);
        }

        /// <summary>
        ///     Is this layer locked
        /// </summary>
        public bool IsLocked
        {
#if UNITY_EDITOR
            get
            {
                return (Tools.lockedLayers & Mask) > 0;
            }
            set
            {
                if (value)
                    Tools.lockedLayers |= Mask;
                else
                    Tools.lockedLayers &= ~Mask;
            }
#else
            get;
            set;
#endif
        }

        /// <summary>
        ///     Is this layer visible
        /// </summary>
        public bool IsVisible
        {
#if UNITY_EDITOR
            get
            {
                return (Tools.visibleLayers & Mask) > 0;
            }
            set
            {
                var was = Tools.visibleLayers;

                if (value)
                    Tools.visibleLayers |= Mask;
                else
                    Tools.visibleLayers &= ~Mask;
                if (was != Tools.visibleLayers)
                    SceneView.RepaintAll();
            }
#else
            get;
            set;
#endif
        }

        private Color color;

        
        /// <summary>
        ///     Get color of this layer
        /// </summary>
        public Color Color
        {
            get => color;
            set
            {
                if (color != value) SetColorInternal(value);
                color = value;
                fillColor = value;
                fillColor.a = 0.5f;
            }
        }

        private Color fillColor;
        
        /// <summary>
        ///     Get fill color of this layer. Usually same as color but more transparent
        /// </summary>
        public Color FillColor
        {
            get => fillColor;
            set => fillColor = value;
        }

        private void SetColorInternal(Color value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat(colorPreferenceNameR, value.r);
            EditorPrefs.SetFloat(colorPreferenceNameG, value.g);
            EditorPrefs.SetFloat(colorPreferenceNameB, value.b);
#endif
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
    }
}