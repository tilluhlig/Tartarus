// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-18-2013
// ***********************************************************************
// <copyright file="Item.cs">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace _4_1_
{
    /// <summary>
    /// Diese Klasse verwaltet Packete gleichartiger Gegenstände(Items)
    /// </summary>
    public class Item
    {
        /// <summary>
        /// gleiche Items werden zusammengefasst
        /// </summary>
        public int Anzahl;

        /// <summary>
        /// der Effekt, der zu diesem Item gehört
        /// </summary>
        public Effekt Effekt;

        /// <summary>
        /// Ein bezeichner für das Item (dient der eindeutigen Unterscheidung), wollte erst Zahlen nehmen, kann ich mir aber schlecht merken
        /// </summary>
        public String Name;

        /// <summary>
        /// Einzelpreis der Items
        /// </summary>
        public int Preis;

        /// <summary>
        /// 0 == Upgrade, 1 == Konsumierbar
        /// </summary>
        public int Typ;

        /// <summary>
        ///Konstruktor
        /// </summary>
        /// <param name="_Name">Name des Items</param>
        /// <param name="_Preis">Einzelpreis</param>
        /// <param name="_Effekt">Das zugehörige Effektpacket</param>
        /// <param name="_Anzahl">Die Anzahl gleichartiger Items</param>
        /// <param name="_Typ">Typ der Items 0 = Upgrade, 1 = Konsumierbares</param>
        public Item(String _Name, int _Preis, Effekt _Effekt, int _Anzahl, int _Typ)
        {
            Name = _Name;
            Preis = _Preis;
            Anzahl = _Anzahl;
            Effekt = _Effekt;
            Typ = _Typ;
        }

        public Item()
        {
        }

        /// <summary>
        /// (+1) erhöht die Anzahl der Items in diesem Item
        /// </summary>
        public void Erhöhen()
        {
            Anzahl++;
        }

        /// <summary>
        /// (+anz) erhöht die Anzahl der Items in diesem Item
        /// </summary>
        /// <param name="anz">Die Anzahl um die Erhöht werden soll</param>
        public void Erhöhen(int anz)
        {
            Anzahl += anz;
        }

        /// <summary>
        /// (=anz) setzt die Anzahl der Items in diesem Item
        /// </summary>
        /// <param name="anz">Die Anzahl auf die gesetzt werden soll</param>
        public void SetzeAnzahl(int anz)
        {
            Anzahl = anz;
        }

        /// <summary>
        /// (-1) reduziert die Anzahl der Items in diesem Item (wenn es dann leer ist, wird false zurückgegeben)
        /// </summary>
        /// <returns>Gibt zurück, ob Objektpacket leer ist, true = >0 Objekte , false = leer</returns>
        public bool Verringern()
        {
            Anzahl--;
            if (Anzahl <= 0) return false;
            return true;
        }

        /// <summary>
        /// (-anz) reduziert die Anzahl der Items in diesem Item (wenn es dann leer ist, wird false zurückgegeben)
        /// </summary>
        /// <param name="anz">die Anzahl um die verringert werden soll</param>
        /// <returns>
        /// Gibt zurück, ob Objektpacket leer ist, true = >0 Objekte , false = leer
        /// </returns>
        public bool Verringern(int anz)
        {
            Anzahl -= anz;
            if (Anzahl <= 0) return false;
            return true;
        }

        // TODO ausfüllen
        /// <summary>
        /// Erzeugt den Inhalt des Effektes aus einem String
        /// </summary>
        /// <param name="Text">der Text in dem der Effekt definiert ist</param>
        public static Item Laden(List<String> Text, ContentManager Content)
        {
            Item temp = new Item();

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "ITEM");

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.Name = TextLaden.LadeString(Liste, "Name", temp.Name);
            temp.Preis = TextLaden.LadeInt(Liste, "Preis", temp.Preis);
            temp.Typ = TextLaden.LadeInt(Liste, "Typ", temp.Typ);
            temp.Anzahl = TextLaden.LadeInt(Liste, "Anzahl", temp.Anzahl);

            temp.Effekt = Effekt.Laden(Text2, Content,null);

            return temp;
        }

        /// <summary>
        /// Wandelt den Effekt zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> Speichern()
        {
            List<String> data = new List<String>();
            data.Add("[ITEM]");
            data.Add("Name=" + Name);
            data.Add("Preis=" + Preis.ToString());
            data.Add("Typ=" + Typ.ToString());
            data.Add("Anzahl=" + Anzahl.ToString());
            data.AddRange(Effekt.Speichern());
            data.Add("[/ITEM]");

            return data;
        }
    }
}