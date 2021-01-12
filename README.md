# Dipa August Almadima
# 19.11.2806

# Studi Kasus Pemprogaman
Aplikasi ini mensimulasi pembelian makanan dan minuman dengan menggunakan promo/voucher.

# Scope and Functionalities
- User dapat melihat daftar makanan dan minuman yang tersedia
- User dapat melihat voucher yang di tawarkan
- User dapat menggunakan voucher
- User dapat melihat potongan harga setelah menggunakan salh satu voucher

# Cara Kerja Program
Dalam kasus yang diberikan saya menggunakan 4 buah model dan 3 buah controller yang bertujuan untuk 
menjalankan program tersebut.

Dimulai dari `Penawaran.xaml.cs` yg bertujuan untuk menawarkan sebuah list makanan yang bisa menggunakan sebuah voucher
untuk di tampilkan di listbox.
```c#
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
```
Kemudian dilanjutkan pada `Voucher.xaml.cs`, yang bertujuan untuk mengsingkronisasi voucher dan model penawaran,
yang akan di tampilkan pada listbox juga.
```c#
 private void generateListVoucher()
        {
            Model.Voucher awaltahun = new Model.Voucher(title: "Promo Awal Tahun 25%", discInPercent: 25);
            Model.Voucher penggunabaru = new Model.Voucher(title: "Promo Pengguna Baru 30%,hingga 30rb", discInPercent: 30);
            Model.Voucher promoNatal = new Model.Voucher(title: "Promo Natal Potongan 10000", disc: 10000);

            voucherController.addItem(awaltahun);
            voucherController.addItem(penggunabaru);
            voucherController.addItem(promoNatal);

            DaftarVoucher.Items.Refresh();
        }
```
Lalu pada `MainWindow.xaml.cs` kita menginisiasi object dari `Penawaran.xaml.cs` dan juga `Voucher.xaml.cs`,
untuk di masukan dalam sebuah list KeranjangBelanja. Dan tampilan Total dari semua belanja akan di tampilkan pada ListBox.
```cs
        public MainWindow()
        {
            InitializeComponent();

            payment = new Payment(this);
            payment.setBalance(500000);
            payment.setPromo(0);

            KeranjangBelanja keranjangBelanja = new KeranjangBelanja(payment, this);

            controller = new MainWindowController(keranjangBelanja);

            listBoxPesanan.ItemsSource = controller.getSelectedItems();
            listBoxPakaiVoucher.ItemsSource = controller.getSelectedVouchers();

            initializeView();

        }

        private void initializeView()
        {
            labelSubtotal.Content = 0;
            labelGrantTotal.Content = 0;
            labelPromoFee.Content = (payment.getPromo() > 0) ? - payment.getPromo() : 0;
        }

        public void onPenawaranSelected(Item item)
        {
            controller.addItem(item);
        }

        private void onButtonAddItemClicked(object sender, RoutedEventArgs e)
        {
            Penawaran penawaranWindow = new Penawaran();
            penawaranWindow.SetOnItemSelectedListener(this);
            penawaranWindow.Show();
        }

        private void listBoxPesanan_ItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Kamu ingin menghapus item ini?",
                    "Konfirmasi", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ListBox listBox = sender as ListBox;
                Item item = listBox.SelectedItem as Item;
                controller.deleteSelectedItem(item);
            }
        }

        private void listBoxPakaiVoucher_ItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Kamu ingin membatalkan voucher ini?",
                   "Konfirmasi", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ListBox listBox = sender as ListBox;
                Voucher item = listBox.SelectedItem as Voucher;
                controller.deleteSelectedVoucher(item);
            }
        }
```
