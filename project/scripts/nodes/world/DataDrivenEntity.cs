using Godot;
using System;
using crawler.scripts.engine.entity;

namespace crawler.scripts.nodes.world;

public partial class DataDrivenEntity : Area2D
{
	private Entity _entity;
	
	public DataDrivenEntity(Entity entity)
	{
		_entity = entity;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
