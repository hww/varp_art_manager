# VARP Visibility Editor

Simple Unity editor extension for managing visibility of layers and categories of objects.

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
public class GameGroups
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
public class GameGroup
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
void OnDrawGizmos()
{
      var category = GameGroups.Gameplay.ActorSpawners;
      if (category.IsVisible)
            Gizmos.DrawBox(transform.position, Vector.one, category.Color);
}
```

## Access to layer settings

The GameLayers class contains settings for layers

```C#
public class GameGroup
{
      public static readonly GameLayer[] Layers = new GameLayer[32];
}
```

In most cases there are no resons access to the layes settings. The layers managed directly by unity UnityEditor. 


