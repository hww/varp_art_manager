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
    ///     Settings for single category
    /// </summary>
    public class ArtCategory
    {
        public readonly ArtCategoryTag artCategoryTag;
        public readonly bool isOptional;
        private readonly string visiblePreferenceName;
        private readonly string colorPreferenceNameR;
        private readonly string colorPreferenceNameG;
        private readonly string colorPreferenceNameB;

        /// <summary>
        ///     Construct new category
        /// </summary>
        /// <param name="groupTag">Parent group</param>
        /// <param name="categoryTag">Category tag</param>
        /// <param name="defaultColor"></param>
        /// <param name="optional"></param>
        public ArtCategory(ArtGroupTag groupTag, ArtCategoryTag categoryTag, Color defaultColor, bool optional)
        {
            isOptional = optional;
            artCategoryTag = categoryTag;
            var artGroupName = groupTag.ToString();
            var categoryName = categoryTag.ToString();
            visiblePreferenceName = $"CategoriesWindowVisible{artGroupName}{categoryName}";
            colorPreferenceNameR = $"CategoriesWindowColorR{artGroupName}{categoryName}";
            colorPreferenceNameG = $"CategoriesWindowColorG{artGroupName}{categoryName}";
            colorPreferenceNameB = $"CategoriesWindowColorB{artGroupName}{categoryName}";
            isVisible = GetVisibleInternal(true);
            color = GetColorInternal(defaultColor);
        }
        
        private bool isVisible;

        /// <summary>
        ///     Is this category visible
        /// </summary>
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                SetVisibleInternal(value);
            }
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

        /// <summary>
        /// Get category color
        /// </summary>
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                fillColor = value;
                fillColor.a = 0.5f;
                SetColorInternal(value);
            }
        }

        private Color fillColor;
        
        /// <summary>
        /// Get category fill color. Usually same as color but more transparent
        /// </summary>
        public Color FillColor => fillColor;

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

        
        /// <summary>
        ///     Get color in case if this object in given layer
        /// </summary>
        /// <param name="gameObjectLayer"></param>
        /// <returns></returns>
        public Color GetLineColor(int gameObjectLayer)
        {
            if (ArtLayers.applyLayersColors)
            {
                var layer = ArtLayers.GetLayer(gameObjectLayer);
                return layer.Color;
            }
            return Color;
        }

        
        /// <summary>
        ///     Get fill color if the object is in given layer
        /// </summary>
        /// <param name="gameObjectLayer"></param>
        /// <returns></returns>
        public Color GetFillColor(int gameObjectLayer)
        {
            if (ArtLayers.applyLayersColors)
            {
                var layer = ArtLayers.GetLayer(gameObjectLayer);
                return layer.FillColor;
            }
            return FillColor;
        }
    }
}