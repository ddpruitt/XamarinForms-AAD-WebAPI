using Xamarin.Forms;
using XFConversion.ViewModels;

namespace XFConversion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            vm.Load();
            this.BindingContext =vm;

        }

    }
}
