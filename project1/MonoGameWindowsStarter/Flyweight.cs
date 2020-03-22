using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    public class Flyweight
    {
        private Meteor _sharedstate;

        public Flyweight(Meteor meteor)
        {
            this._sharedstate = meteor;
        }
    }

    public class Meteor
    {
        public Rectangle meteorRect { get; set; }
        public double Progression { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
