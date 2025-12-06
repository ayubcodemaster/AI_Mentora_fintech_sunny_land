namespace AIMentora
{
    /// <summary>
    /// Interactive shop that displays meat products when the player interacts with it.
    /// Attach this to a GameObject with a Collider2D trigger.
    /// </summary>
    public class MeatShop : InteractiveObject
    {
        /// <summary>
        /// Called when the player interacts with this shop.
        /// Opens the meat shop UI popup.
        /// </summary>
        public override void InteractedWith()
        {
            UIHandler.OpenMeatShop();
        }
    }
}
