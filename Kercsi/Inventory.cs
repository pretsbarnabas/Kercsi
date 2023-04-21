using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kercsi
{
    internal class Inventory : INotifyPropertyChanged
    {
        private int wood;
        public int Wood
        {
            get { return wood; }
            set 
            { 
                wood = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Wood"));
            }
        }

        private int clay;
        public int Clay
        {
            get { return clay; }
            set 
            { 
                clay = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Clay"));
            }
        }
        private int metal;
        public int Metal
        {
            get { return metal; }
            set 
            { 
                metal = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Metal"));
            }
        }
        private int treasure;
        public int Treasure
        {
            get { return treasure; }
            set 
            { 
                treasure = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Treasure"));
            }
        }

        public Inventory()
        {
            Wood = 3;
            Clay = 3;
            Metal = 3;
            Treasure = 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
