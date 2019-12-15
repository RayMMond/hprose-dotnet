using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace Impl
{
	public class Runner : IRunner
	{
		private int _value;
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
		}

		public int Value
		{
			get => _value;
			set
			{
				if (value == _value) return;
				_value = value;
				OnPropertyChanged();
			}
		}

		public void Run()
		{
			Task.Delay(1000).ContinueWith(t => { Value = 100; });
		}
	}
}
