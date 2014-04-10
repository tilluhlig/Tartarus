// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 08-06-2013
// ***********************************************************************
// <copyright file="Inventar.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _4_1_
{
    /// <summary>
    /// Diese Klasse verwaltet Inventare
    /// </summary>
    public class Inventar
    {
        #region Listen

        // Die beiden Listen für die Items (Konsum und Upgrade)
        /// <summary>
        /// Konsumierbaes
        /// </summary>
        public List<Item> Konsumierbares = new List<Item>();

        /// <summary>
        /// Upgrades
        /// </summary>
        public List<Item> Upgrades = new List<Item>();

        #endregion Listen

        /// <summary>
        /// Das ist die Fachgröße für Upgrades und Konsumierbares
        /// </summary>
        public int Fachgroesse = 10;

        /// <summary>
        /// die maximale Anzahl an Fächern im Inventar
        /// </summary>
        public int MaxFaecher = 100;

        /// <summary>
        /// Ein Zeiger auf die Munition des Spielers bzw des Inventarbesitzers
        /// </summary>
        public List<int> Munition; // Die Munition

        /// <summary>
        /// Die Menge an Treibstoff im Inventar
        /// </summary>
        public float Treibstoff;

        /// <summary>
        /// Der Konstruktor des Inventars
        /// </summary>
        /// <param name="_MaxFächer">maximale Anzahl an Fächern</param>
        /// <param name="_Munition">Ein zeiger auf eine Munitionsliste</param>
        /// <param name="_Treibstoff">initial Treibstoff</param>
        public Inventar(int _MaxFächer, List<int> _Munition, float _Treibstoff)
        {
            InventarInit(_MaxFächer, _Munition, _Treibstoff);
        }

        /// <summary>
        /// Initialisierungsfunktion, wird vom Konstruktor aufgerufen
        /// </summary>
        /// <param name="_MaxFächer">maximale Anzahl an Fächern</param>
        /// <param name="_Munition">Ein zeiger auf eine Munitionsliste</param>
        /// <param name="_Treibstoff">initial Treibstoff</param>
        private void InventarInit(int _MaxFächer, List<int> _Munition, float _Treibstoff)
        {
            MaxFaecher = _MaxFächer;
            if (_Munition != null)
            {
                Munition = _Munition;
            }
            else
            {
                Munition = new List<int>();
                for (int i = 0; i < Waffendaten.Verschiessbar.Count(); i++)
                    Munition.Add(0);
            }

            Treibstoff = _Treibstoff;
        }

        /// <summary>
        /// Der Konstruktor
        /// </summary>
        public Inventar()
        { 
            InventarInit(0, null, 0);
        }

        /// <summary>
        /// Erstellt neuen Speicher anhand eines existierenden Items (kopiert das Item)
        /// </summary>
        /// <param name="Objekt">Das zu kopierende item</param>
        /// <returns>Gibt ein Itemobjekt zurück, kopie von "Objekt"</returns>
        public static Item Neu(Item Objekt)
        {
            return new Item(Objekt.Name, Objekt.Preis, Objekt.Effekt, Objekt.Anzahl, Objekt.Typ);
        }

        /// <summary>
        /// Benutzt ein Konsumitem und aktiviert dessen Effekt (Item)
        /// </summary>
        /// <param name="Effekte">Das Effektpacket, auf welches das Item angewendet wird</param>
        /// <param name="KonsumierbaresItem">das Item, welchees genutzt wird</param>
        /// <returns>
        /// true = konnte genutzt werden, false = war leer
        /// </returns>
        public bool BenutzenKonsumierbares(EffectPacket Effekte, Item KonsumierbaresItem)
        {
            if (KonsumierbaresItem.Anzahl > 0)
            {
                Effekte.Hinzufügen(KonsumierbaresItem.Effekt);
                KonsumierbaresItem.Verringern();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Benutzt ein Konsumitem und aktiviert dessen Effekt (über id aus "List-Item- Konsumierbares")
        /// </summary>
        /// <param name="Effekte">Das Effektpacket, auf welches das Item angewendet wird</param>
        /// <param name="KonsumierbaresItem">id des Items, welches benutzt wird</param>
        /// <returns>true = konnte genutzt werden, false = war leer oder falsche id</returns>
        public bool BenutzenKonsumierbares(EffectPacket Effekte, int id)
        {
            if (id < 0 || id >= Konsumierbares.Count) return false;

            if (Konsumierbares[id].Anzahl > 0)
            {
                Effekte.Hinzufügen(Konsumierbares[id].Effekt);

                if (!Konsumierbares[id].Verringern())
                    Konsumierbares.RemoveAt(id);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Benutzt ein Upgrade und aktiviert dessen Effekt (Item)
        /// </summary>
        /// <param name="Effekte">Das Effektpacket, auf welches das Item angewendet wird</param>
        /// <param name="UpgradeItem">das Item, welches genutzt wird</param>
        /// <returns>true = konnte genutzt werden, false = war leer oder bereits vorhanden</returns>
        public bool BenutzenUpgrade(EffectPacket Effekte, Item UpgradeItem)
        {
            for (int i = 0; i < Effekte.Upgrades.Count; i++)
            {
                if (Effekte.Upgrades[i] == UpgradeItem.Effekt) return false;
            }

            if (UpgradeItem.Anzahl > 0)
            {
                Effekte.Hinzufügen(UpgradeItem.Effekt);

                UpgradeItem.Verringern();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Benutzt ein Upgrade und aktiviert dessen Effekt (über id aus "List-Item- Upgrades")
        /// </summary>
        /// <param name="Effekte">Das Effektpacket, auf welches das Item angewendet wird</param>
        /// <param name="id">id des Items, welches genutzt wird</param>
        /// <returns>true = konnte genutzt werden, false = war leer oder bereits vorhanden oder id falsch</returns>
        public bool BenutzenUpgrade(EffectPacket Effekte, int id)
        {
            if (id < 0 || id >= Upgrades.Count) return false;
            for (int i = 0; i < Effekte.Upgrades.Count; i++)
            {
                if (Effekte.Upgrades[i] == Upgrades[id].Effekt) return false;
            }

            if (Upgrades[id].Anzahl > 0)
            {
                Effekte.Hinzufügen(Upgrades[id].Effekt);

                if (!Upgrades[id].Verringern())
                    Upgrades.RemoveAt(id);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// gibt die id in der Konsumliste anhand des "Objekt" zurück
        /// </summary>
        /// <param name="Objekt">das zu suchende Objekt</param>
        /// <returns>id des Objekts in List-Item- Konsumierbares, -1 wenn es nicht vorhanden ist</returns>
        public int FindKonsumierbares(Item Objekt)
        {
            for (int i = 0; i < Konsumierbares.Count; i++)
                if (Konsumierbares[i].Name == Objekt.Name)
                    return i;
            return -1;
        }

        /// <summary>
        /// gibt die id in der Upgradeliste anhand des "Objekt" zurück
        /// </summary>
        /// <param name="Objekt">das zu suchende Objekt</param>
        /// <returns>id des Objekts in List-Item- Upgrades, -1 wenn es nicht vorhanden ist</returns>
        public int FindUpgrades(Item Objekt)
        {
            for (int i = 0; i < Upgrades.Count; i++)
                if (Upgrades[i].Name == Objekt.Name)
                    return i;
            return -1;
        }

        /// <summary>
        /// Gibt die im Inventar belegten Fächer zurück
        /// </summary>
        /// <returns>Anzahl der belegten Fächer</returns>
        public int GibAnzahlBelegterFächer()
        {
            int result = 0;

            // Tribstoff
            result++;

            // Upgrades
            result += GibUpgradeFächer();

            // Konsumierbares
            result += GibKonsumierbareFächer();

            // Munition
            result += GibMunitionsFächer();

            return result;
        }

        /// <summary>
        /// Gibt die Anzahl der durch Konsumierbares belegten Fächer zurück
        /// </summary>
        /// <returns>Gibt die durch Konsumierbares belegten Fächerzahl zurück</returns>
        public int GibKonsumierbareFächer()
        {
            int result = 0;
            for (int i = 0; i < Konsumierbares.Count; i++)
                result += (int)(Math.Ceiling((double)Konsumierbares[i].Anzahl / Fachgroesse));
            return result;
        }

        /// <summary>
        ///  /// Gibt eine Liste der Konusmierbaren zurück
        /// </summary>
        /// <returns>Liste der Konsumierbaren im Inventar (id in "List-Item- Konsumierbares", Anzahl)</returns>
        public List<Vector2> GibListeKonsumierbares()
        {
            List<Vector2> list = new List<Vector2>();
            for (int i = 0; i < Konsumierbares.Count; i++)
            {
                int a = Konsumierbares[i].Anzahl;
                int temp = (int)(Math.Ceiling((double)a / Fachgroesse));
                for (int b = 0; b < temp - 1; b++)
                    list.Add(new Vector2(i, Fachgroesse));

                if (a > 0)
                {
                    if (a % Fachgroesse != 0)
                    {
                        list.Add(new Vector2(i, a % Fachgroesse));
                    }
                    else
                        list.Add(new Vector2(i, Fachgroesse));
                }
            }
            return list;
        }

        /// <summary>
        /// Gibt Vector (i, anzahl) des Inhalts zurück, dabei werden nicht belegte fächer übersprungen (ohne Klasse 0, nur verschiessbare).
        /// </summary>
        /// <param name="id">id einer vorhandenen Munition</param>
        /// <returns>vector2 (Position in List-int- Munition, anzahl), (-1,0) wenn nicht vorhanden </returns>
        public Vector2 GibMunition(int id)
        {
            if (Munition == null) return new Vector2(-1, 0);
            int anz = 0;
            for (int i = 0; i < Munition.Count(); i++)
                if (Munition[i] > 0 && Waffendaten.Verschiessbar[i] != 0 && Waffendaten.Verschiessbar[i] != 5)
                {
                    if (anz == id)
                    {
                        return new Vector2(i, Munition[anz]);
                    }
                    anz++;
                }
            return new Vector2(-1, 0);
        }

        /// <summary>
        /// Gibt Vector (i, anzahl) des Inhalts zurück, dabei werden nicht belegte fächer übersprungen (auch nicht verschiessbare)
        /// </summary>
        /// <param name="id">id einer vorhandenen Munition</param>
        /// <returns>vector2 (Position in List-int- Munition, anzahl), (-1,0) wenn nicht vorhanden</returns>
        public Vector2 GibMunitionAlle(int id)
        {
            if (Munition == null) return new Vector2(-1, 0);
            int anz = 0;
            for (int i = 0; i < Munition.Count(); i++)
                if (Munition[i] > 0)
                {
                    if (anz == id)
                    {
                        return new Vector2(i, Munition[anz]);
                    }
                    anz++;
                }
            return new Vector2(-1, 0);
        }

        /// <summary>
        /// Gibt die Anzahl an belegten Munitionsfächern zurück (ohne Klasse 0, nur verschiessbares)
        /// </summary>
        /// <returns>die anzahl</returns>
        public int GibMunitionsarten()
        {
            if (Munition == null) return 0;
            int anz = 0;
            for (int i = 0; i < Munition.Count(); i++) if (Munition[i] > 0 && Waffendaten.Verschiessbar[i] != 0 && Waffendaten.Verschiessbar[i] != 5) anz++;
            return anz;
        }

        /// <summary>
        /// Gibt die Anzahl an belegten Munitionsfächern zurück, auch nicht verschiessbares
        /// </summary>
        /// <returns>die anzahl</returns>
        public int GibMunitionsartenAlle()
        {
            if (Munition == null) return 0;
            int anz = 0;
            for (int i = 0; i < Munition.Count(); i++) if (Munition[i] > 0) anz++;
            return anz;
        }

        /// <summary>
        /// Gibt die Anzahl der durch Munition belegten Fächer zurück (nur verschiessbares)
        /// </summary>
        /// <returns>Gibt die durch Munition belegten Fächer zurück</returns>
        public int GibMunitionsFächer()
        {
            int result = 0;
            for (int i = 0; i < Munition.Count; i++)
            {
                if (Munition[i] <= 0 || Waffendaten.Verschiessbar[i] == 0 || Waffendaten.Verschiessbar[i] == 5) continue;
                result += (int)(Math.Ceiling((double)Munition[i] / Waffendaten.Fachgröße[i]));
            }

            return result;
        }

        /// <summary>
        /// Gibt die Anzahl der durch Munition belegten Fächer zurück (alle, auch nicht verschiessbare)
        /// </summary>
        /// <returns>Gibt die durch Munition belegten Fächer zurück</returns>
        public int GibMunitionsFächerAlle()
        {
            int result = 0;
            for (int i = 0; i < Munition.Count; i++)
            {
                if (Munition[i] <= 0) continue;
                result += (int)(Math.Ceiling((double)Munition[i] / Waffendaten.Fachgröße[i]));
            }

            return result;
        }

        /// <summary>
        /// Gibt eine Liste der Munition zurück (nur verschiessbare)
        /// </summary>
        /// <returns>Liste der Munition im Inventar (id in "List-int- Munition", Anzahl) </returns>
        public List<Vector2> GibMunitionsliste()
        {
            List<Vector2> list = new List<Vector2>();
            for (int i = 0; i < Munition.Count; i++)
            {
                if (Munition[i] <= 0 || Waffendaten.Verschiessbar[i] == 0 || Waffendaten.Verschiessbar[i] == 5) continue;
                int a = Munition[i];
                int temp = (int)(Math.Ceiling((double)a / Waffendaten.Fachgröße[i]));
                for (int b = 0; b < temp - 1; b++)
                    list.Add(new Vector2(i, Waffendaten.Fachgröße[i]));

                if (a > 0)
                {
                    if (a % Waffendaten.Fachgröße[i] != 0)
                    {
                        list.Add(new Vector2(i, a % Waffendaten.Fachgröße[i]));
                    }
                    else
                        list.Add(new Vector2(i, Waffendaten.Fachgröße[i]));
                }
            }
            return list;
        }

        /// <summary>
        /// Gibt eine Liste der Munition zurück (auch nicht verschiessbares)
        /// </summary>
        /// <returns>Liste der Munition im Inventar (id in "List-int- Munition", Anzahl) </returns>
        public List<Vector2> GibMunitionslisteAlle()
        {
            List<Vector2> list = new List<Vector2>();
            for (int i = 0; i < Munition.Count; i++)
            {
                if (Munition[i] <= 0) continue;
                int a = Munition[i];
                int temp = (int)(Math.Ceiling((double)a / Waffendaten.Fachgröße[i]));
                for (int b = 0; b < temp - 1; b++)
                    list.Add(new Vector2(i, Waffendaten.Fachgröße[i]));

                if (a > 0)
                {
                    if (a % Waffendaten.Fachgröße[i] != 0)
                    {
                        list.Add(new Vector2(i, a % Waffendaten.Fachgröße[i]));
                    }
                    else
                        list.Add(new Vector2(i, Waffendaten.Fachgröße[i]));
                }
            }
            return list;
        }

        /// <summary>
        /// Gibt eine Liste der Upgrades zurück
        /// </summary>
        /// <returns>Liste der Upgrades im Inventar (id in "List-Item- Upgrades", Anzahl) </returns>
        public List<Vector2> GibtListeUpgrades()
        {
            List<Vector2> list = new List<Vector2>();
            for (int i = 0; i < Upgrades.Count; i++)
            {
                int a = Upgrades[i].Anzahl;
                int temp = (int)(Math.Ceiling((double)a / Fachgroesse));
                for (int b = 0; b < temp - 1; b++)
                    list.Add(new Vector2(i, Fachgroesse));

                if (a > 0)
                {
                    if (a % Fachgroesse != 0)
                    {
                        list.Add(new Vector2(i, a % Fachgroesse));
                    }
                    else
                        list.Add(new Vector2(i, Fachgroesse));
                }
            }
            return list;
        }

        /// <summary>
        /// Gibt die Menge an Treibstoff im Inventar zurück
        /// </summary>
        /// <returns>die Menge an Treibstoff</returns>
        public float GibTreibstoff()
        {
            return Treibstoff;
        }

        /// <summary>
        /// Gibt die Anzahl der durch den Treibstoff belegten Fächer zurück
        /// </summary>
        /// <returns>Gibt die durch den Treibstoff belegten Fächer zurück</returns>
        public int GibTreibstoffFächer()
        {
            // es wird nur ein Fach benötigt
            int result = 1;
            return result;
        }

        /// <summary>
        /// Gibt die Anzahl der durch Upgrades belegten Fächer zurück
        /// </summary>
        /// <returns>Gibt die durch Upgrades belegten Fächer zurück</returns>
        public int GibUpgradeFächer()
        {
            int result = 0;
            for (int i = 0; i < Upgrades.Count; i++)
                result += (int)(Math.Ceiling((double)Upgrades[i].Anzahl / Fachgroesse));
            return result;
        }

        /// <summary>
        /// Entnimmt eine Anzahl von Upgrades
        /// </summary>
        /// <param name="id">id in List-Item- Upgrades</param>
        /// <param name="Anzahl">die menge</param>
        /// <returns>Anzahl derer, die er enthehmen konnte</returns>
        public int EntnehmenUpgrade(int id, int Anzahl)
        {
            if (Upgrades[id].Anzahl >= Anzahl)
            {
                Upgrades[id].Anzahl -= Anzahl;
                return 0;
            }

            Anzahl = Upgrades[id].Anzahl;
            Upgrades[id].Anzahl = 0;
            return Anzahl;
        }

        /// <summary>
        /// Entnimmt eine Anzahl von Konsumierbaren
        /// </summary>
        /// <param name="id">id in List-Item- Konsumierbares</param>
        /// <param name="Anzahl">die menge</param>
        /// <returns>Anzahl derer, die er enthehmen konnte</returns>
        public int EntnehmenKonsumierbares(int id, int Anzahl)
        {
            if (Konsumierbares[id].Anzahl >= Anzahl)
            {
                Konsumierbares[id].Anzahl -= Anzahl;
                return 0;
            }

            Anzahl = Konsumierbares[id].Anzahl;
            Konsumierbares[id].Anzahl = 0;
            return Anzahl;
        }

        /// <summary>
        /// Fügt ein Item ins Inventar ein (ändert Original)
        /// </summary>
        /// <param name="Objekt">das einzufügende Objekt</param>
        /// <returns>Gibt die Anzahl der Objekt zurück, die nicht aufgenommen werden konnten</returns>
        public int Hinzufügen(ref Item Objekt)
        {
            if (FindUpgrades(Objekt) >= 0)
            {
                int id = FindUpgrades(Objekt);
                for (int i = 0; i < Objekt.Anzahl; i++)
                {
                    Upgrades[id].Erhöhen();
                    if (GibAnzahlBelegterFächer() > MaxFaecher)
                    {
                        Upgrades[id].Verringern();
                        Objekt.Anzahl -= i;
                        return Objekt.Anzahl;
                    }
                }
                return 0;
            }
            else
                if (FindKonsumierbares(Objekt) >= 0)
                {
                    int id = FindKonsumierbares(Objekt);
                    for (int i = 0; i < Objekt.Anzahl; i++)
                    {
                        Konsumierbares[id].Erhöhen();
                        if (GibAnzahlBelegterFächer() > MaxFaecher)
                        {
                            Konsumierbares[id].Verringern();
                            Objekt.Anzahl -= i;
                            return Objekt.Anzahl;
                        }
                    }
                    return 0;
                }
                else
                {
                    // Objekt noch nicht im Inventar
                    Item a = Neu(Objekt);
                    int anz = Objekt.Anzahl;
                    a.Anzahl = 1;

                    if (a.Typ == 0)
                    {
                        Upgrades.Add(a);
                        if (GibAnzahlBelegterFächer() > MaxFaecher)
                        {
                            Upgrades.Remove(a);
                        }

                        int id = Upgrades.Count - 1;
                        for (int i = 0; i < anz - 1; i++)
                        {
                            Upgrades[id].Erhöhen();
                            if (GibAnzahlBelegterFächer() > MaxFaecher)
                            {
                                Upgrades[id].Verringern();
                                Objekt.Anzahl -= i - 1;
                                return Objekt.Anzahl;
                            }
                        }
                        return 1;
                    }
                    else
                        if (a.Typ == 1)
                        {
                            Konsumierbares.Add(a);
                            if (GibAnzahlBelegterFächer() > MaxFaecher)
                            {
                                Konsumierbares.Remove(a);
                            }

                            int id = Konsumierbares.Count - 1;
                            for (int i = 0; i < anz - 1; i++)
                            {
                                Konsumierbares[id].Erhöhen();
                                if (GibAnzahlBelegterFächer() > MaxFaecher)
                                {
                                    Konsumierbares[id].Verringern();
                                    Objekt.Anzahl -= i - 1;
                                    return Objekt.Anzahl;
                                }
                            }
                            return 1;
                        }
                }

            return Objekt.Anzahl;
        }

        /// <summary>
        /// Fügt ein Item ins Inventar ein
        /// </summary>
        /// <param name="Objekt">The objekt.</param>
        /// <returns>Gibt die Anzahl der Objekt zurück, die nicht aufgenommen werden konnten</returns>
        public int Hinzufügen(Item Objekt)
        {
            if (FindUpgrades(Objekt) >= 0)
            {
                int id = FindUpgrades(Objekt);
                for (int i = 0; i < Objekt.Anzahl; i++)
                {
                    Upgrades[id].Erhöhen();
                    if (GibAnzahlBelegterFächer() > MaxFaecher)
                    {
                        Upgrades[id].Verringern();
                        return Objekt.Anzahl - i;
                    }
                }
                return 0;
            }
            else
                if (FindKonsumierbares(Objekt) >= 0)
                {
                    int id = FindKonsumierbares(Objekt);
                    for (int i = 0; i < Objekt.Anzahl; i++)
                    {
                        Konsumierbares[id].Erhöhen();
                        if (GibAnzahlBelegterFächer() > MaxFaecher)
                        {
                            Konsumierbares[id].Verringern();
                            return Objekt.Anzahl - i;
                        }
                    }
                    return 0;
                }
                else
                {
                    // Objekt noch nicht im Inventar
                    Item a = Neu(Objekt);
                    int anz = Objekt.Anzahl;
                    a.Anzahl = 1;

                    if (a.Typ == 0)
                    {
                        Upgrades.Add(a);
                        if (GibAnzahlBelegterFächer() > MaxFaecher)
                        {
                            Upgrades.Remove(a);
                            return 0;
                        }

                        int id = Upgrades.Count - 1;
                        for (int i = 0; i < anz - 1; i++)
                        {
                            Upgrades[id].Erhöhen();
                            if (GibAnzahlBelegterFächer() > MaxFaecher)
                            {
                                Upgrades[id].Verringern();
                                return anz - (i + 1);
                            }
                        }
                        return 1;
                    }
                    else
                        if (a.Typ == 1)
                        {
                            Konsumierbares.Add(a);
                            if (GibAnzahlBelegterFächer() > MaxFaecher)
                            {
                                Konsumierbares.Remove(a);
                                return 0;
                            }

                            int id = Konsumierbares.Count - 1;
                            for (int i = 0; i < anz - 1; i++)
                            {
                                Konsumierbares[id].Erhöhen();
                                if (GibAnzahlBelegterFächer() > MaxFaecher)
                                {
                                    Konsumierbares[id].Verringern();
                                    return anz - (i + 1);
                                }
                            }
                            return 1;
                        }
                }
            return Objekt.Anzahl;
        }

        /// <summary>
        /// Fügt ein Inventar ins Inventar ein (änder Original)
        /// </summary>
        /// <param name="InventarObjekt">das einzufügende Inventar</param>
        /// <returns>true = alle Objekt im Inventar konnten eingefügt werden</returns>
        public bool Hinzufügen(ref Inventar InventarObjekt)
        {
            // Treibstoff einfügen
            Treibstoff += InventarObjekt.Treibstoff;
            InventarObjekt.Treibstoff = 0;

            // Upgrades einfügen
            for (int i = 0; i < InventarObjekt.Upgrades.Count; i++)
            {
                Item a = InventarObjekt.Upgrades[i];
                if (Hinzufügen(ref a) > 0)
                {
                    InventarObjekt.Upgrades[i] = a;
                    return false;
                }
                else
                    InventarObjekt.Upgrades[i] = a;
            }

            // Konsumierbares einfügen
            for (int i = 0; i < InventarObjekt.Konsumierbares.Count; i++)
            {
                Item a = InventarObjekt.Konsumierbares[i];
                if (Hinzufügen(ref a) > 0)
                {
                    InventarObjekt.Konsumierbares[i] = a;
                    return false;
                }
                else
                    InventarObjekt.Konsumierbares[i] = a;
            }

            // Munition einfügen
            for (int i = 0; i < InventarObjekt.Munition.Count; i++)
            {
                int a = HinzufügenMunition(i, InventarObjekt.Munition[i]);
                InventarObjekt.Munition[i] -= (InventarObjekt.Munition[i] - a);

                if (a > 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Fügt Treibstoff ins Inventar ein
        /// </summary>
        /// <param name="Anzahl">Menge an einzufügenden Treibstoffs</param>
        /// <returns>
        /// Anzahl des Treibstoffs der nicht eingefügt werden konnte
        /// </returns>
        public float HinzufügenTreibstoff(float Anzahl)
        {
            // Treibstoff einfügen
            Treibstoff += Anzahl;

            return 0;
        }

        /// <summary>
        /// Entfernt Treibstoff aus dem Inventar
        /// </summary>
        /// <param name="Anzahl">Menge an zu entfernendem Treibstoff</param>
        /// <returns>
        /// Anzahl des Treibstoffs der nicht entfernt werden konnte
        /// </returns>
        public float EntnehmenTreibstoff(float Anzahl)
        {
            // Treibstoff entnehmen
            if (Treibstoff >= Anzahl) { Treibstoff -= Anzahl; return Anzahl; }

            Anzahl = Treibstoff;
            Treibstoff = 0;
            return Anzahl;
        }

        /// <summary>
        /// Fügt Munition ins Inventar ein
        /// </summary>
        /// <param name="id">id der Munition in List-int- Munition</param>
        /// <param name="Anzahl">Anzahl der einzufügenden Munition</param>
        /// <returns>Anzahl der Munition, die nicht eingefügt werden konnte</returns>
        public int HinzufügenMunition(int id, int Anzahl)
        {
            for (int i = 0; i < Anzahl; i++)
            {
                Munition[id]++;
                if (GibAnzahlBelegterFächer() > MaxFaecher)
                {
                    Munition[id]--;
                    return Anzahl - i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Entnimmt Munition aus dem Inventar
        /// </summary>
        /// <param name="id">id der Munition in List-int- Munition</param>
        /// <param name="Anzahl">Anzahl der zu entnehmenden Munition</param>
        /// <returns>Anzahl der Munition, die entnommen werden konnte</returns>
        public int EntnehmenMunition(int id, int Anzahl)
        {
            if (Munition[id] >= Anzahl) { Munition[id] -= Anzahl; return Anzahl; };

            Anzahl = Munition[id];
            Munition[id] = 0;
            return Anzahl;
        }

        /// <summary>
        /// Entfernt komplettes Konsumitem aus dem Inventar (egal wieviel Anzahl)
        /// </summary>
        /// <param name="id">die id des Konsumitem innerhalb von List-Item- Konsumierbares</param>
        /// <returns>true = konnte entfernt werden, false = id falsch</return
        public bool LöschenKonsumierbares(int id)
        {
            if (id < 0 || id >= Konsumierbares.Count) return false;
            Konsumierbares.RemoveAt(id);
            return true;
        }

        /// <summary>
        /// Entfernt komplettes Upgrade aus dem Inventar (egal wieviel Anzahl)
        /// </summary>
        /// <param name="id">die id des Upgrades innerhalb von List-Item- Upgrades</param>
        /// <returns>true = konnte entfernt werden, false = id falsch</returns>
        public bool LöschenUpgrade(int id)
        {
            if (id < 0 || id >= Upgrades.Count) return false;
            Upgrades.RemoveAt(id);
            return true;
        }

        /// <summary>
        /// Erstellt ein Inventarobjekt aus Text
        /// </summary>
        /// <param name="Text">der Text, der das Objekt darstellt</param>
        /// <param name="Content">einen ContentManager</param>
        /// <param name="Objekt">falls im Text kein Inventar gefunden wird, wird dieses genommen</param>
        /// <returns>
        /// Das aus dem Text erstellte Inventar
        /// </returns>
        public static Inventar Laden(List<String> Text, ContentManager Content, Inventar Objekt)
        {
            Inventar temp = Objekt;
            if (temp == null) temp = new Inventar();

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "INVENTAR");
            if (Text2.Count == 0) return temp;

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.Treibstoff = TextLaden.LadeFloat(Liste, "Treibstoff", temp.Treibstoff);
            temp.MaxFaecher = TextLaden.LadeInt(Liste, "MaxFaecher", temp.MaxFaecher);
            temp.Fachgroesse = TextLaden.LadeInt(Liste, "Fachgroesse", temp.Fachgroesse);

            List<String> Text3 = TextLaden.ErmittleBereich(Text2, "KONSUMIERBARES");
            while (Text3.Count > 0)
                temp.Konsumierbares.Add(Item.Laden(Text3, Content,null));

            Text3 = TextLaden.ErmittleBereich(Text2, "UPGRADES");
            while (Text3.Count > 0)
                temp.Upgrades.Add(Item.Laden(Text3, Content, null));

            Text3 = TextLaden.ErmittleBereich(Text2, "MUNITION");
            List<int> q = new List<int>();
            for (int i = 0; i < Text2.Count(); i++)
                q.Add(Convert.ToInt32(Text3[i]));
            temp.Munition = q;

            return temp;
        }

        /// <summary>
        /// Speichert ein Inventarobjekt in Text
        /// </summary>
        /// <returns>
        /// Eine Textdarstellung des Inventars
        /// </returns>
        public List<String> Speichern()
        {
            List<String> data = new List<String>();
            data.Add("[INVENTAR]");
            data.Add("Treibstoff=" + Treibstoff);
            data.Add("MaxFaecher=" + MaxFaecher.ToString());
            data.Add("Fachgroeße=" + Fachgroesse.ToString());

            data.Add("[KONSUMIERBARES]");
            for (int i = 0; i < Konsumierbares.Count; i++)
                data.AddRange(Konsumierbares[i].Speichern());
            data.Add("[/KONSUMIERBARES]");

            data.Add("[UPGRADES]");
            for (int i = 0; i < Upgrades.Count; i++)
                data.AddRange(Upgrades[i].Speichern());
            data.Add("[/UPGRADES]");

            data.Add("[MUNITION]");
            for (int b = 0; b < Munition.Count; b++)
                data.Add("Munition" + b + "=" + Munition[b]);
            data.Add("[/MUNITION]");
            data.Add("[/INVENTAR]");
            return data;
        }
    }
}