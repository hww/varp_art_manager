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
    public static class LayersTools
    {
        // ====================================================================
        // Statistics
        // ====================================================================

        /// <summary>
        ///     Count objects on all layers 
        /// </summary>
        /// <returns></returns>
        public static int[] CountObjectsInAllLayers()
        {
            var counts = new int[32];
            var root = Resources.FindObjectsOfTypeAll<GameObject>();
            CountObjectsInAllLayers(root, counts);
            return counts;
        }

        /// <summary>
        ///     Count objects on all layers, but start this given roots
        /// </summary>
        /// <param name="root"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        private static int CountObjectsInAllLayers(GameObject[] root, int[] counts)
        {
            var count = 0;
            foreach (var t in root)
                if (t.hideFlags == HideFlags.None)
                    counts[t.layer]++;
            return count;
        }

        // ====================================================================
        // Selecting
        // ====================================================================

        private static void SelectObjectsInLayer(int layerIndex)
        {
            var root = Resources.FindObjectsOfTypeAll<GameObject>();
            SelectObjectsInLayer(root, layerIndex);
        }

        private static void SelectObjectsInLayer(GameObject[] root, int layerIndex)
        {
            var Selected = new List<GameObject>();
            foreach (var t in root)
                if (t.layer == layerIndex && t.hideFlags == HideFlags.None)
                    Selected.Add(t);
            Selection.objects = Selected.ToArray();
        }

        private static void SelectObjectsByLayerMask(int layerMask)
        {
            var root = Resources.FindObjectsOfTypeAll<GameObject>();
            SelectObjectsByLayerMask(root, layerMask);
        }

        private static void SelectObjectsByLayerMask(GameObject[] root, int layerMask)
        {
            var Selected = new List<GameObject>();
            foreach (var t in root)
                if (t.hideFlags == HideFlags.None && ((1 << t.layer) & layerMask) > 0)
                    Selected.Add(t);
            Selection.objects = Selected.ToArray();
        }
    }
}