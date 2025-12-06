namespace AIMentora
{
    public class MarketStall : InteractiveObject
    {
        public override void InteractedWith()
        {
            UIHandler.OpenMarket();
        }
    }
}
