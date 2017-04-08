using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testApp
{
    public partial class Form1 : Form
    {
        const string conf_database = "now3";
        const string conf_coll = "ludzie";
        protected static IMongoDatabase db;
        protected static IMongoClient mongo;
        Czlowiek czlowiekRoboczy;
        
        public Form1()
        {
            InitializeComponent();
            mongo = new MongoClient();
            db = mongo.GetDatabase(conf_database);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            var coll = db.GetCollection<Czlowiek>(conf_coll);
            var dodani = new Czlowiek
            {
                Id = ObjectId.GenerateNewId(),
                Imie = textBox1.Text,
                Wiek = Convert.ToInt16(textBox2.Text)
            };
            coll.InsertOne(dodani);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            nowaLista();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if ( listBox1.SelectedItem != null)
            {
                Czlowiek selectedItem = listBox1.SelectedItem as Czlowiek;
                var coll = db.GetCollection<Czlowiek>(conf_coll);


                FilterDefinition<Czlowiek> dd = Builders<Czlowiek>.Filter.Eq(szuk =>  szuk.Id , selectedItem.Id);
                coll.DeleteOne(dd);
                MessageBox.Show("Usunięto obiekt " + selectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem);
            } else
            {
                MessageBox.Show("kogos");
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            czlowiekRoboczy = listBox1.SelectedItem as Czlowiek;
            textBox1.Text = czlowiekRoboczy.Imie;
            textBox2.Text = czlowiekRoboczy.Wiek.ToString();

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(czlowiekRoboczy != null)
            {
                var coll = db.GetCollection<Czlowiek>(conf_coll);
                FilterDefinition<Czlowiek> dd = Builders<Czlowiek>.Filter.Eq(szuk => szuk.Id, czlowiekRoboczy.Id);
                var update = Builders<Czlowiek>.Update.Set(szuk => szuk.Wiek, Convert.ToInt16(textBox2.Text)).Set(szuk => szuk.Imie, textBox1.Text);


                coll.UpdateOne(dd, update);
                czlowiekRoboczy = null;
                nowaLista();
            } else
            {
                MessageBox.Show("Nie wybrano do update - podwojny klik");
            }
        }

        private void nowaLista()
        {
            listBox1.Items.Clear();
            var coll = db.GetCollection<Czlowiek>(conf_coll);
            var x = coll.Find(_ => true);
            var s = x.ToList();
            foreach (Czlowiek item in s)
            {
                try
                {
                    Console.WriteLine(item.ToString());
                    listBox1.Items.Add(item);
                }
                catch
                {
                    Console.WriteLine("elemnt nie znaleziony");
                    listBox1.Items.Add("N/A");
                }
            }

        }
    }
}
