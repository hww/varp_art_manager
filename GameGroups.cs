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