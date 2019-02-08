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
    /// Settings for single category
    /// </summary>
    public class ArtCategory
    {
        public readonly EArtCategory artCategory;
        public readonly bool isOptional;
        private readonly string visiblePreferenceName;
        private readonly string colorPreferenceNameR;
        private readonly string colorPreferenceNameG;
        private readonly string colorPreferenceNameB;

        public ArtCategory(EArtGroup group, EArtCategory category, Color defaultColor, bool optional)
        {
            isOptional = optional;
            artCategory = category;
            var artGroupName = group.ToString();
            var categoryName = category.ToString();
            visiblePreferenceName = $"CategoriesWindowVisible{artGroupName}{categoryName}";
            colorPreferenceNameR = $"CategoriesWindowColorR{artGroupName}{categoryName}";
            colorPreferenceNameG = $"CategoriesWindowColorG{artGroupName}{categoryName}";
            colorPreferenceNameB = $"CategoriesWindowColorB{artGroupName}{categoryName}";
            isVisible = GetVisibleInternal(true);
            color = GetColorInternal(defaultColor);
        }
        
        private bool isVisible;

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

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                colorTransparent = value;
                colorTransparent.a = 0.5f;
                SetColorInternal(value);
            }
        }

        private Color colorTransparent;
        public Color ColorTransparent => colorTransparent;

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