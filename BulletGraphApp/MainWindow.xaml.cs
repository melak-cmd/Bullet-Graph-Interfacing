using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BulletGraphApp
{
    public partial class MainWindow : Window
    {
        // DependencyProperty for the ActualValue property
        public static readonly DependencyProperty ActualValueProperty =
            DependencyProperty.Register("ActualValue", typeof(double), typeof(MainWindow), new PropertyMetadata(0.0, OnActualValueChanged));

        // ActualValue property
        public double ActualValue
        {
            get { return (double)GetValue(ActualValueProperty); }
            set { SetValue(ActualValueProperty, value); }
        }

        // Animation event handler for ActualValue changes
        private static void OnActualValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (MainWindow)d;

            // Animate the actual value bar
            DoubleAnimation animation = new DoubleAnimation
            {
                To = (double)e.NewValue,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            window.ActualValueBar.BeginAnimation(Rectangle.WidthProperty, animation);
        }

        public MainWindow()
        {
            InitializeComponent();

            // Set the initial value and update it after 5 seconds for testing purposes
            ActualValue = 30;
            Task.Delay(5000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => ActualValue = 75);
            });
        }
    }
}
