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
    public static class GameGroups
    {
        public static GameGroup Camera;
        public static GameGroup Partiles;
        public static GameGroup Sounds;
        public static GameGroup Globals;
        public static GameGroup Rendering;
        public static GameGroup Gameplay;
        
        static GameGroups()
        {
            Globals = new GameGroup("Global");
            Globals.CreateCategory("FeatureOverlays", Color.white, ref Globals.FeatureOverlays);
            Globals.CreateCategory("NavShapes", Color.white, ref Globals.NavShapes);
            Globals.CreateCategory("Traversal", Color.white, ref Globals.Traversal);

            Gameplay = new GameGroup("Gameplay");
            Gameplay.CreateCategory("ActorsSpawners", Color.white, ref Gameplay.ActorsSpawners);
            Gameplay.CreateCategory("Regions", Color.white, ref Gameplay.Regions);
            Gameplay.CreateCategory("Splines", Color.white, ref Gameplay.Splines);

            Camera = new GameGroup("Camera");
            Camera.CreateCategory("ActorsSpawners", Color.white, ref Camera.ActorsSpawners);
            Camera.CreateCategory("Regions", Color.white, ref Camera.Regions);
            Camera.CreateCategory("Splines", Color.white, ref Camera.Splines);

            Sounds = new GameGroup("Sounds");
            Sounds.CreateCategory("ActorsSpawners", Color.white, ref Sounds.ActorsSpawners);
            Sounds.CreateCategory("Regions", Color.white, ref Sounds.Regions);
            Sounds.CreateCategory("Splines", Color.white, ref Sounds.Splines);

            Rendering = new GameGroup("Rendering");
            Rendering.CreateCategory("ActorsSpawners", Color.white, ref Rendering.ActorsSpawners);
            Rendering.CreateCategory("Regions", Color.white, ref Rendering.Regions);

            Partiles = new GameGroup("Particles");
            Partiles.CreateCategory("ActorsSpawners", Color.white, ref Partiles.ActorsSpawners);
            Partiles.CreateCategory("Regions", Color.white, ref Partiles.Regions);
            Partiles.CreateCategory("Splines", Color.white, ref Partiles.Splines);
        }
    }
}