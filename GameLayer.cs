using UnityEditor;
using UnityEngine;

namespace VARP.VisibilityEditor
{
    /// <summary>
    /// Representation for single layer
    /// </summary>
    public class GameLayer
    {
        public int Index;
        public int Mask;
        public string Name;

        public int Quantity;

        private readonly string colorPreferenceNameR;
        private readonly string colorPreferenceNameG;
        private readonly string colorPreferenceNameB;

        public GameLayer(int index, string name, Color defaultColor)
        {
            Name = name;
            Index = index;
            Mask = 1 << index;
            colorPreferenceNameR = "LayersWindowColorR" + name;
            colorPreferenceNameG = "LayersWindowColorG" + name;
            colorPreferenceNameB = "LayersWindowColorB" + name;
            Color = GetColorInternal(defaultColor);
        }

        public bool IsLocked
        {
            get => (Tools.lockedLayers & Mask) > 0;
            set
            {
                if (value)
                    Tools.lockedLayers |= Mask;
                else
                    Tools.lockedLayers &= ~Mask;
            }
        }

        public bool IsVisible
        {
            get => (Tools.visibleLayers & Mask) > 0;
            set
            {
                var was = Tools.visibleLayers;

                if (value)
                    Tools.visibleLayers |= Mask;
                else
                    Tools.visibleLayers &= ~Mask;
                if (was != Tools.visibleLayers)
                    SceneView.RepaintAll();
            }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set
            {
                if (color != value) SetColorInternal(value);
                color = value;
            }
        }

        private void SetColorInternal(Color value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetFloat(colorPreferenceNameR, value.r);
            EditorPrefs.SetFloat(colorPreferenceNameG, value.g);
            EditorPrefs.SetFloat(colorPreferenceNameB, value.b);
#endif
        }

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
    }
}