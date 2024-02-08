using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;


namespace Clock
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Clk clk;
        double screenHeight = SystemParameters.FullPrimaryScreenHeight;
        double screenWidth = SystemParameters.FullPrimaryScreenWidth;
        NotifyIcon ni = new NotifyIcon();
        public MainWindow()
        {
            InitializeComponent();
            clk = new Clk();
            clk.Show();
            cmbColor.ItemsSource = typeof(Colors).GetProperties();
            cmbFontSize.ItemsSource = Enumerable.Range(10, 31).ToArray();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies;           
            ni.Icon = new Icon("Clock.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    clk.txt.IsEnabled = true;
                    clk.ccp.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.01 };
                };
            ni.ContextMenuStrip = new ContextMenuStrip();
            ni.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Settings", null, delegate { this.Show(); clk.ccp.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.01 }; clk.txt.IsEnabled = true; }));
            ni.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit",null,delegate { this.Close(); }));
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            clk.ccp.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("Transparent");
            clk.txt.IsEnabled = false;
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontSize = cmbFontSize.SelectedItem.ToString();
            clk.FontSize = Convert.ToDouble(fontSize);
        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Media.Color selectedColor = (System.Windows.Media.Color)(cmbColor.SelectedItem as PropertyInfo).GetValue(null, null);
            clk.Foreground = new SolidColorBrush(selectedColor);
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Media.FontFamily fontFamily = (System.Windows.Media.FontFamily)cmbFontFamily.SelectedItem;
            clk.FontFamily = new System.Windows.Media.FontFamily(fontFamily.ToString());
        }

        private void Top_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight / screenHeight;
            clk.Left = screenWidth / 2 - clk.Width / 2;
            clk.txt.VerticalAlignment = VerticalAlignment.Top;
        }

        private void Left_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight / 2;
            clk.Left = screenWidth / screenWidth;
            clk.txt.VerticalAlignment = VerticalAlignment.Center;
        }

        private void Bottom_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight + clk.Height / 2;
            clk.Left = screenWidth / 2 - clk.Width / 2;
            clk.txt.VerticalAlignment = VerticalAlignment.Bottom;
        }

        private void Right_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight / 2;
            clk.Left = screenWidth - clk.Width;
            clk.txt.VerticalAlignment = VerticalAlignment.Center;
        }

        private void T_L_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight / screenHeight;
            clk.Left = screenWidth / screenWidth;
            clk.txt.VerticalAlignment = VerticalAlignment.Top;
        }

        private void T_R_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight / screenHeight;
            clk.Left = screenWidth - clk.Width;
            clk.txt.VerticalAlignment = VerticalAlignment.Top;
        }

        private void B_L_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight + clk.Height / 2;
            clk.Left = screenWidth / screenWidth;
            clk.txt.VerticalAlignment = VerticalAlignment.Bottom;
        }

        private void B_R_Checked(object sender, RoutedEventArgs e)
        {
            clk.Top = screenHeight + clk.Height / 2;
            clk.Left = screenWidth - clk.Width;
            clk.txt.VerticalAlignment = VerticalAlignment.Bottom;
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            clk.Owner = this;
        }

        private void Scrl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            clk.txt.Opacity = 1 - Scrl.Value;
        }

        private void main_Closed(object sender, EventArgs e)
        {
            ni.Dispose();
        }
    }
}
