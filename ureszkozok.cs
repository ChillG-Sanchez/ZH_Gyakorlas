az ureszkozos elso megoldasa:

namespace Ureszkozok
{
    public class Ureszkozok
    {
        public Ureszkozok(string name, string typeID, string @operator, DateTime activationDate, DateTime originalLaunchDate)
        {
            Name = name;
            TypeID = typeID;
            Operator = @operator;
            ActivationDate = activationDate;
            OriginalLaunchDate = originalLaunchDate;
        }

        public string Name { get; set; }
        public string TypeID { get; set; }
        public string Operator { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime OriginalLaunchDate { get; set; }

    }

    internal class Program
    {
        static List<string[]> eszkozTipus = new List<string[]>();
        static List<Ureszkozok> ureszkozok = new List<Ureszkozok>();

        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("eszkoz_tipusok.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine()!.Split(",");
                    for (int i = 0; i < sor.Length; i++)
                    {
                        string[] eszkozSzam = sor[i].Split("=");
                        eszkozTipus.Add(eszkozSzam);
                    }
                }
            }

            using (StreamReader streamReader = new StreamReader("ureszkozok_dataset.csv"))
            {
                streamReader.ReadLine();
                streamReader.ReadLine();
                while (!streamReader.EndOfStream)
                {
                    string[] sor = streamReader.ReadLine().Split(";");
                    ureszkozok.Add(new Ureszkozok(
                        sor[0],
                        sor[1],
                        sor[2],
                        Convert.ToDateTime(sor[3]),
                        Convert.ToDateTime(sor[4])
                        ));
                }
            }

            for (int i = 0; i < eszkozTipus.Count; i++)
            {
                for (int j = 0; j < ureszkozok.Count; j++)
                {
                    if (eszkozTipus[i][1] == ureszkozok[j].TypeID)
                    {
                        ureszkozok[j].TypeID = eszkozTipus[i][0];
                    }
                }
            }

            // 3.

            Console.Write("Adja meg egy uzemelteto nevet: ");
            string uzemelteto = Console.ReadLine();

            int szamlalo = 0;

            for (int i = 0; i < ureszkozok.Count; i++)
            {
                if (ureszkozok[i].Operator == uzemelteto)
                {
                    szamlalo++;
                }
            }
            Console.WriteLine($"{szamlalo} db");

            // 4.

            for (int i = 0; i  < ureszkozok.Count; ++i)
            {
                if (ureszkozok[i].OriginalLaunchDate.Year == ureszkozok[i].ActivationDate.Year)
                {
                    Console.WriteLine($"{ureszkozok[i].Name} {ureszkozok[i].TypeID} {ureszkozok[i].OriginalLaunchDate.Year}");
                }
            }

            // 5.

            for (int i = 0; i < eszkozTipus.Count; i++)
            {
                int eszkozSzamlalo = 0;

                for (int j = 0; j < ureszkozok.Count; j++)
                {
                    if (ureszkozok[j].TypeID == eszkozTipus[i][0])
                    {
                        eszkozSzamlalo++;
                    }
                }

                Console.WriteLine($"{ureszkozok[i].TypeID} {eszkozSzamlalo}");
            }
        }
    }
}