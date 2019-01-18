using VARP.JSON;

namespace Plugins.VARP.VisibilityEditor
{
    /// <summary>
    /// This class allow to mark object as the one on art category
    /// </summary>
    public class ArtBehaviour : JsonBehaviour
    {
        /// <summary>
        /// Select the art group of this object
        /// </summary>
        public EArtGroup ArtGroup;
        /// <summary>
        /// Some additional information can be added to this tags field
        /// </summary>
        public string DynamicTags;
    }
}