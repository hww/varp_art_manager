using System;
using System.Collections.Generic;
using UnityEngine;
using VARP.VisibilityEditor;

namespace Plugins.VARP.VisibilityEditor
{
    public static class GameLayers
    {
        public static readonly GameLayer[] Layers = new GameLayer[32];
        
        static GameLayers()
        {
            // -- initialize all layers --
            var layersValues = System.Enum.GetValues(typeof(EGameLayer));
            foreach (var layer in layersValues)
                Layers[(int)layer] = new GameLayer((int)layer, ((EGameLayer)layer).ToString(), Color.white);
        }

        public static GameLayer GetLayer(EGameLayer gameLayer)
        {
            return Layers[(int) gameLayer];
        }
        
        public static void CountObjects()
        {
            if (Layers == null) return;
            var counts = LayerTools.CountObjectsInAllLayers();
            var min = Math.Min(counts.Length, Layers.Length);
            for (var i = 0; i < min; i++)
            {
                var layer = Layers[i];
                if (layer != null)
                    layer.Quantity = counts[i];
            }
        }
    }
}