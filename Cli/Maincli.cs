using System;
using static System.Random;
using Gtk;

namespace pbo
{
    public class MainCli
    {
        private Random random = new Random();
        public MainCli()
        {
            Application.Init(); // Initialize GTK once for all dialogs

            Tugas2Pendahuluan();

            testFaktorial();
            Soal1();
            Soal2();
            Soal3();
            Soal4();
            Soal5();
        }

        void Tugas2Pendahuluan()
        {
            Console.WriteLine("While Loop");
            LoopWhile();
            Console.WriteLine("For Loop");
            ForLoop();
            Console.WriteLine("Do While Loop");
            DoWhileLoop();
        }
        void LoopWhile()
        {
            int i = 10;
            while (i > 0)
            {
                Console.WriteLine($"{i}");
                i--;
            }            
        }

        void DoWhileLoop()
        {
            int i = 0;
            do
            {
                Console.WriteLine($"{i}");
                i++;
            } while (i < 10);
        }

        void ForLoop()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i}");
            }
        }

        void ForLoopBreak(int breakTo)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == breakTo)
                {
                    break;
                }
                Console.Write($"Looping: {i}");
            }
        }

        void ForLoopBreakLabel(int breakTo)
        {
            outerLoop:
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == breakTo)
                    {
                        goto outerLoop;
                    }
                    Console.Write($"Looping: {i} {j}");
                }
            }
        }

        void ForLoopContinue(int continueTo)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == continueTo)
                {
                    continue;
                }
                Console.Write($"Looping: {i}");
            }
        }

        void ForLoopContinueLabel(int continueTo)
        {
            outerLoop:
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == continueTo)
                    {
                        goto outerLoop;
                    }
                    Console.Write($"Looping: {i} {j}");
                }
            }
        }

        void cobaFor()
        {
            int jml = 0;
            for (int i = 1; i <= 10; i++)
            {
                jml += i;
            }
            Console.WriteLine($"Jumlah: {jml}");
        }

        void cobaFor1()
        {
            for (double jari = 1.0; jari <= 2.0; jari += 0.2)
            {
                Console.WriteLine($"Radius: {jari} Luas: {Math.PI * jari * jari}");
            }
        }

        void CobaWhile()
        {
            double r = 0;
            while (r <= 0.99d)
            {
                r = random.NextDouble();
                Console.WriteLine($"{r}");
            }
        }

        void CobaDoWhile()
        {
            double r = 0;
            do
            {
                r = random.NextDouble();
                Console.WriteLine(r);
            } while (r <= 0.99d);
        }

        long Faktorial(long n)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;
            long hasil = 1;
            for (long i = 1; i <= n; i++)
            {
                hasil *= i;
            }
            return hasil;
        }

        void testFaktorial()
        {
            long batas = 4;
            for(int i = 0; i <= batas; i++)
            {
                Console.WriteLine("Faktorial: {0}", i);
                long hasil = Faktorial(i);
                Console.WriteLine($"{i}! = {hasil}");
            }
        }

        int Pangkat(int a, int b)
        {
            int hasil = 1;
            for (int i = 0; i < b; i++)
            {
                hasil *= a;
            }
            return hasil;
        }

        void Soal1()
        {
            Console.WriteLine("Soal 1");
            cobaFor();
            cobaFor1();
            CobaWhile();
            CobaDoWhile();
            Faktorial(2);
        }

        int Soal2A(int a, int b)
        {
            return Pangkat(a, b) + Pangkat(b, 2);
        }

        void Soal2C()
        {
            int pangkat42 = Pangkat(4, 2);
            int pangkat51 = Pangkat(5, 1);
            Console.WriteLine($"{pangkat42}, {pangkat51}");
            int jumlahper = pangkat42 + pangkat51;
            Console.WriteLine($"{jumlahper}");
            jumlahper /= 5;
            Console.WriteLine($"{jumlahper}");
            jumlahper += pangkat42;

            Console.WriteLine(jumlahper);
        }

        void Soal2()
        {
            Console.Write("Soal 2\na. 4^3 + 5^2: ");
            Console.WriteLine(Pangkat(4, 3) + Pangkat(5, 2));
            Console.Write("b. 5! + 4!: ");
            Console.WriteLine(Faktorial(5) + Faktorial(4));
            Console.Write("c. (4^2 + 5^1) / 5 + 4^2: ");
            Soal2C();
            // Console.WriteLine((Pangkat(4, 2) + Pangkat(5, 1)) / 5 + Pangkat(4, 2));
        }

        void HandleWithIf()
        {
            Console.WriteLine("Input angka 1 - 10:");
            string stream = Console.ReadLine() ?? "0";
            if (!int.TryParse(stream, out int input))
            {
                Console.WriteLine("Input tidak valid");
                return;
            }

            if (input == 1)
            {
                Console.WriteLine("Satu");
            }
            else if (input == 2)
            {
                Console.WriteLine("Dua");
            }
            else if (input == 3)
            {
                Console.WriteLine("Tiga");
            }
            else if (input == 4)
            {
                Console.WriteLine("Empat");
            }
            else if (input == 5)
            {
                Console.WriteLine("Lima");
            }
            else if (input == 6)
            {
                Console.WriteLine("Enam");
            }
            else if (input == 7)
            {
                Console.WriteLine("Tujuh");
            }
            else if (input == 8)
            {
                Console.WriteLine("Delapan");
            }
            else if (input == 9)
            {
                Console.WriteLine("Sembilan");
            }
            else if (input == 10)
            {
                Console.WriteLine("Sepuluh");
            }
            else
            {
                Console.WriteLine("Input tidak valid");
            }
        }

        void HandleWithSwitch()
        {
            Console.WriteLine("Input angka 1 - 10:");
            string stream = Console.ReadLine() ?? "0";
            if (!int.TryParse(stream, out int input))
            {
                Console.WriteLine("Input tidak valid");
                return;
            }
            switch (input)
            {
                case 1:
                    Console.WriteLine("Satu");
                    break;
                case 2:
                    Console.WriteLine("Dua");
                    break;
                case 3:
                    Console.WriteLine("Tiga");
                    break;
                case 4:
                    Console.WriteLine("Empat");
                    break;
                case 5:
                    Console.WriteLine("Lima");
                    break;
                case 6:
                    Console.WriteLine("Enam");
                    break;
                case 7:
                    Console.WriteLine("Tujuh");
                    break;
                case 8:
                    Console.WriteLine("Delapan");
                    break;
                case 9:
                    Console.WriteLine("Sembilan");
                    break;
                case 10:
                    Console.WriteLine("Sepuluh");
                    break;
                default:
                    Console.WriteLine("Input tidak valid");
                    break;
            }
        }

        void Soal3()
        {
            Console.WriteLine("Soal 3");
            HandleWithIf();
            HandleWithSwitch();
        }

        void NilaiGanjil()
        {
            // 2. Create the dialog (parent = null, no main window needed)
            MessageDialog dialog = new MessageDialog(
                null,
                DialogFlags.Modal,
                MessageType.Info,
                ButtonsType.Ok,
                "NIM Ganjil"
            );
            dialog.Title = "Info";

            // 3. Run dialog and wait for response
            dialog.Run();

            // 4. Destroy dialog
            dialog.Destroy();
        }

        void Soal4()
        {
            Console.WriteLine("Soal 4");
            Console.WriteLine("Masukkan NIM Anda: ");
            int nim = int.Parse(Console.ReadLine() ?? "0");
            if (nim % 2 == 0)
            {
                Console.WriteLine("NIM Genap");
            }
            else
            {
                NilaiGanjil();
            }
        }

        void Soal5()
        {
            Console.WriteLine("Soal 5");

            // 1. Create Dialog without a parent window (null)
            MessageDialog md = new MessageDialog(null, 
                DialogFlags.Modal, 
                MessageType.Question, 
                ButtonsType.OkCancel, 
                "Masukkan Angka yang Anda inginkan");
            
            md.Title = "Input Needed";

            // 2. Add an entry field for input
            Entry entry = new Entry();
            entry.Show();
            md.ContentArea.PackStart(entry, true, true, 5);
            md.ShowAll();

            // 3. Run dialog
            ResponseType response = (ResponseType)md.Run();

            if (response == ResponseType.Ok)
            {
                if (int.TryParse(entry.Text, out int n))
                {
                    for (int i = 1; i <= n; i++)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            Console.Write("*");
                        }
                        Console.WriteLine();
                    }
                }
            }

            // 4. Destroy
            md.Destroy();
        }
    }
}