using System;
using System.Collections.Generic;
using crawler.scripts.engine.entity;
using Godot;
using Godot.Collections;

namespace crawler.scripts.engine.zones;

public class ZoneService
{
    private static ZoneService _instance = null;
    private readonly Godot.Collections.Dictionary<string, Zone> _zones = new(); 

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
    
    private readonly StringName _startingScene = new StringName("TestDungeonRoom");

    private Zone _currentZone;

    public Zone CurrentZone
    {
        get
        {
            if (_currentZone is null)
            {
                _currentZone = GetStartingZone();
            }

            return _currentZone;
        }
        set => _currentZone = value;
    }

    public Zone GetStartingZone()
    {
        Zone val;
        if (!_zones.TryGetValue(_startingScene, out val))
        {
            throw new Exception("Couldn't load starting scene " + _startingScene);
        }

        return val;
    }

    public void MovePlayerToZone(Player p, Node2D zone)
    {
        Marker2D startMarker = (Marker2D) zone.GetNode("PlayerStart");
        p.Position = startMarker.Position;
    }
}