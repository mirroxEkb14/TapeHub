using TapeHubDemo.View;

namespace TapeHubDemo;

//
// Summary:
//     Is the code-behind file for MainPage.xaml.
//     Defines behavior, event handlers, and interaction logic
//         (handling button clicks or updating UI elements based on user interactions).
//     «ContentPage» - is a base class that represents a single page of content, commonly
//         used for pages with a single layout.
//     The «partial» keyword indicates that this class definition is split across multiple
//         files, which is typically used with XAML pages, where the layout is defined in
//         the .xaml file, and the code-behind is defined in the .xaml.cs file.
public partial class MainPage : ContentPage
{
    //
    // Summary:
    //     Initializes the UI elements defined in the associated .xaml file.
    //     Loads the XAML-defined layout and binds the UI elements in the XAML to fields
    //         in the code-behind file, allowing interaction between code and UI.
    public MainPage()
    {
        InitializeComponent();
        StartPuzzleAnimation();
    }

    //
    // Summary:
    //     Starts the puzzle animation by animating the logo pieces and showing the slogan at the same time.
    //     The typewriter effect lasts for 2.1 seconds.
    //     Starts the typewriter effect and piece animations in parallel.
    //     Waits for the typewriter effect to complete.
    //     Navigates to the LoginPage after the animation is done with the optional delay for a smooth transition.
    private async Task StartPuzzleAnimation()
    {
        var typewriterTask = ShowSloganWithTypewriterEffect(SloganLabel, "Discover the Classics");
        await AnimatePiecesSequentially();
        await typewriterTask;

        await Task.Delay(500);
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    #region Logo Animations
    //
    // Summary:
    //     Animates the logo pieces one by one: top-left piece, top-right, bottom-left, bottom-right.
    //     Initializes (i) duration for each piece animation and (ii) delay between each piece animation.
    //     Animates each piece sequentially with a delay.
    private async Task AnimatePiecesSequentially()
    {
        uint pieceAnimationDuration = 500;
        int delayBetweenPieces = 400;

        await AnimatePiece(TopLeftPiece, -50, -50, pieceAnimationDuration);
        await Task.Delay(delayBetweenPieces);

        await AnimatePiece(TopRightPiece, 50, -50, pieceAnimationDuration);
        await Task.Delay(delayBetweenPieces);

        await AnimatePiece(BottomLeftPiece, -50, 50, pieceAnimationDuration);
        await Task.Delay(delayBetweenPieces);

        await AnimatePiece(BottomRightPiece, 50, 50, pieceAnimationDuration);
    }

    //
    // Summary:
    //     Animates the logo piece with a fade-in effect and translation.
    //     Initializes initial position for the animation.
    //     Animates fade-in and translation.
    private static async Task AnimatePiece(Image piece, double initialX, double initialY, uint duration)
    {
        piece.TranslationX = initialX;
        piece.TranslationY = initialY;

        await Task.WhenAll(
            piece.FadeTo(1, duration, Easing.SpringOut),
            piece.TranslateTo(0, 0, duration, Easing.SpringOut)
        );
    }
    #endregion

    #region Slogan Animations
    //
    // Summary:
    //     Animates the text of the slogan label to appear as if it is being typed (typewriter effect).
    //     Adjusts the delay for typing speed.
    private static async Task ShowSloganWithTypewriterEffect(Label label, string text)
    {
        label.Text = string.Empty;
        label.Opacity = 1;
        foreach (char c in text)
        {
            label.Text += c;
            await Task.Delay(100);
        }
    }

    //
    // Summary:
    //     Uses a small delay before showing the slogan.
    //     Shows the slogan with a fade-in effect.
    private async void ShowSloganWithFadeInEffect()
    {
        await Task.Delay(300);
        await SloganLabel.FadeTo(1, 1000);
    }
    #endregion
}
