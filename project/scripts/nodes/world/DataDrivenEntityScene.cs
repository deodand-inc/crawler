using Godot;
using System;
using crawler.scripts.engine.entity;
using crawler.scripts.utils;

namespace crawler.scripts.nodes.world;

public partial class DataDrivenEntityScene : Area2D
{
	private static readonly PackedScene Source = GD.Load<PackedScene>("res://scenes/world/DataDrivenEntityScene.tscn");
	
	private DataDrivenEntity _entity;

	public static DataDrivenEntityScene Instantiate(DataDrivenEntity entity)
	{
		var ret = Source.Instantiate<DataDrivenEntityScene>();
		ret._entity = entity;
		return ret;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadSprite();
	}

	private void LoadSprite()
	{ 
		var sprite = GD.Load<Texture2D>(_entity.Sprite.SpritePath);
		var spriteNode = GetNode<Sprite2D>("Sprite2D");
		spriteNode.Texture = sprite;
		spriteNode.RegionEnabled = true;
		spriteNode.RegionRect = new Rect2(_entity.Sprite.SpriteCoordinates, Constants.SixteenSquare);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
