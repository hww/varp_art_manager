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

using UnityEngine;


namespace VARP.VisibilityEditor
{
    /// <summary>
    /// Settings for single group. Each group contains several categories
    /// </summary>
    public class ArtGroup
    {
        public EArtGroup artGroup;
        
        public readonly ArtCategory[] Categories = new ArtCategory[(int)EArtCategory.Count];

        public ArtCategory ActorsSpawners;
        public ArtCategory Regions;
        public ArtCategory Splines;
        public ArtCategory FeatureOverlays;
        public ArtCategory NavShapes;
        public ArtCategory Traversal;

        public ArtGroup(EArtGroup group, Color color)
        {
            artGroup = group;
            FeatureOverlays = CreateCategory(EArtCategory.FeatureOverlays, color, true);
            NavShapes = CreateCategory(EArtCategory.NavShapes, color, true);
            Traversal = CreateCategory(EArtCategory.Traversal, color, true);
            ActorsSpawners = CreateCategory(EArtCategory.ActorsSpawners, color, true);
            Regions = CreateCategory(EArtCategory.Regions, color, true);
            Splines = CreateCategory(EArtCategory.Splines, color, true);
        }
                
        public ArtCategory CreateCategory(EArtCategory category, Color defaultColor, bool optional = false)
        {
            return Categories[(int)category] = new ArtCategory(artGroup, category, defaultColor, optional);
        }

        public ArtCategory GetCategory(EArtCategory category)
        {
            return Categories[(int) category];
        }
        
        public bool IsVisible
        {
            get
            {
                var count = Categories.Length;
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
                var count = Categories.Length;
                for (var i = 0; i < count; i++)
                    Categories[i].IsVisible = value;
            }
        }

    }
}