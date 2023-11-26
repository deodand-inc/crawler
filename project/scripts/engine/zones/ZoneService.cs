using System;
using System.Collections.Generic;
using crawler.scripts.engine.entity;
using crawler.scripts.nodes;
using crawler.scripts.utils;
using Godot;
using Godot.Collections;
using crawler.scripts.utils.extensions;

namespace crawler.scripts.engine.zones;

public class ZoneService
{
    private static ZoneService _instance = null;
    private readonly Godot.Collections.Dictionary<string, Zone> _zones = new(); 
    private readonly StringName _startingScene = new("TestDungeonRoom");
    public ActiveZone CurrentZone = null;

    private ZoneService()
    {
        const string path = "res://scenes/zones";
        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (dir.CurrentIsDir())
                {
                    GD.Print($"Found directory: {fileName}");
                }
                else
                {
                    var sanitisedFileName = fileName.Replace(".tscn", "");
                    _zones.Add(sanitisedFileName, new Zone(new StringName(path + "/" + fileName)));
                }
                fileName = dir.GetNext();
            }
        }
    }

    public static ZoneService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ZoneService();
            }
            return _instance;
        }
    }

    public Zone GetStartingZone()
    {
        return _zones.GetOrThrow(_startingScene, $"Couldn't load starting scene ${_startingScene}");
    }

    public void MovePlayerToZone(StringName zoneName)
    {
        MovePlayerToZone(_zones.GetOrThrow(zoneName), Constants.VectorNotSet);
    }

    public void MovePlayerToZone(StringName zoneName, Vector2 atPosition)
    {
        MovePlayerToZone(_zones.GetOrThrow(zoneName), atPosition);
    }

    public void MovePlayerToZone(Zone zone)
    {
        MovePlayerToZone(zone, Constants.VectorNotSet);
    }

    public void MovePlayerToZone(Zone zone, Vector2 atPosition)
    {
        TryRemoveCurrentZone();
        CurrentZone = ActiveZone.MakeActive(zone);
        Vector2 finalPosition;
        if (atPosition == Constants.VectorNotSet)
        {
            finalPosition = CurrentZone.StartPosition;
        }
        else
        {
            finalPosition = atPosition;
        }
        AddCurrentZoneToTree(finalPosition);
    }

    public void MovePlayerToZone(ZoneLocation zoneLocation)
    {
        TryRemoveCurrentZone();
        Zone zone = _zones.GetOrThrow(zoneLocation.ZoneName);
        CurrentZone = ActiveZone.MakeActive(zone);
        Node2D target = CurrentZone.Map.GetNode<Node2D>($"%{zoneLocation.UniqueName}");
        if (target is null)
        {
            throw new Exception($"No matching ID for zone location {zoneLocation.ZoneName}->%{zoneLocation.UniqueName}");
        }

        AddCurrentZoneToTree(target.Position);
    }

    private void AddCurrentZoneToTree(Vector2 playerPosition)
    {
        Game.Instance.Player.Position = playerPosition;
        CurrentZone.Map.AddChild(PlayerScene.Instance);
        GameView.Instance.AddChild(CurrentZone.Map);
    }

    private void TryRemoveCurrentZone()
    {
        if (CurrentZone is not null)
        {
            CurrentZone.Map.RemoveChild(PlayerScene.Instance);
            GameView.Instance.RemoveChild(CurrentZone.Map);
        }
    }
}