using Aszteroidák.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidák.Persistence
{
    public class TextFilePersistence : IPersistence
    {
        public Game Load(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása olvasásra
                {
                    String[] numbers = reader.ReadToEnd().Split(' '); // fájl tartalmának feldarabolása a whitespace karakterek mentén


                    Game values = new Game();

                    values.Player = new Player(Int32.Parse(numbers[0]), Int32.Parse(numbers[1]));
                    values.GameTime = TimeSpan.Parse(numbers[2]);
                    values.Asteroids = new List<Asteroid>();

                    for (Int32 i = 3; i < numbers.Length - 2; i += 2)
                    {
                        values.AddAsteroid(new Asteroid(Int32.Parse(numbers[i]), Int32.Parse(numbers[i + 1])));
                    }

                    return values;
                } // bezárul a fájl
            }
            catch // ha bármi hiba történt
            {
                throw new DataException("Error occurred during reading.");
            }
        }

        public void Save(String path, Game values)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása írásra
                {
                    writer.Write(values.Player!.X + " ");
                    writer.Write(values.Player.Y + " ");
                    writer.Write(values.GameTime + " ");
                    for (Int32 i = 0; i < values.Asteroids!.Count; i++)
                    {
                        writer.Write(values.Asteroids[i].X + " ");
                        writer.Write(values.Asteroids[i].Y + " ");
                    }
                }
            }
            catch // ha bármi hiba történt
            {
                throw new DataException("Error occurred during writing.");
            }
        }
    }
}
