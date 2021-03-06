﻿using System;
using System.Collections.Generic;
using System.IO;

namespace PrzygotowanieKolos
{
	public class Konto {
		private decimal StanKonta = 0;
		private List<string> wyciag = new List<string>();

		public void Wplata(decimal wplata) {
			if (wplata > 0)
			{
				this.StanKonta += wplata;
				wyciag.Add("Dokonano wplaty na kwote " + wplata);
			}
			else
				throw new ArgumentOutOfRangeException("Podano ujemną kwotę");
		}

		public void Wyplata(decimal wyplata)
		{
			if (wyplata < 0)
			{
				throw new ArgumentOutOfRangeException("Podano ujemną kwotę");
			}
			else if (wyplata > this.StanKonta)
				throw new ArgumentOutOfRangeException("Masz za mało środków");
			else
			{
				this.StanKonta -= wyplata;
			
				wyciag.Add("Dokonano wyplaty na kwote " + wyplata);
			}
		}
		// Pokazanie jak działa zapisywanie do pliku
		public void GenerujWyciag() {
			using (StreamWriter sw = new StreamWriter("wyciag.txt"))
			{
				foreach (string element in wyciag)
				{
					sw.WriteLine(element);
				}
			}
		}
		public decimal ZwrocStanKonta() {
			return this.StanKonta;
		}
	}
	enum menu {Wplata, Wyplata, GenerujWyciag, WyswietlStan, Zakoncz};
	class Program
	{
		static void Main(string[] args)
		{
			Konto testowe = new Konto();
			menu wybor;
			bool sprawdzCzyWyjsc = true;
			try {
				while (sprawdzCzyWyjsc)
				{
					
					Console.WriteLine("Wybierz 0, aby wplacic, \n 1, aby wyplacic, \n 2, aby wysiwetlic wyciag \n 3, aby wsywietlic stan konta \n 4, aby zakonczyc");
					while (!menu.TryParse(Console.ReadLine(), out wybor))
					{
						Console.WriteLine("Podałeś nie poprawną wartość");
					}
					
					
					switch (wybor)
					{
						case menu.Wplata:
							decimal Mikolaj = 0;
							Console.WriteLine("Podaj kwotę jaką chcesz wpłacić na konto");
							while (!decimal.TryParse(Console.ReadLine(), out Mikolaj))
							{
								Console.WriteLine("Podałeś nie poprawną wartość");
							}

								testowe.Wplata(Mikolaj);


							break;
						case menu.Wyplata:
							decimal wyplata = 0;
							Console.WriteLine("Podaj kwotę jaką chcesz wypłacić z konta");
							while (!decimal.TryParse(Console.ReadLine(), out wyplata))
							{
								Console.WriteLine("Podałeś nie poprawną wartość");
							}
							testowe.Wyplata(wyplata);
							break;
						case menu.GenerujWyciag:
							//Pokazanie jak działa wczytywanie iformacji z pliku i wyświetlanie ich z konsoli
							testowe.GenerujWyciag();
							using (StreamReader sr = new StreamReader("wyciag.txt"))
							{
								string line = "";
								while ((line = sr.ReadLine()) != null)
								{
									Console.WriteLine(line);
								}
							}
							break;
						case menu.WyswietlStan:
							Console.WriteLine("Twoj stan konta to: " + testowe.ZwrocStanKonta());
							break;
						case menu.Zakoncz:
							sprawdzCzyWyjsc = false;
							break;
					}
					
				}
			}
			catch(Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}
	}
}
