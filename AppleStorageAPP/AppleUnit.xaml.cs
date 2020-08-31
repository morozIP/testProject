using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace AppleStorage
{
    /// <summary>
    /// Interaction logic for AppleUnit.xaml
    /// </summary>
    public partial class AppleUnit : UserControl
    {
        object Type { get; set; }
        object Model { get; set; }
        object Info { get; set; }
        AppleProduct appleProduct { get; set; }
        MainWindow mainWindow;

        public AppleUnit()
        {
            InitializeComponent();
           
        }

        public AppleUnit(AppleProduct _appleProduct, MainWindow _mainWindow)
        {            
            InitializeComponent();
            mainWindow = _mainWindow;
            appleProduct = _appleProduct;
            Type = _appleProduct.Type;
            Model = _appleProduct.Model;
            Info = _appleProduct.Info;

            TypeL.Content = Type;
            ModelL.Content = Model;
            InfoL.Content = Info;
            var path = Path.Combine(Environment.CurrentDirectory, "img", Type+".png");
            var uri = new Uri(path);
            var bitmap = new BitmapImage(uri);
            ModelIMG.Source = bitmap;
        }       

        private void Button_MouseRightButtonUp_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            myContextMenu.IsOpen = true;
        }
            
        private void DeletMe(object sender, RoutedEventArgs e)
        {
            mainWindow.NeedDelete(appleProduct.Id);
                
        }

        private void Show_info(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(""+Info);
        }

        #region MyRegion
        //
        private bool _isMoving;
        private Point? _unitPos;
        private double deltaX;
        private double deltaY;
        private TranslateTransform _currentTT;

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_unitPos == null)
                _unitPos = this.TransformToAncestor(mainWindow.ListUG).Transform(new Point(0, 0));
            var mousePosition = Mouse.GetPosition(mainWindow.ListUG);
            deltaX = mousePosition.X - _unitPos.Value.X;
            deltaY = mousePosition.Y - _unitPos.Value.Y;
            _isMoving = true;
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentTT = this.RenderTransform as TranslateTransform;
            _isMoving = false;

        }

        private void Button_PreviewMouseMove_1(object sender, MouseEventArgs e)
        {
            if (!_isMoving) return;

            var mousePoint = Mouse.GetPosition(mainWindow.ListUG);

            var offsetX = (_currentTT == null ? _unitPos.Value.X : _unitPos.Value.X - _currentTT.X) + deltaX - mousePoint.X;
            var offsetY = (_currentTT == null ? _unitPos.Value.Y : _unitPos.Value.Y - _currentTT.Y) + deltaY - mousePoint.Y;

            this.RenderTransform = new TranslateTransform(-offsetX, -offsetY);
        }

        //
        #endregion


    }
}
