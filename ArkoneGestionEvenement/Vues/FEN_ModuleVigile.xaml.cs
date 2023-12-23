using ArkoneGestionEvenement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logique d'interaction pour FEN_ModuleVigile.xaml
    /// </summary>
    public partial class FEN_ModuleVigile : Window
    {
        public FEN_ModuleVigile()
        {
            InitializeComponent();
        }

        private void BTN_ValiderCode_Click(object sender, RoutedEventArgs e)
        {
            if(TBX_CodeAcces.Text.Length == 6)
            {
                var acces = CodeAccesService.GetCodeAcces(TBX_CodeAcces.Text);
                if( acces != null && acces.Statut != "Entré")
                {
                    LBL_ErreurCode.Visibility = Visibility.Hidden;
                    var invite = InviteService.GetInvite((int)acces.IdInvite);
                    var evenement = EvenementService.GetEvenement((int) acces.IdEvent);
                    
                    LBL_NomEvent.Content = evenement.Nom;
                    LBL_NomInvite.Content = invite.Nom + " " + invite.Prenom;
                    
                    acces.Statut = "Entré";
                    CodeAccesService.UpdateCodeAcces(acces);
                    CNV_Valide.Visibility = Visibility.Visible;
                }
                else
                {
                    LBL_ErreurCode.Visibility = Visibility.Visible;
                }
            }  
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BTN_ValiderEntree_Click(object sender, RoutedEventArgs e)
        {
            TBX_CodeAcces.Text = "";
            CNV_Valide.Visibility = Visibility.Hidden;
        }
    }
}
