namespace crawler.scripts.engine.events;

public class DamageEvent : Event
{
    public int Amount;

    public DamageEvent(int amount)
    {
        this.Amount = amount;
    }
}