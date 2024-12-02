namespace TapeHubDemo.Control;

//
// Summary:
//     Represents a custom «Frame» element that displays a shop branch with an animation.
public partial class ShopBranchFrame : Frame
{
    //
    // Summary:
    //     Initializes a «TapGestureRecognizer» to trigger animations.
    //     Animates the frame when tapped (shrink and expand).
    //     Attaches the gesture recognizer to the frame.
    public ShopBranchFrame()
    {
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += async (s, e) =>
        {
            await this.ScaleTo(0.95, 100);
            await this.ScaleTo(1.0, 100);
        };
        GestureRecognizers.Add(tapGesture);
    }
}
