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
using UnityEngine;

namespace VARP.VisibilityEditor
{
    public static class ArtGroups
    {
        /// <summary>
        ///     Specialized for camera management
        /// </summary>
        public static ArtGroup Camera;

        /// <summary>
        ///     Specialized for particles and effects management
        /// </summary>
        public static ArtGroup Particles;

        /// <summary>
        ///     Specialized for sounds and listeners management
        /// </summary>
        public static ArtGroup Sounds;

        /// <summary>
        ///     Not fit to any group
        /// </summary>
        public static ArtGroup Globals;

        /// <summary>
        ///     Specialized for GUI, Rendering, Lighting, and PostFX
        /// </summary>
        public static ArtGroup Rendering;

        /// <summary>
        ///     Specialized for game play behaviour
        /// </summary>
        public static ArtGroup Gameplay;

        private static readonly ArtGroup[] groups = new ArtGroup[(int) ArtGroupTag.ArtGroupsCount];
        private static bool isInitialized;

        /// <summary>
        ///     Static constructor
        /// </summary>
        static ArtGroups()
        {
            Initialize();
        }

        /// <summary>
        ///     Initialize all groups
        /// </summary>
        public static void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;

            Globals = CreateGroup(ArtGroupTag.Globals, Color.white);
            Gameplay = CreateGroup(ArtGroupTag.GamePlay, Color.green);
            Camera = CreateGroup(ArtGroupTag.Camera, Color.red);
            Sounds = CreateGroup(ArtGroupTag.Sounds, Color.blue);
            Rendering = CreateGroup(ArtGroupTag.Rendering, Color.magenta);
            Particles = CreateGroup(ArtGroupTag.Particles, Color.magenta);
        }

        private static ArtGroup CreateGroup(ArtGroupTag egroup, Color color)
        {
            var group = new ArtGroup(egroup, color);
            groups[(int) group.artGroupTag] = group;
            return group;
        }

        /// <summary>
        /// Get art group
        /// </summary>
        /// <param name="artGroupTag"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ArtGroup GetGroup(ArtGroupTag artGroupTag)
        {
            switch (artGroupTag)
            {
                case ArtGroupTag.Globals: return Globals;
                case ArtGroupTag.Camera: return Camera;
                case ArtGroupTag.Sounds: return Sounds;
                case ArtGroupTag.Rendering: return Rendering;
                case ArtGroupTag.GamePlay: return Gameplay;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}