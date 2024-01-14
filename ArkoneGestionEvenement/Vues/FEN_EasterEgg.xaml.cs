using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArkoneGestionEvenement.Vues
{
    /// <summary>
    /// Logique d'interaction pour FEN_EasterEgg.xaml
    /// </summary>
    public partial class FEN_EasterEgg : Window
    {
        private SoundPlayer player;

        public FEN_EasterEgg()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string cheminAudio = "C:\\Users\\mat\\Documents\\GitHub\\ArkoneGestion\\ArkoneGestionEvenement\\bin\\Debug\\net7.0-windows\\Assets\\Music\\cat.wav";

            try
            {
                using (player = new SoundPlayer(cheminAudio))
                {
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la lecture du son : " + ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            player.Stop();
        }
    }
}
