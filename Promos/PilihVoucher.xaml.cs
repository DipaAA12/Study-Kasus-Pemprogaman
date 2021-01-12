using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Promos
{
    /// <summary>
    /// Interaction logic for PilihVoucher.xaml
    /// </summary>
    public partial class PilihVoucher : Window
    {
        Controller.VoucherController voucherController;
        OnPilihVoucherChangedListener listener;

        public PilihVoucher()
        {
            InitializeComponent();

            voucherController = new Controller.VoucherController();
            DaftarVoucher.ItemsSource = voucherController.getItems();

            generateListVoucher();
        }

        private void generateListVoucher()
        {
            Model.Voucher seninspesial = new Model.Voucher(title: "Promo Senin Spesial 25%", discInPercent: 25);
            Model.Voucher penggunabaru = new Model.Voucher(title: "Promo Pengguna Baru 50%", discInPercent: 50);
            Model.Voucher promoNatal = new Model.Voucher(title: "Promo Natal Potongan 10000", disc: 10000);

            voucherController.addItem(seninspesial);
            voucherController.addItem(penggunabaru);
            voucherController.addItem(promoNatal);

            DaftarVoucher.Items.Refresh();
        }

        public void SetOnItemSelectedListener(OnPilihVoucherChangedListener listener)
        {
            this.listener = listener;
        }

        public interface OnPilihVoucherChangedListener
        {
            void OnPilihVoucherChangedListener(Model.Voucher item);
        }

        private void DaftarVoucherSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            Model.Voucher item = listbox.SelectedItem as Model.Voucher;

            this.listener.OnPilihVoucherChangedListener(item);
        }
    }
}
