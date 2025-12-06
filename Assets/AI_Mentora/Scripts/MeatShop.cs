namespace AIMentora
{
    public class MeatShop : InteractiveObject
    {
        public override void InteractedWith()
        {
            UIHandler.OpenMeatShop();
        }
    }
}
