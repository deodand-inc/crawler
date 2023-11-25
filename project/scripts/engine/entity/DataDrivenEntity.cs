using Godot;

namespace crawler.scripts.engine.entity;

public class DataDrivenEntity : Entity
{
    public record SpriteData(Vector2 SpriteCoordinates, string SpritePath);

    public readonly SpriteData Sprite;
    
    public DataDrivenEntity(Vector2 spriteCoordinates, string spritePath)
    {
        this.Sprite = new SpriteData(spriteCoordinates, spritePath);
    }
}