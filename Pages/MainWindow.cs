using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace pbo.Pages
{
    class MainWindow : Window
    {
        private ServiceHandler serviceHandler;
        [UI] private Label _label1 = null;
        [UI] private Button _button1 = null;
        [UI] private Label _label2 = null;
        [UI] private Image bg_image = null;
        [UI] private Button _button2 = null;
        [UI] private Label no_data = null;
        [UI] private Label nama_data = null;
        [UI] private Label stock_data = null;
        [UI] private Label created_at_data = null;
        [UI] private Label created_by_data = null;

        private int _counter;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
            _button2.Clicked += Button2_Clicked;
            serviceHandler = new ServiceHandler();
            LoadInventory();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private async void LoadInventory(){
            InventoryResponse inventoryResponse = await serviceHandler.GetInventory();
            _label2.Text = "";
            no_data.Text = "";
            int no = 0;
            foreach (var item in inventoryResponse.data)
            {
                _label2.Text += item.name + "\n";
                no++;
                no_data.Text += no.ToString() + "\n";
                nama_data.Text += item.name + "\n";
                stock_data.Text += item.stock.ToString() + "\n";
                created_at_data.Text += item.created_at + "\n";
                created_by_data.Text += item.created_by + "\n";
            }
            if (inventoryResponse.data.Count == 0)
            {
                no_data.Text = "No Data";
            }
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {
            // Instantiate Gtk.FileChooserNative
            FileChooserNative fileChooser = new FileChooserNative(
                "Open File",                          // Title
                this,                         // Transient Parent
                FileChooserAction.Open,               // Action (Open, Save, SelectFolder, CreateFolder)
                "Open",                               // Accept button text (optional)
                "Cancel"                              // Cancel button text (optional)
            );

            // Run the dialog and get the response
            ResponseType response = (ResponseType)fileChooser.Run();

            if (response == ResponseType.Accept)
            {
                // Get the selected filename/path
                string filename = fileChooser.Filename;
                Console.WriteLine("Selected file: " + filename);
                bg_image.Pixbuf = new Gdk.Pixbuf(filename);
                // Open file logic here
            }

            // Manually manage the lifetime by destroying the dialog
            fileChooser.Destroy();
        }

        private void Button2_Clicked(object sender, EventArgs a)
        {
            // Instantiate Gtk.FileChooserNative
            FileChooserNative fileChooser = new FileChooserNative(
                "Open File",                          // Title
                this,                         // Transient Parent
                FileChooserAction.Open,               // Action (Open, Save, SelectFolder, CreateFolder)
                "Open",                               // Accept button text (optional)
                "Cancel"                              // Cancel button text (optional)
            );

            // Run the dialog and get the response
            ResponseType response = (ResponseType)fileChooser.Run();

            if (response == ResponseType.Accept)
            {
                // Get the selected filename/path
                string filename = fileChooser.Filename;
                Console.WriteLine("Selected file: " + filename);
                // videoPlayer.SetUri(filename);
                // Open file logic here
            }

            // Manually manage the lifetime by destroying the dialog
            fileChooser.Destroy();
        }
    }
}
