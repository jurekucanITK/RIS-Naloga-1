using Naloga_1;

const string pot = @"..\..\..\Artikli.txt";
List<Artikel> seznamArtiklov = Operacije.ShraniVSeznamArtiklov(Operacije.PreberiIzDatoteke(pot));
int izbira;

do
{
    Operacije.GlavnaNavodila();

    if (!int.TryParse(Console.ReadLine(), out izbira))
    {
        Console.WriteLine("Izbira mora biti ena od navedenih številk v navodilu! \n");
    }
    else
    {
        switch (izbira)
        {
            case 1:
                Operacije.VnosArtikla(pot, seznamArtiklov);
                break;

            case 2:
                Operacije.IzpisiArtikle(seznamArtiklov);
                break;
            case 3:
                Operacije.UrediArtikle(seznamArtiklov);
                break;

            default:
                Console.WriteLine("Izbira mora biti ena od navedenih številk v navodilu! \n");
                break;
        }
    }

} while (izbira != 4);