using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naloga_1
{
    public static class Operacije
    {
        public static void GlavnaNavodila()
        {
            Console.WriteLine("Izberite želeno operacijo: ");
            Console.WriteLine("");
            Console.WriteLine("1 - Vnos artikla\n2 - Izpis artiklov\n3 - Urejanje artiklov\n4 - Izhod\n");
        }

        public static void NavodilaZaIzpis()
        {
            Console.WriteLine("Izberite želeno operacijo: ");
            Console.WriteLine("");
            Console.WriteLine("1 - Izpiši artikle izbranega dobavitelja\n2 - Izpiši vse artikle\n3 - Izhod\n");
        }

        public static void ShraniVDatoteko(string pot, List<Artikel> seznamArtiklov)
        {
            File.WriteAllText(pot, String.Empty);

            using (StreamWriter sw = new StreamWriter(pot))
            {
                foreach (Artikel artikel in seznamArtiklov)
                {
                    sw.WriteLine(artikel.Izpisi());
                }
            }
        }

        public static List<string> PreberiIzDatoteke(string pot)
        {
            List<string> artikli = new List<string>();

            using (StreamReader sr = new StreamReader(pot))
            {
                string vrstica;
                while ((vrstica = sr.ReadLine()) != null)
                {
                    artikli.Add(vrstica);
                }
            }

            return artikli;
        }

        public static List<Artikel> ShraniVSeznamArtiklov(List<string> artikli)
        {
            List<Artikel> seznamArtiklov = new List<Artikel>();

            for(int i = 0; i < artikli.Count; i++)
            {
                string[] parametriArtikla = artikli[i].Split('|');
                List<string> vrednostiParametrov = new List<string>();

                for (int j = 0; j < parametriArtikla.Length; j++)
                {
                    vrednostiParametrov.Add(parametriArtikla[j].Split(':')[1].Trim());
                }
                 
                Artikel artikel = new Artikel();

                artikel.ime = vrednostiParametrov[0];
                artikel.cena = double.Parse(vrednostiParametrov[1]);
                artikel.zaloga = int.Parse(vrednostiParametrov[2]);
                artikel.dobavitelj = vrednostiParametrov[3].Trim(';');

                seznamArtiklov.Add(artikel);
            }

            return seznamArtiklov;
        }

        public static void VnosArtikla(string pot, List<Artikel> seznamArtiklov)
        {
            Artikel novArtikel = new Artikel();
            bool konecVnosa = false;

            do
            {
                Console.Write("Vnesi ime: ");
                string ime = Console.ReadLine();

                if (string.IsNullOrEmpty(ime))
                {
                    Console.WriteLine("Polje z imenom je obvezno!\n");
                }
                else if (ime[0] == ' ')
                {
                    Console.WriteLine("Nepravilna vrednost imena!\n");
                }
                else
                {
                    novArtikel.ime = ime;
                    konecVnosa = true;
                }

            } while (konecVnosa != true);

            do
            {
                konecVnosa = false;

                Console.Write("Vnesi ceno: ");
                string cena = Console.ReadLine();

                if (string.IsNullOrEmpty(cena))
                {
                    Console.WriteLine("Polje s ceno je obvezno!\n");
                }
                else if (!double.TryParse(cena, out double _))
                {
                    Console.WriteLine("Cena ni število!\n");
                }
                else
                {
                    novArtikel.cena = double.Parse(cena);
                    konecVnosa = true;
                }

            } while (konecVnosa != true);

            do
            {
                konecVnosa = false;

                Console.Write("Vnesi zalogo: ");
                string zaloga = Console.ReadLine();

                if (string.IsNullOrEmpty(zaloga))
                {
                    Console.WriteLine("Polje zalogo je obvezno!\n");
                }
                else if (!int.TryParse(zaloga, out int _))
                {
                    Console.WriteLine("Zaloga ni število!\n");
                }
                else
                {
                    novArtikel.zaloga = int.Parse(zaloga);
                    konecVnosa = true;
                }

            } while (konecVnosa != true);

            do
            {
                konecVnosa = false;

                Console.Write("Vnesi dobavitelja: ");
                string dobavitelj = Console.ReadLine();

                if (string.IsNullOrEmpty(dobavitelj))
                {
                    Console.WriteLine("Polje dobavitelj je obvezno!\n");
                }
                else if (dobavitelj[0] == ' ')
                {
                    Console.WriteLine("Nepravilna vrednost dobavitelja!\n");
                }
                else
                {
                    novArtikel.dobavitelj = dobavitelj;
                    konecVnosa = true;
                }

            } while (konecVnosa != true);

            seznamArtiklov.Add(novArtikel);

            Console.WriteLine("Vnos artikla je bil uspešen!\n");
            Console.WriteLine($"Vnesli ste artikel z naslednjimi podatki:\n\n{novArtikel.Izpisi()}\n");

            ShraniVDatoteko(pot, seznamArtiklov);
        }

        public static void IzpisiArtikle(List<Artikel> seznamArtiklov)
        {
            int izbira;
            do
            {
                NavodilaZaIzpis();
                if (!int.TryParse(Console.ReadLine(), out izbira))
                {
                    Console.WriteLine("Izbira mora biti ena od navedenih številk v navodilu! \n");
                    continue;
                }
                else
                {
                    switch (izbira)
                    {
                        case 1:
                            Console.Write("Vnesi ime dobavitelja: ");
                            string dobavitelj = Console.ReadLine();

                            Console.Write("Vnesi število, za katero želite izpisati artikle, ki imajo manjšo zalogo, kot to število: ");
                            string zaloga = Console.ReadLine();

                            if (!int.TryParse(zaloga, out int _))
                            {
                                Console.WriteLine("Zaloga mora biti število!");
                            }
                            else
                            {
                                var artikliDobavitelja = from artikel in seznamArtiklov
                                                         where artikel.dobavitelj == dobavitelj && artikel.zaloga < int.Parse(zaloga)
                                                         select artikel;

                                if (!artikliDobavitelja.Any())
                                {
                                    Console.WriteLine("Ni artiklov tega dobavitelja oz. ni artiklov, ki bi imele manjšo zalogo, kot izbrano število!\n");
                                }
                                else
                                {
                                    foreach (Artikel artikel in artikliDobavitelja)
                                    {
                                        Console.WriteLine(artikel.Izpisi());
                                    }

                                    Console.WriteLine();

                                    string imeDatoteke = $"Artikli_{dobavitelj}_z_zalogo_manj_kot_{zaloga}.txt";
                                    ShraniVDatoteko(@"..\..\..\" + imeDatoteke, artikliDobavitelja.ToList());
                                }
                            }
                            break;
                        case 2:
                            if (!seznamArtiklov.Any())
                            {
                                Console.WriteLine("Ni vnešenih artiklov!\n");
                            }
                            else
                            {
                                foreach (Artikel artikel in seznamArtiklov)
                                {
                                    Console.WriteLine(artikel.Izpisi());
                                }
                                Console.WriteLine();
                            }
                            break;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("Izbira mora biti ena od navedenih številk v navodilu! \n");
                            break;
                    }
                }
            } while (izbira != 3);
        }

        public static void UrediArtikle(List<Artikel> seznamArtiklov)
        {
            Console.Write("Vnesi vrednost popusta v procentih: ");
            string vrednost = Console.ReadLine();

            if(!int.TryParse(vrednost, out int _))
            {
                Console.WriteLine("Vrednost mora biti število!");
            }
            else if(int.Parse(vrednost) < 0 || int.Parse(vrednost) > 100)
            {
                Console.WriteLine("Vrednost popusta mora biti med 0 in 100!");
            }
            else
            {
                List<Artikel> seznamArtiklovVAkciji = seznamArtiklov.Select(a => new Artikel(a.ime, a.cena, a.zaloga, a.dobavitelj)).ToList();
                foreach (Artikel artikel in seznamArtiklovVAkciji)
                {
                    artikel.cena = artikel.cena * (1- int.Parse(vrednost)/100.0);
                }

                string imeDatoteke = $"Artikli_v_akciji_{Guid.NewGuid().ToString("N")[..8]}.txt";

                ShraniVDatoteko(@"..\..\..\" + imeDatoteke, seznamArtiklovVAkciji);
            }
        }
    }
}