namespace AIMentora
{
    /// <summary>
    /// Interactive shop that displays dairy products when the player interacts with it.
    /// Attach this to a GameObject with a Collider2D trigger.
    /// </summary>
    public class DairyShop : InteractiveObject
    {
        /// <summary>
        /// Called when the player interacts with this shop.
        /// Opens the dairy shop UI popup.
        /// </summary>
        public override void InteractedWith()
        {
            UIHandler.OpenDairyShop();
        }
    }
}
