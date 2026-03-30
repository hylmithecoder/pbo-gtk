using System;
using Gtk;
using pbo.Pages;

namespace pbo
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            int choice;
            Console.Write("Pilih GUI / CLI\n1.GUI\n2.CLI\n");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Application.Init();

                var app = new Application("org.pbo.pbo", GLib.ApplicationFlags.None);
                app.Register(GLib.Cancellable.Current);

                var win = new MainWindow();
                app.AddWindow(win);

                win.Show();
                Application.Run();
            } else if (choice == 2)
            {
                MainCli maincli = new MainCli();
            }
        }
    }
}
