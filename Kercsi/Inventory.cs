using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kercsi
{
    public class Inventory : INotifyPropertyChanged
    {
        private int wood;
        public int Wood
        {
            get { return wood; }
            set 
            { 
                wood = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Wood"));
                if (wood == 0)
                {
                    CraftRoadIsActive = false;
                    CraftShovelIsActive = false;
                }
                else if (metal > 0)
                {
                    CraftShovelIsActive = true;
                }
                else if(clay > 0)
                {
                    CraftRoadIsActive = true;
                }
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
                if (clay == 0)
                {
                    CraftRoadIsActive = false;
                }
                else if (wood > 0)
                {
                    CraftRoadIsActive = true;
                }
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
                if (metal == 0)
                {
                    CraftRoadIsActive = false;
                    CraftShovelIsActive = false;
                }
                else if (wood > 0)
                {
                    CraftShovelIsActive = true;
                }
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
        private int shovel;
        public int Shovel
        {
            get { return shovel; }
            set 
            { 
                shovel = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Shovel"));
            }
        }
        private int road;
        public int Road
        {
            get { return road; }
            set 
            { 
                road = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Road"));
            }
        }
        private bool craftshovelisactive;
        public bool CraftShovelIsActive
        {
            get { return craftshovelisactive; }
            set 
            { 
                craftshovelisactive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CraftShovelIsActive"));
            }
        }

        private bool craftroadisactive;
        public bool CraftRoadIsActive
        {
            get { return craftroadisactive; }
            set 
            { 
                craftroadisactive = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CraftRoadIsActive"));
            }
        }




        public Inventory()
        {
            Wood = 3;
            Clay = 3;
            Metal = 3;
            Treasure = 0;
            Shovel = 0;
            Road = 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void CraftRoad()
        {
            if(Wood>0 && Clay > 0)
            {
                Wood--;
                Clay--;
                Road++;
            }
        }
        public void CraftShovel()
        {
            if(Wood>0 && Metal > 0)
            {
                Wood--;
                Metal--;
                Shovel++;
            }
        }
    }
}
