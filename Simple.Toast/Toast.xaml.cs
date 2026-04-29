using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Simple.Toast;

//[ContentProperty(nameof(InnerContent))]
public partial class Toast : ContentView
{
    public enum ProgressBarDirections
    {
        Center = 1,
        Left = 2,
        Right = 3
    }

    public enum Direction
    {
        TopToDown = 1,
        LeftToRight = 2,
        DownToTop = 3,
        RightToLeft = 4,
    }

    public static readonly BindableProperty ShowProperty = BindableProperty.Create(
        nameof(Show), typeof(bool), typeof(Toast), false, BindingMode.TwoWay,
        propertyChanged: OnShowChanged);

    public bool Show
    {
        get => (bool)GetValue(ShowProperty);
        set => SetValue(ShowProperty, value);
    }

    static async void OnShowChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Toast toast && newValue is bool nw && nw)
        {
            toast.Show = false;
            await toast.ShowToast();
        }
    }

    public static readonly BindableProperty DismissDelayProperty = BindableProperty.Create(
        nameof(DismissDelay), typeof(int), typeof(Toast), 2000);
    public int DismissDelay
    {
        get => (int)GetValue(DismissDelayProperty);
        set => SetValue(DismissDelayProperty, value);
    }

    public static readonly BindableProperty ToastBackgroundColorProperty = BindableProperty.Create(
        nameof(ToastBackgroundColor), typeof(Color), typeof(Toast), Colors.Red);
    public Color ToastBackgroundColor
    {
        get => (Color)GetValue(ToastBackgroundColorProperty);
        set => SetValue(ToastBackgroundColorProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius), typeof(double), typeof(Toast), 40.0,
        propertyChanged: OnCornerRadiusChanged);
    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Toast toast && toast.Content is Border border)
        {
            var radius = (double)newValue;
            border.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(radius) };
        }
    }

    public static readonly BindableProperty ToastPaddingProperty = BindableProperty.Create(
        nameof(ToastPadding), typeof(Thickness), typeof(Toast), new Thickness(10));
    public Thickness ToastPadding
    {
        get => (Thickness)GetValue(ToastPaddingProperty);
        set => SetValue(ToastPaddingProperty, value);
    }

    public static readonly BindableProperty ToastHorizontalOptionsProperty = BindableProperty.Create(
        nameof(ToastHorizontalOptions), typeof(LayoutOptions), typeof(Toast), LayoutOptions.Center);
    public LayoutOptions ToastHorizontalOptions
    {
        get => (LayoutOptions)GetValue(ToastHorizontalOptionsProperty);
        set => SetValue(ToastHorizontalOptionsProperty, value);
    }

    public static readonly BindableProperty ToastVerticalOptionsProperty = BindableProperty.Create(
        nameof(ToastVerticalOptions), typeof(LayoutOptions), typeof(Toast), LayoutOptions.Start);
    public LayoutOptions ToastVerticalOptions
    {
        get => (LayoutOptions)GetValue(ToastVerticalOptionsProperty);
        set => SetValue(ToastVerticalOptionsProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty = BindableProperty.Create(
        nameof(BorderBrush), typeof(Color), typeof(Toast), Colors.Transparent);
    public Color BorderBrush
    {
        get => (Color)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(
        nameof(BorderThickness), typeof(Thickness), typeof(Toast), new Thickness(0));
    public Thickness BorderThickness
    {
        get => (Thickness)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    public static readonly BindableProperty ProgressBarColorProperty = BindableProperty.Create(
        nameof(ProgressBarColor), typeof(Color), typeof(Toast), Colors.Wheat);
    public Color ProgressBarColor
    {
        get => (Color)GetValue(ProgressBarColorProperty);
        set => SetValue(ProgressBarColorProperty, value);
    }

    public static readonly BindableProperty ProgressBarCornerRadiusProperty = BindableProperty.Create(
        nameof(ProgressBarCornerRadius), typeof(double), typeof(Toast), 40.0,
        propertyChanged: OnProgressBarCornerRadiusChanged);
    public double ProgressBarCornerRadius
    {
        get => (double)GetValue(ProgressBarCornerRadiusProperty);
        set => SetValue(ProgressBarCornerRadiusProperty, value);
    }

    private static void OnProgressBarCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Toast toast && toast.ProgressBar != null)
        {
            var radius = (double)newValue;
            toast.ProgressBar.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(radius) };
        }
    }

    public static readonly BindableProperty IsProgressBarEnabledProperty = BindableProperty.Create(
        nameof(IsProgressBarEnabled), typeof(bool), typeof(Toast), true);
    public bool IsProgressBarEnabled
    {
        get => (bool)GetValue(IsProgressBarEnabledProperty);
        set => SetValue(IsProgressBarEnabledProperty, value);
    }

    public static readonly BindableProperty ProgressBarHeightProperty = BindableProperty.Create(
        nameof(ProgressBarHeight), typeof(double), typeof(Toast), 3.0);
    public double ProgressBarHeight
    {
        get => (double)GetValue(ProgressBarHeightProperty);
        set => SetValue(ProgressBarHeightProperty, value);
    }

    public static readonly BindableProperty InnerContentProperty = BindableProperty.Create(nameof(InnerContent), typeof(View), typeof(Toast), default(View));
    public View InnerContent
    {
        get => (View)GetValue(InnerContentProperty);
        set => SetValue(InnerContentProperty, value);
    }

    public static readonly BindableProperty DismissProperty = BindableProperty.Create(nameof(Dismiss), typeof(bool), typeof(Toast), false, BindingMode.TwoWay, propertyChanged: OnDismissPropertyChanged);
    public bool Dismiss
    {
        get => (bool)GetValue(DismissProperty);
        set => SetValue(DismissProperty, value);
    }

    private async static void OnDismissPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Toast toast && newValue is bool nw && nw)
        {
            toast.Dismiss = false;
            await toast.SendStopAnimationsSignal();
        }
    }
    
    public static readonly BindableProperty ToastDirectionProperty = BindableProperty.Create(nameof(ToastDirection), typeof(Direction), typeof(Toast), Direction.TopToDown, propertyChanged: OnToastDirectionPropertyChanged);
    public Direction ToastDirection
    {
        get => (Direction)GetValue(ToastDirectionProperty);
        set => SetValue(ToastDirectionProperty, value);
    }

    private static void OnToastDirectionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Toast toast && newValue is Direction dir)
        {
            toast.SetInitialState(dir, false);
        }
    }

    public static readonly BindableProperty ProgressBarDirectionProperty = BindableProperty.Create(nameof(ProgressBarDirection), typeof(ProgressBarDirections), typeof(Toast), ProgressBarDirections.Center, propertyChanged: OnProgressBarDirectionPropertyChanged);

    private static void OnProgressBarDirectionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Toast toast && newValue is ProgressBarDirections dir)
        {
            toast.SetProgressBarMoveDirection(dir);
        }
    }

    public ProgressBarDirections ProgressBarDirection
    {
        get => (ProgressBarDirections)GetValue(ProgressBarDirectionProperty);
        set => SetValue(ProgressBarDirectionProperty, value);
    }

    private void SetProgressBarMoveDirection(ProgressBarDirections direction)
    {
        double anchorX = 0.5;
        switch (direction)
        {
            case ProgressBarDirections.Left:
                anchorX = 0.0;
                break;
            case ProgressBarDirections.Right:
                anchorX = 1.0;
                break;
            case ProgressBarDirections.Center:
            default:
                break;
        }
        ProgressBar.AnchorX = anchorX;
    }

    private CancellationTokenSource? _cts = null;

    private Size CurrentSize { get => this.ComputeDesiredSize(double.PositiveInfinity, double.PositiveInfinity); }

    private double BottomEdgePosition { get => CurrentSize.Height; }
    private double RightEdgePosition { get => CurrentSize.Width; }
    private double TopEdgePosition { get => -BottomEdgePosition; }
    private double LeftEdgePosition { get => -RightEdgePosition; }

    private void SetInitialState(Direction direction, bool visible)
    {
        ProgressBar.CancelAnimations();
        ProgressBar.ScaleX = 1.0;
        Opacity = 0.0;
        IsVisible = visible;
        switch (direction)
        {
            case Direction.TopToDown:
                VerticalOptions = LayoutOptions.Start;
                TranslationY = TopEdgePosition;
                break;

            case Direction.LeftToRight:
                HorizontalOptions = LayoutOptions.Start;
                TranslationX = LeftEdgePosition;
                break;
            
            case Direction.DownToTop:
                VerticalOptions = LayoutOptions.End;
                TranslationY = BottomEdgePosition;
                break;
            
            case Direction.RightToLeft:
                HorizontalOptions = LayoutOptions.End;
                TranslationX = RightEdgePosition;
                break;
        }
    }

    private async Task SendStopAnimationsSignal()
    {
        if (_cts is not null) await _cts.CancelAsync();
        _cts = new CancellationTokenSource();
        this.CancelAnimations();
        SetInitialState(ToastDirection, true);
    }

    private async Task ShowToast()
    {
        if (!MainThread.IsMainThread)
        {
            await MainThread.InvokeOnMainThreadAsync(ShowToast);
            return;
        }

        await SendStopAnimationsSignal();
        await ShowToastInternal(_cts!.Token);
    }


    private async Task PlayAppearanceAnimation(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var anim = Task.CompletedTask;
        
        var x = 0.0d;
        var y = 0.0d;
        
        switch (ToastDirection)
        {
            case Direction.TopToDown:
                y = 5;
                break;
            case Direction.LeftToRight:
                x = 5;
                break;
            case Direction.DownToTop:
                y = -5;
                break;
            case Direction.RightToLeft:
                x = -5;
                break;
        }

        anim = this.TranslateTo(x, y, 250, Easing.SpringOut);
        await Task.WhenAll(this.FadeTo(1.0, 200), anim).WaitAsync(token);
        token.ThrowIfCancellationRequested();
    }

    private async Task PlayIdleAnimation(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var progressBarAnim = IsProgressBarEnabled ? ProgressBar.ScaleXTo(0.0, Convert.ToUInt32(DismissDelay), Easing.Linear) : Task.CompletedTask;
        token.ThrowIfCancellationRequested();
        await Task.WhenAll(Task.Delay(DismissDelay), progressBarAnim)
        .WaitAsync(token);
        token.ThrowIfCancellationRequested();
        //if (IsProgressBarEnabled)
        //{
        //    token.ThrowIfCancellationRequested();
        //    await Task.WhenAll(Task.Delay(DismissDelay), 
        //                       )
        //    .WaitAsync(token);
        //    token.ThrowIfCancellationRequested();
        //}
        //else
        //{
        //    token.ThrowIfCancellationRequested();
        //    await Task.Delay(DismissDelay)
        //    .WaitAsync(token);
        //    token.ThrowIfCancellationRequested();
        //}
    }

    private async Task PlayDisappearanceAnimation(CancellationToken token)
    {

        token.ThrowIfCancellationRequested();

        var anims = new Task<bool>[2] { this.FadeTo(0.0, 250), null! };

        var x = 0.0d;
        var y = 0.0d;

        switch (ToastDirection)
        {
            case Direction.TopToDown:
                y = TopEdgePosition;
                break;
            case Direction.LeftToRight:
                x = LeftEdgePosition;
                break;
            case Direction.DownToTop:
                y = BottomEdgePosition;
                break;
            case Direction.RightToLeft:
                x = RightEdgePosition;
                break;

        }
        anims[1] = this.TranslateTo(x, y, 250, Easing.SpringIn);
        await Task.WhenAll(anims).WaitAsync(token);
        token.ThrowIfCancellationRequested();
    }

    private async Task ShowToastInternal(CancellationToken token)
    {
        try
        {
            token.ThrowIfCancellationRequested();
            await PlayAppearanceAnimation(token);

            token.ThrowIfCancellationRequested();
            await PlayIdleAnimation(token);

            token.ThrowIfCancellationRequested();
            await PlayDisappearanceAnimation(token);

            token.ThrowIfCancellationRequested();
            SetInitialState(ToastDirection, false);
        }
        catch (OperationCanceledException) { }
        finally
        {
            if (_cts?.Token == token) _cts = null;
        }
    }

    public Toast()
    {
        InitializeComponent();
        SetInitialState(ToastDirection, true);
        IsVisible = false;
        if (Content is Border border) border.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(CornerRadius) };
        if (ProgressBar != null) ProgressBar.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(ProgressBarCornerRadius) };
    }
}