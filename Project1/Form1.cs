namespace Project1
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Add event handlers for the buttons
            button1.Click += (sender, e) => FindPrimes(textBox1, listBox1);
            button2.Click += (sender, e) => FindPrimes(textBox2, listBox2);
        }

        private void FindPrimes(TextBox textBox, ListBox listBox)
        {
            if (int.TryParse(textBox.Text, out int max))
            {
                listBox.Items.Clear();
                Thread thread = new Thread(() => {
                    var primes = GetPrimes(max);  // get all the possible primes
                    // Invoke(new Action(() => listBox.Items.AddRange(primes.Select(p => (object)p).ToArray())));

                    // Use Invoke to update the UI on the main thread
                    Invoke(new Action(() => {
                        if (primes.Any())
                        {
                            // If primes are found, add them to the listbox
                            listBox.Items.AddRange(primes.Select(p => (object)p).ToArray());
                        }
                        else
                        {
                            // If no primes are found, add a message to the listbox
                            listBox.Items.Add("no primes found");
                        }
                    }));

                });
                thread.Start();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private List<int> GetPrimes(int max)
        {
            List<int> primes = new List<int>();
            for (int i = 2; i <= max; i++)
            {
                if (IsPrime(i))
                    primes.Add(i);
            }
            return primes;
        }

        private bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }

}
