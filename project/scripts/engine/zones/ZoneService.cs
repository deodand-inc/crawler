﻿using System;
using System.Collections.Generic;
using crawler.scripts.engine.entity;
using crawler.scripts.nodes;
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
        const String path = "res://scenes/zones";
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
        MovePlayerToZone(_zones.GetOrThrow(zoneName));
    }

    public void MovePlayerToZone(Zone zone)
    {
        if (CurrentZone is not null)
        {
            CurrentZone.Map.RemoveChild(PlayerScene.Instance);
            GameView.Instance.RemoveChild(CurrentZone.Map);
        }
        CurrentZone = ActiveZone.MakeActive(zone);
        Game.Instance.Player.Position = CurrentZone.StartPosition;
        CurrentZone.Map.AddChild(PlayerScene.Instance);
        GameView.Instance.AddChild(CurrentZone.Map);
    }
}