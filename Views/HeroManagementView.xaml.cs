using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class HeroManagementView : Window
    {
        public HeroManagementView()
        {
            InitializeComponent();
        }
        private void LaunchCombat_Click(object sender, RoutedEventArgs e)
        {
            var vm = (HeroManagement)this.DataContext;
            if (vm.SelectedHero == null)
            {
                MessageBox.Show("Veuillez sélectionner un héros d'abord");
                return;
            }
            var combatWindow = new CombatView
            {
                DataContext = new Combat(vm.SelectedHero),
                Owner = this
            };
            combatWindow.ShowDialog();
        }
    }
}