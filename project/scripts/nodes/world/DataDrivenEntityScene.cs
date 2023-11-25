using Godot;
using System;
using crawler.scripts.engine.entity;
using crawler.scripts.engine.events;
using crawler.scripts.utils;

namespace crawler.scripts.nodes.world;

public partial class DataDrivenEntityScene : Area2D
{
	private static readonly PackedScene Source = GD.Load<PackedScene>("res://scenes/world/DataDrivenEntityScene.tscn");
	
	public DataDrivenEntity Entity { get; private set; }

	public static DataDrivenEntityScene Instantiate(DataDrivenEntity entity)
	{
		var ret = Source.Instantiate<DataDrivenEntityScene>();
		ret.Entity = entity;
		return ret;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadSprite();
	}

	private void LoadSprite()
	{ 
		var sprite = GD.Load<Texture2D>(Entity.Sprite.SpritePath);
		var spriteNode = GetNode<Sprite2D>("Sprite2D");
		spriteNode.Texture = sprite;
		spriteNode.RegionEnabled = true;
		spriteNode.RegionRect = new Rect2(Entity.Sprite.SpriteCoordinates, Constants.SixteenSquare);
	}

	public void RouteEvent(Event e)
	{
		Entity.RouteEvent(e);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
