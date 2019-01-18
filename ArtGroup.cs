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
using UnityEngine;


namespace VARP.VisibilityEditor
{
    /// <summary>
    /// Settings for single group. Each group contains several categories
    /// </summary>
    public class ArtGroup
    {
        public string Name;
        
        public readonly List<ArtCategory> Categories = new List<ArtCategory>();
        
        public ArtCategory ActorsSpawners;
        public ArtCategory Regions;
        public ArtCategory Splines;
        public ArtCategory FeatureOverlays;
        public ArtCategory NavShapes;
        public ArtCategory Traversal;

        public ArtGroup(string groupName)
        {
            Name = groupName;
        }
                
        public void CreateCategory(string categoryName, Color defaultColor, ref ArtCategory artCategory)
        {
            artCategory = new ArtCategory(Name, categoryName, defaultColor);
            Categories.Add(artCategory);
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
}