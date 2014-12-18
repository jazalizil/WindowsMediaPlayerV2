using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace WindowsMediaPlayerV2Core
{
    public class Program
    {
        [Import(typeof(IPlayer))]
        public IPlayer player;
        private CompositionContainer _container;

        public Program()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            _container = new CompositionContainer(catalog);

            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        static String FileName = "C:\\Users\\jazalizil\\Music\\MZ-MZ_music_vol.3-WEB-FR-2014-RITH\\01-Bratatata.mp3";

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Hello player");
            Console.WriteLine(Environment.CurrentDirectory.ToString());
            p.player.Play();
            Console.ReadLine();
        }
    }
}
