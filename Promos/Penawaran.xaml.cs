using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Promos.Controller;
using Promos.Model;

namespace Promos
{
    /// <summary>
    /// Interaction logic for Penawaran.xaml
    /// </summary>

    public partial class Penawaran : Window
    {
        PenawaranController Penawarancontroller;
        OnPenawaranChangedListener listener;
        public Penawaran()
        {
            InitializeComponent();

            Penawarancontroller = new PenawaranController();
            listPenawaran.ItemsSource = Penawarancontroller.getItems();

            generateContentPenawaran();

        }

        public void SetOnItemSelectedListener(OnPenawaranChangedListener listener)
        {
            this.listener = listener;
        }

        private void generateContentPenawaran()
        {
            Item coffeLate = new Item("Coffe Late", 15000);
            Item greenTea = new Item("Green tea", 10000);
            Item milkShake = new Item("Milk Shake", 12000);
            Item alpukatJuice = new Item("Alpukat Juice", 15000);
            Item lemonSquash = new Item("Lemon Squash", 15000);
            Item chickenwings = new Item("Chicken wings (8 pcs)", 35000);
            Item friedRice = new Item("Fried Rice Special", 20000);

            Penawarancontroller.addItem(coffeLate);
            Penawarancontroller.addItem(greenTea);
            Penawarancontroller.addItem(milkShake);
            Penawarancontroller.addItem(alpukatJuice);
            Penawarancontroller.addItem(lemonSquash);
            Penawarancontroller.addItem(chickenwings);
            Penawarancontroller.addItem(friedRice);

            listPenawaran.Items.Refresh();
        }

        private void listPenawaran_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            Item item = listbox.SelectedItem as Item;

            this.listener.onPenawaranSelected(item);
        }
    }

    public interface OnPenawaranChangedListener
    {
        void onPenawaranSelected(Item item);
    }
}
