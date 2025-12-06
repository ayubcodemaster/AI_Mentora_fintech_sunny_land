namespace HappyHarvest
{
    public class DairyShop : InteractiveObject
    {
        public override void InteractedWith()
        {
            UIHandler.OpenDairyShop();
        }
    }
}
