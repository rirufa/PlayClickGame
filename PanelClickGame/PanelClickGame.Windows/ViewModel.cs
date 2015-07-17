using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using System.Windows.Input;

namespace PanelClickGame
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            this.PropertyChanged += (s, e) => { };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperyChanged([CallerMemberName] string property = "")
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

    class ViewModel : ViewModelBase
    {
        Model model = new Model(16);
        DispatcherTimer timer = new DispatcherTimer();

        public ViewModel() : base()
        {
            this.OnProperyChanged("NumberList");
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            this.timer.Tick += (s, e) =>
            {
                this.OnProperyChanged("ElapsedSecnod");
            };
            this.timer.Start();
        }

        public double ElapsedSecnod
        {
            get
            {
                return this.model.ElapsedTick / 10000 / 1000;
            }
        }

        public ObservableCollection<NumberViewModel> NumberList
        {
            get
            {
                var list = new ObservableCollection<NumberViewModel>();
                foreach (var item in this.model.NumberList())
                    list.Add(new NumberViewModel(this.model, item));
                return list;
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                return new DelegateCommand<object>((e) => {
                    this.OnProperyChanged("NumberList");
                    this.model.Reset();
                });
            }
        }
    }

    class NumberViewModel : ViewModelBase
    {
        Model model;
        bool clicked = false;
        public NumberViewModel(Model model, int number) : base()
        {
            this.model = model;
            this.Number = number;
            this.OnProperyChanged("Label");
        }

        public string Label
        {
            get
            {
                if (clicked)
                    return " ";
                else
                    return this.Number.ToString();
            }
        }

        public int Number
        {
            get;
            private set;
        }

        public ICommand ClickCommand
        {
            get
            {
                return new DelegateCommand<object>((e) => {
                    this.model.Click(this.Number);
                    this.clicked = true;
                    this.OnProperyChanged("Label");
                });
            }
        }
    }
}
