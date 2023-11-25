namespace crawler.scripts.engine.events;

public class DamageEvent : Event
{
    public int amount;

    public DamageEvent(int amount)
    {
        this.amount = amount;
    }
}