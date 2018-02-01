using System;
namespace sheepshead.Resources.models.interfaces
{
    public interface IPlayer
    {
        IHand Hand { get; set; }
        String Name { get; }

        Boolean IsPartner();
        ICard PlayCard();
        Boolean WantPick();
        // IBlind Pick(IBlind blind);

    }
}
