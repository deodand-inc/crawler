using System.Collections.Generic;
using Godot;

namespace crawler.scripts.utils;

public class Constants
{
    public const int TileSize = 16;
    public const int ForegroundLayer = 0;
    public const int BackgroundLayer = 1;
    
    /* Rename this constant so it's more meaningful in context */
    public static readonly Vector2 VectorNotSet = Vector2.Inf;
    public static readonly Vector2 SixteenSquare = new Vector2(16, 16);
    public const string DefaultSpritePath = "res://tiles/Nethack-Tiles16x16_noalpha.png";
}
