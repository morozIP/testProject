using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AppleStorage
{    
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            //only for test without connection with API and DB
            ListUG.Children.Add(new AppleUnit(new AppleProduct("imac","test","this is imac"), this));
            //
        }

        
        //redirect DELETE request 
        public void NeedDelete(int id)
        {
            new RequestPool().DeleteProduct(id);
            ShowUnits();
        }

        //get apple from DB
        public void ShowUnits()
        {
            ListUG.Children.Clear();
            RequestPool requestPool = new RequestPool();
            AppleUnit[] appleUnits = requestPool.GetAppleProducts(this);
            for (int i = 0; i < appleUnits.Length; i++)
            {

                ListUG.Children.Add(appleUnits[i]);

            }
        }

        //add new AppleProduct
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ModelText.Text == null|| ModelText.Text=="" & cmbox.SelectedIndex<=-1)
            {
                MessageBox.Show("you need add Type and Model");
            }
            else
            {
                ComboBoxItem itm = (ComboBoxItem)cmbox.SelectedItem;

                new RequestPool().AddAppleProduct(new AppleProduct(itm.Name, ModelText.Text, InfoText.Text), this);
                RefreshAddGrid();
                ShowUnits();
            }
            
        }

        //Show response in textBox(ResponseBox)
        public void ResponseUpdate(string response)
        {
            ResponseBox.Text = response;
        }

        //
        public void RefreshAddGrid()
        {
            cmbox.SelectedIndex = -1;
            ModelText.Text = null;
            InfoText.Text = null;
        }

        private void Sync_Click(object sender, RoutedEventArgs e)
        {
            ShowUnits();
        }

        private void textURL_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textURL.Text== "standart port used")
            {
                textURL.Text = "";
                textURL.Background = Brushes.Transparent;
            }
        }

        private void textURL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textURL.Text == "")
            {
                textURL.Text = "standart port used";
                textURL.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDA5757"));
            }
        }

        private void ListP_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LabelURL.Focus();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LabelURL.Focus();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LabelURL.Focus();
        }

        
    }
}
