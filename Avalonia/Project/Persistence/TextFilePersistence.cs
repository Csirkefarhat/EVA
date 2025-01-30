using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence
{
    public class TextFilePersistence : IPersistence
    {
        public async Task<Game> Load(Stream stream)
        {
            try
            {
                using (StreamReader reader = new StreamReader(stream)) // fájl megnyitása olvasásra
                {
                    String[] numbers = (await reader.ReadToEndAsync()).ToString().Split(' '); // fájl tartalmának feldarabolása a whitespace karakterek mentén

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
            catch (DataException)// ha bármi hiba történt
            {
                throw new DataException("Error occurred during reading.");
            }
        }

        public async void Save(Stream stream, Game values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            try
            {
                using (StreamWriter writer = new StreamWriter(stream)) // fájl megnyitása írásra
                {

                    await writer.WriteAsync(values.Player!.X + " ");
                    await writer.WriteAsync(values.Player.Y + " ");
                    await writer.WriteAsync(values.GameTime + " ");
                    for (Int32 i = 0; i < values.Asteroids!.Count; i++)
                    {
                        await writer.WriteAsync(values.Asteroids[i].X + " ");
                        await writer.WriteAsync(values.Asteroids[i].Y + " ");
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
