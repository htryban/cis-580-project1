using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace MonogameWindowsStarter
{
    public class TilemapTileset
    {
        public int FirstGID;

        public string Source;

        public ExternalReference<TilesetContent> Reference;
    }

    public class EnemyContent
    {
        public uint numberOfEnemies { get; set; }
    }

}