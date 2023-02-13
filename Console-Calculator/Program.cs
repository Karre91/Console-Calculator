using System;
using System.Collections.Generic; //För att kunna använda List

namespace Miniräknare
{
	class Program
	{
		/*Metod som skriver ut menyn, den har ingen in eller ut data. Dvs ingenting mellan (), void = ingen ut data.
        Den bara skriver ut till skärmen.*/
		static void Meny()
		{
			Console.WriteLine("\t------ Meny ------");
			Console.WriteLine("\t[G]ör en uträkning");
			Console.WriteLine("\t[V]isa historiken");
			Console.WriteLine("\t[A]vsluta");
			Console.Write("\tDitt val: ");
		}

		/*Denna metod har string som in-parameter, den skriver ut samma string med röd text.*/
		static void RedWarningMsg(string msg)
		{
			Console.ForegroundColor = ConsoleColor.Red; //Röd textfärg vid felmeddelande
			Console.Write(msg); //Skriver ut stringen som kom in i metoden skrivs ut igen 
			Console.ResetColor(); //Återställ till vit textfärg
		}

		//Metod som skriver ut operator-menyn. Fungerar som meny-metoden ovan
		static void Operator()
		{
			Console.WriteLine("\n\tVälj en operator från listan");
			Console.WriteLine("\t\t[+] - Plus");
			Console.WriteLine("\t\t[-] - Minus");
			Console.WriteLine("\t\t[*] - Gånger");
			Console.WriteLine("\t\t[/] - Delat med");
			Console.Write("\t\tDitt val: ");
		}

		/*Metod som kontrollerar användarens input av meny-val. Denna har både in och ut data. In kommer en string som jag döpt till choiceIs.
        Ut kommer en bool, är den true = input ok. False = input EJ ok.*/
		static bool MenyInputControl(string choiceIs)
		{
			if (choiceIs == "G" || choiceIs == "V" || choiceIs == "A")
			{
				return true; //Om det är något utav ovanstående, returnera en bool med värdet true.
			}
			else
			{
				return false; //Annars, returnera false.
			}
		}

		//Metod som kontrolerar användarens input av räknesätts-val. Fungerar som metoden ovan
		static bool OperatorInputControl(string choiceIs)
		{

			if (choiceIs == "+" || choiceIs == "-" || choiceIs == "*" || choiceIs == "/" || choiceIs == "MARCUS")
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/*Metod som gör uträkningen, skriver ut felmeddelande vid division med 0, eller om talet är för stort
         och sparar hela uträkningen i en string som returneras.*/
		static string ProfessorKalkyl(decimal nbrOne, decimal nbrTwo, string op)
		{
			decimal product = 0; //Variabel för produkten           
			bool calculationOk = true; //Kontroll variabel
			string all = ""; //Sammanställning av uträkning för historiken

			if (op == "+")
				product = nbrOne + nbrTwo; //Räknar ut produkten           
			else if (op == "-")
				product = nbrOne - nbrTwo; //Räknar ut produkten 
			else if (op == "*")
				product = nbrOne * nbrTwo; //Räknar ut produkten 
			else if (op == "/")
			{
				if (nbrTwo == 0) //Om nr 2 är 0
				{
					RedWarningMsg("\n\tFörsök inte dividera med 0 din nolla.\n\n");
					calculationOk = false; //För if-satsen längre ner. Försöker man dela man med 0 sätts variabeln som false.
				}
				else
					product = nbrOne / nbrTwo; //Räknar ut produkten 
			}

			if (product > 1000000000000000000 || product < -1000000000000000000) //Summan av uträkningen får inte vara större än såhär.
			{
				RedWarningMsg("\n\tSumman av dina tal är för stor för denna miniräknare.\n\n");

				all = $"{nbrOne} {op} {nbrTwo} = För stort tal"; //Sparar talen och texten i en string    
				return all; //returnerar stringen 
			}

			if (calculationOk) //Om ok är true (Tal 2 var inte 0)
			{
				Console.WriteLine($"\n\tUträkningen av dina tal är: {product}\n"); //Skriver ut summan               
				all = $"{nbrOne} {op} {nbrTwo} = {product}"; //Sparar talen och summan i en string 
			}
			else if (!calculationOk) //Om ok är false, dvs tal 2 är 0
			{
				all = $"{nbrOne} {op} {nbrTwo} = Du kan inte dela med 0!"; //Sparar talen och mitt svar i en string                
			}
			return all; //returnerar stringen 
		}

		//Här börjar mitt program
		static void Main(string[] args)
		{
			//Deklarera variablar
			string userChoice; //Användarvalet i menyn och räknesätts operatorn sparas här.
			string inputNumber; //Användarens input av siffror sparas här
			decimal inputNumberOneIsNumber = 0, inputNumberTwoIsNumber = 0; //Tal 1 och Tal 2, Out från TryParse hamnar här.

			//Historiken 
			List<String> AllMyCalculations = new List<String>(); //Innehåller en string för varje uträkning.

			Console.ForegroundColor = ConsoleColor.Green; //Grön textfärg
														  //Hälsa användaren välkommen i början av programmet.
			Console.WriteLine("\t---------------------------------");
			Console.WriteLine("\t------ KARINS MINIRÄKNARE -------");
			Console.WriteLine("\t---------------------------------\n");
			Console.ResetColor(); //Vit textfärg igen

			bool endMeny = false; //Variabel, min while loop för programmet.
			while (!endMeny)
			{
				Meny(); //Kallar på metoden Meny() som skriver ut.
				userChoice = Console.ReadLine().ToUpper(); //läser in användarens input, gör till stor bokstav och sparar i en sträng.

				if (!MenyInputControl(userChoice))
				{
					RedWarningMsg("\n\tVänligen välj ett av alternativen i menyn.\n\n");
				}

				/*Om vi inte går in i if-satsen, dvs meny input va okej hamnar vi här*/
				else
				{
					switch (userChoice) //Beroende på vad användaren valt i menyn.
					{/*Switch är smidigare att använda istället för flera if och else if, Mindre kod = lättare att läsa.*/

						case "G": //Gör en uträkning
							Console.Clear(); //Rensar fönsteret

							Console.WriteLine("\t------ Uträkning ------");

							//Ber om det första talet
							Console.Write("\tAnge ditt första tal: ");
							inputNumber = Console.ReadLine(); //Läser input och sparar i en string

							bool endCheck = false;
							while (!endCheck)
							{
								int minusIndex = inputNumber.IndexOf("-", 0); //Kollar om det finns något minustecken framför input talet dvs på index 0

								int allowedLength = 18; //Såhär många siffror tillåter jag min miniräknare att jobba med

								int stringLength; //Variabel som håller koll på hur lång input faktiskt var

								if (minusIndex == 0) //Om det ligger ett minus innan input av tal börjar
								{
									allowedLength = 19; //Längden får vara längre, minus är inte del av siffrorna
								}

								int commaIndex = inputNumber.IndexOf(",", 0); //Vilket index ligger kommatecken på
								if (commaIndex == -1) // -1 betyder att det inte finns något kommatecken
								{
									stringLength = inputNumber.Length; //Räknar ut längden av användar input
									if (stringLength > allowedLength) //Om användaren skriver in ett tal som är längre än den tillåtna längden (15 eller 16)
									{
										RedWarningMsg("\n\tTal för långt. Försök igen: "); //Röd varningstext                                          
										inputNumber = Console.ReadLine(); //Sparar ny input 
										continue; //Tillbaka till början av whileloopen
									}
								}
								//Det finns ett kommatecken.  Här hade jag kunnat ha else if istället med ifsatsen som finns innuti,
								else //men det blir tydligare när man delar upp dem.
								{
									if (commaIndex > allowedLength) //Platsen för kommatecken får inte vara större än den tillåtna längden på talet
									{
										RedWarningMsg("\n\tTal för långt. Försök igen: "); //Röd varningstext
										inputNumber = Console.ReadLine(); //Sparar ny input 
										continue; //Tillbaka till början av whileloopen
									}
								}

								if (!decimal.TryParse(inputNumber, out inputNumberOneIsNumber))
								{
									RedWarningMsg("\n\tNågonting blev fel i din innmatning. Försök igen: "); //Röd varningstext                                   
									inputNumber = Console.ReadLine(); //Sparar ny input 
									continue; //Tillbaka till början av whileloopen
								}
								/*Användarens input innehåller ingenting konstigt, och är inte för långt.*/
								endCheck = true; //Allt gick bra. Avslutar loopen
							}

							//Input 1 är OK, vi går vidare till räknesätts menyn                           
							Operator(); //Metod som skriver ut räknesätten

							userChoice = Console.ReadLine().ToUpper();

							while (!(OperatorInputControl(userChoice)))
							{
								RedWarningMsg("\n\tVänligen välj en giltig operator i listan: "); //Röd varningstext                                       
								userChoice = Console.ReadLine().ToUpper(); //läser in användarens input, gör till stor bokstav (om det är bokstäver) och sparar i en string.
							}

							/*Om du skrivit marcus ist för operator, så skrivs en text ut. Men vi avslutar inte programmet,
                            eftersom vi har allting efter inom en else så hoppar vi bara ner till break, och menyn skrivs ut igen.*/
							if (userChoice == "MARCUS")
							{
								Console.ForegroundColor = ConsoleColor.Cyan;
								Console.WriteLine("\n\tHej Marcus!\n");
								Console.ResetColor();
							}

							//Skrev du INTE marcus händer detta
							else
							{
								//Ber om tal nummer 2
								Console.Write("\n\tAnge ditt andra tal: ");
								inputNumber = Console.ReadLine(); //Sparar i samma variabel som vi hade för tal 1

								/* Här används exakt samma typ av kontroll som för tal 1, Skulle kanske kunna lägga in detta i en metod istället*/
								endCheck = false;
								while (!endCheck)
								{
									int minusIndex = inputNumber.IndexOf("-", 0); //Kollar om det finns något minustecken framför input talet

									int allowedLength = 18; //Såhär många siffror tillåter jag min miniräknare att jobba med

									int stringLength; //Variabel som håller koll på hur lång input faktiskt var

									if (minusIndex == 0) //Om det ligger ett minus innan input av tal börjaar
									{
										allowedLength = 19; //Längden får vara längre, minus är inte del av siffrorna
									}

									int commaIndex = inputNumber.IndexOf(",", 0); //Vilket index ligger kommatecken på
									if (commaIndex == -1) // -1 betyder att det inte finns något kommatecken
									{
										stringLength = inputNumber.Length; //Räknar ut längden av användar input
										if (stringLength > allowedLength) //Om användaren skriver in ett tal som är längre än den tillåtna längden (15 eller 16)
										{
											RedWarningMsg("\n\tTal för långt. Försök igen: ");
											inputNumber = Console.ReadLine(); //Sparar input 
											continue; //Tillbaka till början av whileloopen
										}
									}

									else //Det finns ett kommatecken
									{
										if (commaIndex > allowedLength) //Platsen för kommatecken får inte vara större än den tillåtna längden på talet
										{
											RedWarningMsg("\n\tTal för långt. Försök igen: ");
											inputNumber = Console.ReadLine(); //Sparar input 
											continue; //Tillbaka till början av whileloopen
										}
									}

									if (!decimal.TryParse(inputNumber, out inputNumberTwoIsNumber))
									{
										RedWarningMsg("\n\tNågonting blev fel i din innmatning. Försök igen: ");
										inputNumber = Console.ReadLine(); //Sparar input 
										continue; //Tillbaka till början av whileloopen
									}
									endCheck = true; //Avslutar loopen
								}

								//Input 2 är också ok. Vi räknar ut och lägger till i listan via metoden.
								AllMyCalculations.Add(ProfessorKalkyl(inputNumberOneIsNumber, inputNumberTwoIsNumber, userChoice));
								//Uträkningen är klar och sparad.                    
							}
							break;


						case "V": //Visa historik                       
							Console.Clear(); //Rensa fönster

							Console.WriteLine("\t------ Historik ------");
							//Om det inte har gjorts några uträkningar
							if (AllMyCalculations.Count == 0)
							{
								RedWarningMsg("\n\tDu har inte gjort några uträkningar ännu.\n\tVänligen välj på nytt i menyn.\n\n");
							}

							for (int i = 0; i < AllMyCalculations.Count; i++)
							{
								Console.WriteLine($"\t{AllMyCalculations[i]}");

								//Lägger till en rad efter att all historik skrivits ut
								if (i == AllMyCalculations.Count - 1)
								{
									Console.WriteLine();
								}
							}
							break;

						case "A": //Avsluta
							Console.Clear(); //Rensa fönsteret 
							Console.WriteLine("\t------ Hejdå! ------");
							endMeny = true; //Avslutar programmet
							break;
					}
				}
			}
		}
	}
}
