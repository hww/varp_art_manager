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
    public static class ArtGroups
    {
        /// <summary>
        /// Specialized for camera management
        /// </summary>
        public static ArtGroup Camera;
        /// <summary>
        /// Specialized for particles and effects management
        /// </summary>
        public static ArtGroup Particles;
        /// <summary>
        /// Specialized for sounds and listeners management
        /// </summary>
        public static ArtGroup Sounds;
        /// <summary>
        /// Not fit to any group
        /// </summary>
        public static ArtGroup Globals;
        /// <summary>
        /// Specialized for GUI, Rendering, Lighting, and PostFX 
        /// </summary>
        public static ArtGroup Rendering;
        /// <summary>
        /// Specialized for game play behaviour
        /// </summary>
        public static ArtGroup Gameplay;
        
        static ArtGroups()
        {
            Globals = new ArtGroup("Global");
            Globals.CreateCategory("FeatureOverlays", Color.white, ref Globals.FeatureOverlays);
            Globals.CreateCategory("NavShapes", Color.white, ref Globals.NavShapes);
            Globals.CreateCategory("Traversal", Color.white, ref Globals.Traversal);

            Gameplay = new ArtGroup("Gameplay");
            Gameplay.CreateCategory("ActorsSpawners", Color.white, ref Gameplay.ActorsSpawners);
            Gameplay.CreateCategory("Regions", Color.white, ref Gameplay.Regions);
            Gameplay.CreateCategory("Splines", Color.white, ref Gameplay.Splines);

            Camera = new ArtGroup("Camera");
            Camera.CreateCategory("ActorsSpawners", Color.white, ref Camera.ActorsSpawners);
            Camera.CreateCategory("Regions", Color.white, ref Camera.Regions);
            Camera.CreateCategory("Splines", Color.white, ref Camera.Splines);

            Sounds = new ArtGroup("Sounds");
            Sounds.CreateCategory("ActorsSpawners", Color.white, ref Sounds.ActorsSpawners);
            Sounds.CreateCategory("Regions", Color.white, ref Sounds.Regions);
            Sounds.CreateCategory("Splines", Color.white, ref Sounds.Splines);

            Rendering = new ArtGroup("Rendering");
            Rendering.CreateCategory("ActorsSpawners", Color.white, ref Rendering.ActorsSpawners);
            Rendering.CreateCategory("Regions", Color.white, ref Rendering.Regions);

            Particles = new ArtGroup("Particles");
            Particles.CreateCategory("ActorsSpawners", Color.white, ref Particles.ActorsSpawners);
            Particles.CreateCategory("Regions", Color.white, ref Particles.Regions);
            Particles.CreateCategory("Splines", Color.white, ref Particles.Splines);
        }

    }
}