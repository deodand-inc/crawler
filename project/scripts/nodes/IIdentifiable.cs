using System;

namespace crawler.scripts.nodes;

public interface IIdentifiable
{
    public Guid GetId();
}