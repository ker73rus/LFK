using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal interface Levels
    {
        public bool lose {  get; set; }
        public bool story { get; set; }
        public bool win {  get; set; }
        public bool tutor {  get; set; }
        public void loadLevel1();
        public void loadLevel2();
        public void loadLevel3();
        public void Win();
        public void Begin();
    }
}
