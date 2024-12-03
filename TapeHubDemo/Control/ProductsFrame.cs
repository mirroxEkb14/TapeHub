namespace TapeHubDemo.Control;

//
// Summary:
//     Represents a custom «Frame» element that displays an item (product) with an animation.
public partial class ProductsFrame : Frame
{
    //
    // Summary:
    //     Initializes a «TapGestureRecognizer» to trigger animations.
    //     Animates the frame when tapped (shrink and expand).
    public ProductsFrame()
    {
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(async () =>
            {
                await this.ScaleTo(0.95, 100);
                await this.ScaleTo(1.0, 100);
            })
        });
    }
}
