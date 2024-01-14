using ArkoneGestionEvenement.DAL;
using ArkoneGestionEvenement.Models;
using ArkoneGestionEvenement.Utils;
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
                    var regroupeInviteCourant = InviteRegroupeService.GetInviteRegroupeByInviteId(invite.IdInvite);
                    var regroupeInvite = InviteRegroupeService.GetInviteRegroupeByRegroupementId((int)regroupeInviteCourant.First().IdRegroupement);
                    
                    LBL_NomEvent.Content = evenement.Nom;
                    
                    LBL_NomInvite.ContentStringFormat = "Wrap";

                    List<Invite> inviteARegrouper = new List<Invite>();
                    foreach (InvitesRegroupement reg in regroupeInvite)
                    {
                        inviteARegrouper.Add(InviteService.GetInvite((int)reg.IdInvite));
                    }
                    LBL_NomInvite.Content = "Invités autorisés :";
                    foreach (Invite inv in inviteARegrouper)
                    {
                        LBL_NomInvite.Content += "\n - " + inv.Nom + " " + inv.Prenom;
                    }
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

        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)VariablesGlobales.UtilisateurCourant.IsVigile)
            {
                this.Close();
            }
            else
            {
                FEN_ModuleOrganisateur fen = new FEN_ModuleOrganisateur();
                fen.Show();
                this.Close();
            }
        }
    }
}
