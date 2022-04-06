using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Text;
using Avalonia.Data.Converters;
using ReactiveUI;

namespace avaloniaX.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _greeting;

        public string Greeting
        {
            get => _greeting;
            set => this.RaiseAndSetIfChanged(ref _greeting, value);
        } 
        public ObservableCollection<Foo> Foos { get; } = new ObservableCollection<Foo>();
        public ReactiveCommand<Arg, Unit> Command { get; }
        public MainViewModel()
        {
            Greeting = "Welcome to Avalonia!";
            for (var i = 0; i < 64; i++)
            {
                Foos.Add(new Foo(i));
            }

            Command = ReactiveCommand.Create<Arg>(OnCommand);
        }

        void OnCommand(Arg arg)
        {
            Greeting = arg.ToString();
        }
    }

    public class ArgConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Arg()
            {
                BarId = ((Bar)values.FirstOrDefault(x => x is Bar))?.Id ?? -1,
                FooId = ((Foo)values.FirstOrDefault(x => x is Foo))?.Id ?? -1
            };
        }
    }
    public class Arg : ReactiveObject
    {
        private int _fooId, _barId;

        public int FooId
        {
            get => _fooId; 
            set => this.RaiseAndSetIfChanged(ref _fooId, value);
        } 
        public int BarId 
        {
            get => _barId; 
            set => this.RaiseAndSetIfChanged(ref _barId, value);
        }

        public override string ToString()
        {
            return $"Foo was {_fooId} Bar was {_barId}";
        }
    }
    public class Bar : ViewModelBase
    {
     
        public int Id { get; }

        public Bar(int id)
        {
            Id = id;
        }
        
    }
    public class Foo : ViewModelBase
    {
        public int Id { get; }
        public ObservableCollection<Bar> Bars { get; } = new ObservableCollection<Bar>();
        public Foo(int id)
        {
            Id = id;
            for (var i = 0; i < 64; i++)
            {
               Bars.Add(new Bar(i));
            }
        }
    }
}
