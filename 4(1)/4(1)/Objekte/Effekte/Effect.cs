// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 08-02-2013
// ***********************************************************************
// <copyright file="Effect.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    /// Class Effect
    /// </summary>
    public class Effekt
    {
        #region Grundeinstellungen

        /// <summary>
        /// Eine zugehörige Textur (Buttonbild)
        /// </summary>
        public Texture2D Bild=null;

        /// <summary>
        /// Wirkungsdauer, nach wievielen Runden löst sicher der Effekt im Effektpacket auf
        /// </summary>
        public int Dauer =0;

        /// <summary>
        /// Ein eindeutiger Bezeichner für den Effekt
        /// </summary>
        public String Name ="";

        /// <summary>
        /// 0==Konsumierbar, 1==Status, 2==Upgrade
        /// </summary>
        public int Sorte = 0;

        #endregion Grundeinstellungen

        #region Status

        /// <summary>
        /// Eingefroren
        /// </summary>
        public float Eingefroren = 0;

        /// <summary>
        /// Elektrisiert
        /// </summary>
        public float Elektrisiert = 0;

        /// <summary>
        /// Vergiftet
        /// </summary>
        public float Vergiftet = 0;

        #endregion Status

        #region Effekte/Bonus

        /// <summary>
        /// Arbeitsbereich erweitern
        /// </summary>
        public int Arbeitsbereich = 0;

        /// <summary>
        /// Arbeitsbereich in Prozent erweitern
        /// </summary>
        public int ArbeitsbereichProzent = 0;

        /// <summary>
        /// Fächerzahl im Inventar erhöhen
        /// </summary>
        public int Faecher = 0;

        /// <summary>
        /// Fächerzahl in Prozent im Inventar erhöhen
        /// </summary>
        public int FaecherProzent = 0;

        /// <summary>
        /// erhaltenen Feuerschaden verringern
        /// </summary>
        public int FeuerResistenz = 0;

        /// <summary>
        /// erhaltenen Feuerschaden in Prozent verringern
        /// </summary>
        public int FeuerResistenzProzent = 0;

        /// <summary>
        /// Geschwindigkeit Rückwärts erhöhen
        /// </summary>
        public int GeschwR = 0;

        /// <summary>
        /// TGeschwindigkeit Rückwärts in Prozent erhöhen
        /// </summary>
        public int GeschwRProzent = 0;

        /// <summary>
        /// Geschwindigkeit Vorwärts erhöhen
        /// </summary>
        public int GeschwV = 0;

        /// <summary>
        /// Geschwindigkeit Vorwärts in Prozent erhöhen
        /// </summary>
        public int GeschwVProzent = 0;

        /// <summary>
        /// erhaltenen Giftschaden reduzieren
        /// </summary>
        public int GiftResistenz = 0;

        /// <summary>
        /// erhaltenen Giftschaden in Prozent reduzieren
        /// </summary>
        public int GiftResistenzProzent = 0;

        /// <summary>
        /// Lebenspunkte erhöhen
        /// </summary>
        public int HP = 0;

        /// <summary>
        /// Lebenspunkte in Przent erhöhen
        /// </summary>
        public int HPProzent = 0;

        /// <summary>
        /// die maximalen Lebenspunkte erhöhen
        /// </summary>
        public int MaxHP = 0;

        /// <summary>
        /// die maximalen Lebenspunkte in Prozent erhöhen
        /// </summary>
        public int MaxHPProzent = 0;

        /// <summary>
        /// den eigenen Schaden, den man verursacht erhöhen
        /// </summary>
        public int Schaden = 0;

        /// <summary>
        /// den eigenen Schaden in Prozent, den man verursacht erhöhen
        /// </summary>
        public int SchadenProzent = 0;

        /// <summary>
        /// den Treibstroffverbrauch in Prozent erhöhen
        /// </summary>
        public int VerbrauchProzent = 0;

        /// <summary>
        /// verringert den erhalten Allgemeinschaden
        /// </summary>
        public int Verteidigung = 0;

        /// <summary>
        /// verringert den erhalten Allgemeinschaden in Prozent
        /// </summary>
        public int VerteidigungProzent = 0;

        #endregion Effekte/Bonus

        #region Spezialeffekte

        /// <summary>
        /// Tarnfähigkeit
        /// </summary>
        public int Tarnfaehigkeit = 0;

        /// <summary>
        /// Zielhilfe
        /// </summary>
        public int Zielhilfe = 0;

        #endregion Spezialeffekte

        /// <summary>
        /// Konstruktor ohne initialisierung
        /// </summary>
        public Effekt()
        {
        }

        /// <summary>
        /// Konstruktor übernimmt Werte aus Effekt A
        /// </summary>
        /// <param name="A">das Effektobjekt, dessen Eigenschaften übernommen werden sollen</param>
        public Effekt(Effekt A)
        {
            init(A.Name, (String)A.Bild.Tag, A.Dauer, A.Sorte, A.HP, A.HPProzent, A.MaxHP, A.MaxHPProzent, A.Arbeitsbereich, A.ArbeitsbereichProzent, A.GeschwV, A.GeschwVProzent, A.GeschwR, A.GeschwRProzent, A.Schaden, A.SchadenProzent, A.FeuerResistenz, A.FeuerResistenzProzent, A.GiftResistenz, A.GiftResistenzProzent, A.Verteidigung, A.VerteidigungProzent, A.Eingefroren, A.Vergiftet, A.Elektrisiert, A.Tarnfaehigkeit, A.Zielhilfe, A.Faecher, A.FaecherProzent, A.VerbrauchProzent);
        }

        /// <summary>
        /// Konstruktor initialisiert Effektobjekt
        /// </summary>
        /// <param name="_Name">Eindeutiger Bezeichner</param>
        /// <param name="_Bild">Der Button (Textur)</param>
        /// <param name="_Dauer">Effektdauer</param>
        /// <param name="_Sorte">Sorte  0==Konsumierbar, 1==Status, 2==Upgrade</param>
        /// <param name="_HP">The _ HP.</param>
        /// <param name="_ProzentHP">The _ prozent HP.</param>
        /// <param name="_MaxHP">The _ max HP.</param>
        /// <param name="_ProzentMaxHP">The _ prozent max HP.</param>
        /// <param name="_Arbeitsbereich">The _ arbeitsbereich.</param>
        /// <param name="_ProzentArbeitsbereich">The _ prozent arbeitsbereich.</param>
        /// <param name="_GeschwV">The _ geschw V.</param>
        /// <param name="_ProzentGeschwV">The _ prozent geschw V.</param>
        /// <param name="_GeschwR">The _ geschw R.</param>
        /// <param name="_ProzentGeschwR">The _ prozent geschw R.</param>
        /// <param name="_Schaden">The _ schaden.</param>
        /// <param name="_ProzentSchaden">The _ prozent schaden.</param>
        /// <param name="_FeuerSchaden">The _ feuer schaden.</param>
        /// <param name="_FeuerProzentSchaden">The _ feuer prozent schaden.</param>
        /// <param name="_GiftSchaden">The _ gift schaden.</param>
        /// <param name="_GiftProzentSchaden">The _ gift prozent schaden.</param>
        /// <param name="_Verteidigung">The _ verteidigung.</param>
        /// <param name="_ProzentVerteidigung">The _ prozent verteidigung.</param>
        /// <param name="_Eingefroren">The _ eingefroren.</param>
        /// <param name="_Vergiftet">The _ vergiftet.</param>
        /// <param name="_Elektrisiert">The _ elektrisiert.</param>
        /// <param name="_Tarnfähigkeit">The _ tarnfähigkeit.</param>
        /// <param name="_Zielhilfe">The _ zielhilfe.</param>
        /// <param name="_Slots">The _ slots.</param>
        /// <param name="_ProzentSlots">The _ prozent slots.</param>
        /// <param name="_ProzentVerbrauch">The _ prozent verbrauch.</param>
        public Effekt(String _Name, String _Bild, int _Dauer, int _Sorte, int _HP, int _ProzentHP, int _MaxHP, int _ProzentMaxHP, int _Arbeitsbereich, int _ProzentArbeitsbereich, int _GeschwV, int _ProzentGeschwV, int _GeschwR, int _ProzentGeschwR, int _Schaden, int _ProzentSchaden, int _FeuerSchaden, int _FeuerProzentSchaden, int _GiftSchaden, int _GiftProzentSchaden, int _Verteidigung, int _ProzentVerteidigung, float _Eingefroren, float _Vergiftet, float _Elektrisiert, int _Tarnfähigkeit, int _Zielhilfe, int _Slots, int _ProzentSlots, int _ProzentVerbrauch)
        {
            init(_Name, _Bild, _Dauer, _Sorte, _HP, _ProzentHP, _MaxHP, _ProzentMaxHP, _Arbeitsbereich, _ProzentArbeitsbereich, _GeschwV, _ProzentGeschwV, _GeschwR, _ProzentGeschwR, _Schaden, _ProzentSchaden, _FeuerSchaden, _FeuerProzentSchaden, _GiftSchaden, _GiftProzentSchaden, _Verteidigung, _ProzentVerteidigung, _Eingefroren, _Vergiftet, _Elektrisiert, _Tarnfähigkeit, _Zielhilfe, _Slots, _ProzentSlots, _ProzentVerbrauch);
        }

        /// <summary>
        /// Addiert ein Effektobjekt zu diesem Objekt
        /// </summary>
        /// <param name="Objekt">Das zu addierende Objekt</param>
        public void Addieren(Effekt Objekt)
        {
            HP += Objekt.HP;
            HPProzent += Objekt.HPProzent;
            MaxHP += Objekt.MaxHP;
            if (HP > MaxHP) HP = MaxHP;
            MaxHPProzent += Objekt.MaxHPProzent;
            Arbeitsbereich += Objekt.Arbeitsbereich;
            ArbeitsbereichProzent += Objekt.ArbeitsbereichProzent;
            GeschwV += Objekt.GeschwV;
            GeschwVProzent += Objekt.GeschwVProzent;
            GeschwR += Objekt.GeschwR;
            GeschwRProzent += Objekt.GeschwRProzent;
            Schaden += Objekt.Schaden;
            SchadenProzent += Objekt.SchadenProzent;
            Verteidigung += Objekt.Verteidigung;
            VerteidigungProzent += Objekt.VerteidigungProzent;
            FeuerResistenzProzent += Objekt.FeuerResistenzProzent;
            GiftResistenzProzent += Objekt.GiftResistenzProzent;
            FeuerResistenz += Objekt.FeuerResistenz;
            GiftResistenz += Objekt.GiftResistenz;
            Eingefroren += Objekt.Eingefroren;
            Vergiftet += Objekt.Vergiftet;
            Elektrisiert += Objekt.Elektrisiert;
            Tarnfaehigkeit += Objekt.Tarnfaehigkeit;
            Zielhilfe += Objekt.Zielhilfe;
            Faecher += Objekt.Faecher;
            FaecherProzent += Objekt.FaecherProzent;
            VerbrauchProzent += Objekt.VerbrauchProzent;
        }

        /// <summary>
        /// Erstellt einen beschreibenden für den Effekt (auflistung der Fähigkeiten)
        /// </summary>
        /// <returns>Gibt den Beschreibungstext zurück mit \n</returns>
        public String Beschreibungstext()
        {
            List<String> Text = new List<String>();
            //Status
            if (Eingefroren > 0) Text.Add("!!!Eingefroren!!!");
            if (Vergiftet > 0) Text.Add("!!!Vergiftet!!!");
            if (Elektrisiert > 0) Text.Add("!!!Elektrisiert!!!");

            // Effekte/Bonus
            if (HP != 0) Text.Add("HP: " + Vorzeichen(HP));
            if (HPProzent != 0) Text.Add("HP: " + Vorzeichen(HPProzent) + "%");
            if (MaxHP != 0) Text.Add("MaxHP: " + Vorzeichen(MaxHP));
            if (MaxHPProzent != 0) Text.Add("MaxHP: " + Vorzeichen(MaxHPProzent) + "%");
            if (Arbeitsbereich != 0) Text.Add("Arbeitsbereich: " + Vorzeichen(Arbeitsbereich));
            if (ArbeitsbereichProzent != 0) Text.Add("Arbeitsbereich: " + Vorzeichen(ArbeitsbereichProzent) + "%");
            if (GeschwV != 0) Text.Add("Geschw. Vorwaerts: " + Vorzeichen(GeschwV));
            if (GeschwVProzent != 0) Text.Add("Geschw. Vorwaerts: " + Vorzeichen(GeschwVProzent) + "%");
            if (GeschwR != 0) Text.Add("Geschw. Rueckwaerts: " + Vorzeichen(GeschwR));
            if (GeschwRProzent != 0) Text.Add("Geschw. Rueckwaerts: " + Vorzeichen(GeschwRProzent) + "%");

            // Schaden den du machst
            if (Schaden != 0) Text.Add("Schaden: " + Vorzeichen(Schaden));
            if (SchadenProzent != 0) Text.Add("Schaden: " + Vorzeichen(SchadenProzent) + "%");

            // Schaden den du bekommst
            if (FeuerResistenz != 0) Text.Add("Resistenz Feuer: " + Vorzeichen(FeuerResistenz));
            if (FeuerResistenzProzent != 0) Text.Add("Resistenz Feuer: " + Vorzeichen(FeuerResistenzProzent) + "%");
            if (GiftResistenz != 0) Text.Add("Resistenz Gift: " + Vorzeichen(GiftResistenz));
            if (GiftResistenzProzent != 0) Text.Add("Resistenz Gift: " + Vorzeichen(GiftResistenzProzent) + "%");
            if (Verteidigung != 0) Text.Add("Resistenz Schaden: " + Vorzeichen(Verteidigung));
            if (VerteidigungProzent != 0) Text.Add("Resistenz Schaden: " + Vorzeichen(VerteidigungProzent) + "%");

            String p = "";
            for (int i = 0; i < Text.Count; i++) p = p + "> " + Text[i] + "\n";
            return p;
        }

        // TODO ausfüllen
        /// <summary>
        /// Erzeugt den Inhalt des Effektes aus einem Text
        /// </summary>
        /// <param name="Text">der Text in dem der Effekt definiert ist</param>
        public static Effekt Laden(List<String> Text, ContentManager Content, Effekt Default)
        {
            Effekt temp = new Effekt();

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "EFFEKT");
            if (Text2.Count == 0) return Default;

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.Name = TextLaden.LadeString(Liste, "Name", temp.Name);
            temp.Sorte = TextLaden.LadeInt(Liste, "Sorte", temp.Sorte);
            temp.Dauer = TextLaden.LadeInt(Liste, "Dauer", temp.Dauer);

            temp.HP = TextLaden.LadeInt(Liste, "HP", temp.HP);
            temp.HPProzent = TextLaden.LadeInt(Liste, "HPProzent", temp.HPProzent);
            temp.MaxHP = TextLaden.LadeInt(Liste, "MaxHP", temp.MaxHP);
            temp.MaxHPProzent = TextLaden.LadeInt(Liste, "MaxHPProzent", temp.MaxHPProzent);
            temp.Arbeitsbereich = TextLaden.LadeInt(Liste, "Arbeitsbereich", temp.Arbeitsbereich);
            temp.ArbeitsbereichProzent = TextLaden.LadeInt(Liste, "ArbeitsbereichProzent", temp.ArbeitsbereichProzent);
            temp.GeschwV = TextLaden.LadeInt(Liste, "GeschwV", temp.GeschwV);
            temp.GeschwVProzent = TextLaden.LadeInt(Liste, "GeschwVProzent", temp.GeschwVProzent);
            temp.GeschwR = TextLaden.LadeInt(Liste, "GeschwR", temp.GeschwR);
            temp.GeschwRProzent = TextLaden.LadeInt(Liste, "GeschwRProzent", temp.GeschwRProzent);
            temp.Schaden = TextLaden.LadeInt(Liste, "Schaden", temp.Schaden);
            temp.SchadenProzent = TextLaden.LadeInt(Liste, "SchadenProzent", temp.SchadenProzent);
            temp.Verteidigung = TextLaden.LadeInt(Liste, "Verteidigung", temp.Verteidigung);
            temp.VerteidigungProzent = TextLaden.LadeInt(Liste, "VerteidigungProzent", temp.VerteidigungProzent);
            temp.FeuerResistenzProzent = TextLaden.LadeInt(Liste, "FeuerResistenzProzent", temp.FeuerResistenzProzent);
            temp.GiftResistenzProzent = TextLaden.LadeInt(Liste, "GiftResistenzProzent", temp.GiftResistenzProzent);
            temp.FeuerResistenz = TextLaden.LadeInt(Liste, "FeuerResistenz", temp.FeuerResistenz);
            temp.GiftResistenz = TextLaden.LadeInt(Liste, "GiftResistenz", temp.GiftResistenz);
            temp.Eingefroren = TextLaden.LadeFloat(Liste, "Eingefroren", temp.Eingefroren);
            temp.Vergiftet = TextLaden.LadeFloat(Liste, "Vergiftet", temp.Vergiftet);
            temp.Elektrisiert = TextLaden.LadeFloat(Liste, "Elektrisiert", temp.Elektrisiert);
            temp.Tarnfaehigkeit = TextLaden.LadeInt(Liste, "Tarnfaehigkeit", temp.Tarnfaehigkeit);
            temp.Zielhilfe = TextLaden.LadeInt(Liste, "Zielhilfe", temp.Zielhilfe);
            temp.Faecher = TextLaden.LadeInt(Liste, "Faecher", temp.Faecher);
            temp.FaecherProzent = TextLaden.LadeInt(Liste, "FaecherProzent", temp.FaecherProzent);
            temp.VerbrauchProzent = TextLaden.LadeInt(Liste, "VerbrauchProzent", temp.VerbrauchProzent);

            String Typ = TextLaden.LadeString(Liste, "Bild", temp.Name);
            temp.Bild = Content.Load<Texture2D>(Typ);
            temp.Bild.Tag = Typ;
            return temp;
        }

        /// <summary>
        /// Wandelt den Effekt zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> Speichern()
        {
            List<String> data = new List<String>();
            data.Add("[EFFEKT]");
            data.Add("Name=" + Name);
            data.Add("Sorte=" + Sorte.ToString());
            data.Add("Dauer=" + Dauer.ToString());
            data.Add("Bild=" + (Bild == null ? "Textures//leer" : Bild.Tag));

            if (HP != 0) data.Add("HP=" + HP.ToString());
            if (HPProzent != 0) data.Add("HPProzent=" + HPProzent.ToString());
            if (MaxHP != 0) data.Add("MaxHP=" + MaxHP.ToString());
            if (MaxHPProzent != 0) data.Add("MaxHPProzent=" + MaxHPProzent.ToString());
            if (Arbeitsbereich != 0) data.Add("Arbeitsbereich=" + Arbeitsbereich.ToString());
            if (ArbeitsbereichProzent != 0) data.Add("ArbeitsbereichProzent=" + ArbeitsbereichProzent.ToString());
            if (GeschwV != 0) data.Add("GeschwV=" + GeschwV.ToString());
            if (GeschwVProzent != 0) data.Add("GeschwVProzent=" + GeschwVProzent.ToString());
            if (GeschwR != 0) data.Add("GeschwR=" + GeschwR.ToString());
            if (GeschwRProzent != 0) data.Add("GeschwRProzent=" + GeschwRProzent.ToString());
            if (Schaden != 0) data.Add("Schaden=" + Schaden.ToString());
            if (SchadenProzent != 0) data.Add("SchadenProzent=" + SchadenProzent.ToString());
            if (Verteidigung != 0) data.Add("Verteidigung=" + Verteidigung.ToString());
            if (VerteidigungProzent != 0) data.Add("VerteidigungProzent=" + VerteidigungProzent.ToString());
            if (FeuerResistenzProzent != 0) data.Add("FeuerResistenzProzent=" + FeuerResistenzProzent.ToString());
            if (GiftResistenzProzent != 0) data.Add("GiftResistenzProzent=" + GiftResistenzProzent.ToString());
            if (FeuerResistenz != 0) data.Add("FeuerResistenz=" + FeuerResistenz.ToString());
            if (GiftResistenz != 0) data.Add("GiftResistenzz=" + GiftResistenz.ToString());
            if (Eingefroren != 0) data.Add("Eingefroren=" + Eingefroren.ToString());
            if (Vergiftet != 0) data.Add("Vergiftet=" + Vergiftet.ToString());
            if (Elektrisiert != 0) data.Add("Elektrisiert=" + Elektrisiert.ToString());
            if (Tarnfaehigkeit != 0) data.Add("Tarnfaehigkeit=" + Tarnfaehigkeit.ToString());
            if (Zielhilfe != 0) data.Add("Zielhilfe=" + Zielhilfe.ToString());
            if (Faecher != 0) data.Add("Faecher=" + Faecher.ToString());
            if (FaecherProzent != 0) data.Add("FaecherProzent=" + FaecherProzent.ToString());
            if (VerbrauchProzent != 0) data.Add("VerbrauchProzent=" + VerbrauchProzent.ToString());
            data.Add("[/EFFEKT]");

            return data;
        }

        /// <summary>
        /// Subtrahiert die Fähigkeiten zweier Effekte
        /// </summary>
        /// <param name="Objekt">das zu subtrahierende Objekt</param>
        public void Subtrahieren(Effekt Objekt)
        {
            HP -= Objekt.HP;
            HPProzent -= Objekt.HPProzent;
            MaxHP -= Objekt.MaxHP;
            MaxHPProzent -= Objekt.MaxHPProzent;
            Arbeitsbereich -= Objekt.Arbeitsbereich;
            ArbeitsbereichProzent -= Objekt.ArbeitsbereichProzent;
            GeschwV -= Objekt.GeschwV;
            GeschwVProzent -= Objekt.GeschwVProzent;
            GeschwR -= Objekt.GeschwR;
            GeschwRProzent -= Objekt.GeschwRProzent;
            Schaden -= Objekt.Schaden;
            SchadenProzent -= Objekt.SchadenProzent;
            Verteidigung -= Objekt.Verteidigung;
            VerteidigungProzent -= Objekt.VerteidigungProzent;
            FeuerResistenzProzent -= Objekt.FeuerResistenzProzent;
            GiftResistenzProzent -= Objekt.GiftResistenzProzent;
            FeuerResistenz -= Objekt.FeuerResistenz;
            GiftResistenz -= Objekt.GiftResistenz;
            Eingefroren -= Objekt.Eingefroren;
            Vergiftet -= Objekt.Vergiftet;
            Elektrisiert -= Objekt.Elektrisiert;
            Tarnfaehigkeit -= Objekt.Tarnfaehigkeit;
            Zielhilfe -= Objekt.Zielhilfe;
            Faecher -= Objekt.Faecher;
            FaecherProzent -= Objekt.FaecherProzent;
            VerbrauchProzent -= Objekt.VerbrauchProzent;
        }

        /// <summary>
        /// Initialisiert Effektobjekt
        /// </summary>
        /// <param name="_Name">Eindeutiger Bezeichner</param>
        /// <param name="_Bild">Der Button (Textur)</param>
        /// <param name="_Dauer">Effektdauer</param>
        /// <param name="_Sorte">Sorte  0==Konsumierbar, 1==Status, 2==Upgrade</param>
        /// <param name="_HP">The _ HP.</param>
        /// <param name="_ProzentHP">The _ prozent HP.</param>
        /// <param name="_MaxHP">The _ max HP.</param>
        /// <param name="_ProzentMaxHP">The _ prozent max HP.</param>
        /// <param name="_Arbeitsbereich">The _ arbeitsbereich.</param>
        /// <param name="_ProzentArbeitsbereich">The _ prozent arbeitsbereich.</param>
        /// <param name="_GeschwV">The _ geschw V.</param>
        /// <param name="_ProzentGeschwV">The _ prozent geschw V.</param>
        /// <param name="_GeschwR">The _ geschw R.</param>
        /// <param name="_ProzentGeschwR">The _ prozent geschw R.</param>
        /// <param name="_Schaden">The _ schaden.</param>
        /// <param name="_ProzentSchaden">The _ prozent schaden.</param>
        /// <param name="_FeuerSchaden">The _ feuer schaden.</param>
        /// <param name="_FeuerProzentSchaden">The _ feuer prozent schaden.</param>
        /// <param name="_GiftSchaden">The _ gift schaden.</param>
        /// <param name="_GiftProzentSchaden">The _ gift prozent schaden.</param>
        /// <param name="_Verteidigung">The _ verteidigung.</param>
        /// <param name="_ProzentVerteidigung">The _ prozent verteidigung.</param>
        /// <param name="_Eingefroren">The _ eingefroren.</param>
        /// <param name="_Vergiftet">The _ vergiftet.</param>
        /// <param name="_Elektrisiert">The _ elektrisiert.</param>
        /// <param name="_Tarnfähigkeit">The _ tarnfähigkeit.</param>
        /// <param name="_Zielhilfe">The _ zielhilfe.</param>
        /// <param name="_Slots">The _ slots.</param>
        /// <param name="_ProzentSlots">The _ prozent slots.</param>
        /// <param name="_ProzentVerbrauch">The _ prozent verbrauch.</param>
        private void init(String _Name, String _Bild, int _Dauer, int _Sorte, int _HP, int _ProzentHP, int _MaxHP, int _ProzentMaxHP, int _Arbeitsbereich, int _ProzentArbeitsbereich, int _GeschwV, int _ProzentGeschwV, int _GeschwR, int _ProzentGeschwR, int _Schaden, int _ProzentSchaden, int _FeuerSchaden, int _FeuerProzentSchaden, int _GiftSchaden, int _GiftProzentSchaden, int _Verteidigung, int _ProzentVerteidigung, float _Eingefroren, float _Vergiftet, float _Elektrisiert, int _Tarnfähigkeit, int _Zielhilfe, int _Slots, int _ProzentSlots, int _ProzentVerbrauch)
        {
            HP = _HP;
            Name = _Name;
            Bild = Game1.ContentAll.Load<Texture2D>(_Bild);
            Bild.Tag = _Bild;
            Dauer = _Dauer;
            Sorte = _Sorte;
            HPProzent = _ProzentHP;
            MaxHP = _MaxHP;
            MaxHPProzent = _ProzentMaxHP;
            Arbeitsbereich = _Arbeitsbereich;
            ArbeitsbereichProzent = _ProzentArbeitsbereich;
            GeschwV = _GeschwV;
            GeschwVProzent = _ProzentGeschwV;
            GeschwR = _GeschwR;
            GeschwRProzent = _ProzentGeschwR;
            Schaden = _Schaden;
            SchadenProzent = _ProzentSchaden;
            Verteidigung = _Verteidigung;
            VerteidigungProzent = _ProzentVerteidigung;
            FeuerResistenzProzent = _FeuerProzentSchaden;
            GiftResistenzProzent = _GiftProzentSchaden;
            FeuerResistenz = _FeuerSchaden;
            GiftResistenz = _GiftSchaden;
            Eingefroren = _Eingefroren;
            Vergiftet = _Vergiftet;
            Elektrisiert = _Elektrisiert;
            Tarnfaehigkeit = _Tarnfähigkeit;
            Zielhilfe = _Zielhilfe;
            Faecher = _Slots;
            FaecherProzent = _ProzentSlots;
            VerbrauchProzent = _ProzentVerbrauch;
        }

        /// <summary>
        ///  Hilfsfunktion zum Anhängen des Vorzeichens an eine Zahl
        /// </summary>
        /// <param name="Zahl">Die zu verarbeitende Zahl</param>
        /// <returns>Gibt die "Zahl" als String mit + und - aus</returns>
        private String Vorzeichen(int Zahl)
        {
            return (Zahl < 0 ? "" : "+") + Zahl.ToString();
        }
    }
}