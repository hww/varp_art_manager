# VARP Visibility Editor

Simple Unity editor extension for managing visibility of layers and categories of objects.

## Edit Layers Visibility

![Layers Window](/Documentation/layers_window.png)

## Edit Categories Visibility

![Categories Window](/Documentation/categories_window.png)

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



