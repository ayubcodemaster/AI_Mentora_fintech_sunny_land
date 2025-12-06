namespace AIMentora
{
    public class DairyShop : InteractiveObject
    {
        public override void InteractedWith()
        {
            UIHandler.OpenDairyShop();
        }
    }
}
