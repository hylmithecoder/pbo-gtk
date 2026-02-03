using System;
using Gtk;
using LibVLCSharp.Shared;
using System.Runtime.InteropServices;
using UI = Gtk.Builder.ObjectAttribute;

namespace pbo.Pages
{
    class MainWindow : Window
    {
        private ServiceHandler serviceHandler;
        
        [UI] private Label _labelTitle = null;
        [UI] private TreeView treeViewInventory = null;
        [UI] private Frame video_container = null;
        [UI] private Button _buttonRefresh = null;
        [UI] private Button _buttonOpenVideo = null;

        private ListStore inventoryListStore;
        private LibVLC _libvlc;
        private MediaPlayer _mediaPlayer;
        private DrawingArea _videoWidget;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);
            
            // Initialize LibVLC
            try {
                Core.Initialize();
                _libvlc = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libvlc);
                
                // Configure Video Surface
                _videoWidget = new DrawingArea();
                
                // Set background to black (optional, for video feel)
                _videoWidget.ModifyBg(StateType.Normal, new Gdk.Color(0, 0, 0));

                _videoWidget.Realized += (sender, e) => {
                    var handle = _videoWidget.Window.Handle;
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) 
                    {
                        // On Linux (X11), handle is likely the XID
                        _mediaPlayer.XWindow = (uint)handle; 
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        _mediaPlayer.Hwnd = handle;
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        _mediaPlayer.NsObject = handle;
                    }
                };

                video_container.Add(_videoWidget);
                _videoWidget.ShowAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize Video Player: {ex.Message}");
            }

            DeleteEvent += Window_DeleteEvent;
            _buttonRefresh.Clicked += ButtonRefresh_Clicked;
            _buttonOpenVideo.Clicked += ButtonOpenVideo_Clicked;
            
            serviceHandler = new ServiceHandler();
            
            SetupTreeView();
            LoadInventory();
        }

        private void SetupTreeView()
        {
            treeViewInventory.AppendColumn("No", new CellRendererText(), "text", 0);
            treeViewInventory.AppendColumn("Name", new CellRendererText(), "text", 1);
            treeViewInventory.AppendColumn("Stock", new CellRendererText(), "text", 2);
            treeViewInventory.AppendColumn("Created By", new CellRendererText(), "text", 3);
            treeViewInventory.AppendColumn("Created At", new CellRendererText(), "text", 4);

            inventoryListStore = new ListStore(typeof(int), typeof(string), typeof(int), typeof(string), typeof(string));
            treeViewInventory.Model = inventoryListStore;
        }

        private async void LoadInventory()
        {
            try 
            {
                inventoryListStore.Clear();
                InventoryResponse inventoryResponse = await serviceHandler.GetInventory();
                
                if (inventoryResponse != null && inventoryResponse.data != null)
                {
                    int no = 1;
                    foreach (var item in inventoryResponse.data)
                    {
                        inventoryListStore.AppendValues(
                            no++, 
                            item.name ?? "-", 
                            item.stock ?? 0, 
                            item.created_by ?? "-", 
                            item.created_at ?? "-"
                        );
                    }
                }
                else
                {
                   Console.WriteLine("No data received or API error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading inventory: {ex.Message}");
                // MessageDialog requires a parent window, 'this' works if realized
                // If called from constructor (async), window might not be fully ready, but typically fine.
            }
        }

        private void ButtonRefresh_Clicked(object sender, EventArgs a)
        {
            LoadInventory();
        }

        private void ButtonOpenVideo_Clicked(object sender, EventArgs a)
        {
            FileChooserNative fileChooser = new FileChooserNative(
                "Open Video", 
                this, 
                FileChooserAction.Open, 
                "Open", 
                "Cancel"
            );

            if (fileChooser.Run() == (int)ResponseType.Accept)
            {
                string filename = fileChooser.Filename;
                if (!string.IsNullOrEmpty(filename))
                {
                    if (_libvlc != null)
                    {
                        using (var media = new Media(_libvlc, filename))
                        {
                            _mediaPlayer.Play(media);
                        }
                    }
                }
            }
            fileChooser.Destroy();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Stop();
                _mediaPlayer.Dispose();
            }
            if (_libvlc != null)
            {
                _libvlc.Dispose();
            }
            Application.Quit();
        }
    }
}
