using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = MonoGameWindowsStarter.EnemyContent;


namespace MonoGameWindowsStarter
{
    [ContentImporter(".tmx", DisplayName = "TMX Importer - Tiled", DefaultProcessor = "TilemapProcessor")]

    public class EnemyImporter : ContentImporter<TInput>
    {

        public override TInput Import(string filename, ContentImporterContext context)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);

            // The tilemap should be the tilemap tag
            XmlNode map = document.SelectSingleNode("//map");

            // The attributes on the tilemap 
            uint NumberOfEnemies = uint.Parse(map.Attributes["numberOfEnemies"].Value);

            // Construct the TilemapContent
            var output = new EnemyContent()
            {
                numberOfEnemies = NumberOfEnemies;
            };

            // A tilemap will have one or more tilesets 
            XmlNodeList tilesets = map.SelectNodes("//tileset");
            foreach (XmlNode tileset in tilesets)
            {
                output.Tilesets.Add(new TilemapTileset()
                {
                    FirstGID = int.Parse(tileset.Attributes["firstgid"].Value),
                    Source = tileset.Attributes["source"].Value
                });
            }

            // A tilemap will have one or more layers
            XmlNodeList layers = map.SelectNodes("//layer");
            foreach (XmlNode layer in layers)
            {
                var id = uint.Parse(layer.Attributes["id"].Value);
                var name = layer.Attributes["name"].Value;
                var width = uint.Parse(layer.Attributes["width"].Value);
                var height = uint.Parse(layer.Attributes["height"].Value);

                // A tilemap layer will have a data element
                XmlNode data = layer.SelectSingleNode("//data");
                if (data.Attributes["encoding"].Value != "csv") throw new NotSupportedException("Only csv encoding is supported");
                var dataString = data.InnerText;

                output.Layers.Add(new TilemapLayerContent()
                {
                    Id = id,
                    Name = name,
                    Width = width,
                    Height = height,
                    DataString = dataString
                });
            }

            return output;
        }
    }
}