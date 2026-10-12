using System.Threading.Channels;

namespace zhgyak
{
    internal class Program
    {
        public class Jatek
        {
            public Jatek(string title, string id, string publisher, DateTime releaseDate, DateTime originalReleaseDate)
            {
                Title = title;
                Id = id;
                Publisher = publisher;
                ReleaseDate = releaseDate;
                OriginalReleaseDate = originalReleaseDate;
            }

            public string Title { get; set; }
            public string Id { get; set; }
            public string Publisher { get; set; }
            public DateTime ReleaseDate { get; set; }
            public DateTime OriginalReleaseDate { get; set; }
        }
        static List<Jatek> jatekok = new List<Jatek> ();

        static List<string[]> mufajok = new List<string[]> ();

        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("games_dataset.csv"))
            {
                sr.ReadLine(); //Jatekok
                sr.ReadLine(); //  amit eleve beirtak
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split(";");
                    jatekok.Add(new Jatek(
                        sor[0],
                        sor[1],
                        sor[2],
                        Convert.ToDateTime(sor[3]),
                        Convert.ToDateTime(sor[4])
                        ));
                }
            }

            using (StreamReader streamReader = new StreamReader("genre.txt"))
            {
                streamReader.ReadLine();
                while (!streamReader.EndOfStream)
                {
                    string[] sor = streamReader.ReadLine().Split(",");
                    for (int i = 0; i < sor.Length; i++)
                    {
                        string[] mufaj = sor[i].Split("=");
                        mufajok.Add(mufaj);
                    }
                }
            }

            for (int i = 0; i < jatekok.Count; i++)
            {
                for (int j = 0; j < mufajok.Count; j++)
                {
                    if (jatekok[i].Id == mufajok[j][1])
                    {
                        jatekok[i].Id = mufajok[j][0];
                    }
                }
            }

            //for (int i = 0;i < mufajok.Count; i++)
            //{
            //    Console.WriteLine($"{mufajok[i][0]} = {mufajok[i][1]}");
            //}

            // 3. Feladat

            Console.Write("Add meg egy keszito nevet: ");
            string kiado = Console.ReadLine();

            int szamlalo = 0;

            for (int i = 0; i < jatekok.Count; i++)
            {
                if (jatekok[i].Publisher == kiado)
                {
                    szamlalo++;
                }
            }

            Console.WriteLine($"{szamlalo}");

            // 4. Feladat

            for (int i =0; i < jatekok.Count; i++)
            {
                if (jatekok[i].ReleaseDate == jatekok[i].OriginalReleaseDate)
                {
                    Console.WriteLine($"{jatekok[i].Title} {jatekok[i].Id} {jatekok[i].ReleaseDate}");
                }
            }

            // 5. Feladat

            for (int i = 0; i < mufajok.Count; i++)
            {
                int darabszam = 0;

                for (int j = 0;j < jatekok.Count;j++)
                {
                    if (mufajok[i][0] == jatekok[j].Id)
                    {
                        darabszam++;
                    }
                }

                Console.WriteLine($"Mufajok: {mufajok[i][0]} | {darabszam} darab");
            }


        }
    }
}
