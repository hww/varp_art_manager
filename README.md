# VARP Visibility Editor

Simple Unity editor extension for managing visibility of layers and categories of objects. 

## ArtPrimitive Class

The example of ArtPrimitive class below. This class associate the game object with one of art groups and categories.

```C#
public class ArtPrimitive : MonoBehaviour
{
    public EArtGroup artGroup;        // Select the art group of this object
    public EArtCategory artCategory;  // Select the art category of this object

    public ArtGroup GetArtGroup()
    {
        return ArtGroups.GetGroup(artGroup);
    }
    
    public ArtCategory GetArtCategory()
    {
        return ArtGroups.GetGroup(artGroup).GetCategory(artCategory);
    }
}
```

![Art Primitive Component](/Documentation/art-primitive.png)

## Edit Layers Visibility

The pannel alow makes visible or invisible layers, also it can make layer protected or not. Additionaly it allow to change layer's color. And finaly it displays metrics per layer.

![Layers Window](/Documentation/layers_window.png)

## Edit Categories Visibility

The pannel alow makes visible or invisible category (or group of categories). Additionaly it displays metrics per category.

![Categories Window](/Documentation/categories_window.png)

## Change Layer Names

The enum value EGameLayer contains the names for all layers in your game.

## Access to categories settings

For each group static field in the GameGroups class.

```C#
public class ArtGroups
{
      public static Group Camera;
      public static Group Partiles;
      public static Group Sounds;
      public static Group Globals;
      public static Group Rendering;
      public static Group Gameplay;
}
```

Each group has fields per each category.

```C#
public class ArtGroup
{
      public Category ActorsSpawners;
      public Category Regions;
      public Category Splines;
      public Category FeatureOverlays;
      public Category NavShapes;
      public Category Traversal;
}
```

Example of using

```C#
private void OnDrawGizmos()
{
    var category = GetArtCategory();
    if (category.IsVisible)
    {
        var lineColor = category.GetLineColor(gameObject.layer);
        var fillColor = category.GetFillColor(gameObject.layer);
        VarpGizmos.Cylinder3D(transform.position, transform.rotation, 1f, zoneRadius, GizmoDrawAxis.Y, fillColor, lineColor);
        VarpGizmos.Label(transform.position, lineColor, LabelPivot.MIDDLE_CENTER, LabelAlignment.CENTER, name, 100);
    }
}
```

The line and fill colors will be used from category or from layers panel, depends on checkbox "User Layer Colors"

![Layers Window](/Documentation/layers_window_colors.png)

## Access to layer settings

The ArtLayers class contains settings for layers

```C#
public class ArtGroup
{
      public static readonly ArtLayer[] Layers = new ArtLayer[32];
}
```

In most cases there are no resons access to the layes settings. The layers managed directly by unity UnityEditor. 


