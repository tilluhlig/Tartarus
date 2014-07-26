// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-30-2013
// ***********************************************************************
// <copyright file="EffectPacket.cs">
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
    ///     Class EffectPacket
    /// </summary>
    public class EffectPacket
    {
        #region Fields

        /// <summary>
        ///     The effekt summe
        /// </summary>
        public Effekt EffektSumme = new Effekt();

        /// <summary>
        ///     The konsumierbares
        /// </summary>
        public List<Effekt> Konsumierbares = new List<Effekt>();

        /// <summary>
        ///     The status
        /// </summary>
        public List<Effekt> Status = new List<Effekt>();

        /// <summary>
        ///     The upgrades
        /// </summary>
        public List<Effekt> Upgrades = new List<Effekt>();

        #endregion Fields

        #region Methods

        // TODO ausfüllen
        public static EffectPacket Laden(List<String> Text, ContentManager Content)
        {
            var temp = new EffectPacket();

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "EFFECTPACKET");

            List<String> Text3 = TextLaden.ErmittleBereich(Text2, "STATUS");
            while (Text3.Count > 0)
                temp.Status.Add(Effekt.Laden(Text3, Content, null));

            Text3 = TextLaden.ErmittleBereich(Text2, "UPGRADES");
            while (Text3.Count > 0)
                temp.Upgrades.Add(Effekt.Laden(Text3, Content, null));

            Text3 = TextLaden.ErmittleBereich(Text2, "KONSUMIERBARES");
            while (Text3.Count > 0)
                temp.Konsumierbares.Add(Effekt.Laden(Text3, Content, null));

            temp.EffektSumme = Effekt.Laden(Text2, Content, temp.EffektSumme);

            return temp;
        }

        /// <summary>
        ///     Gets the eingefroren.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool GetEingefroren()
        {
            return EffektSumme.Eingefroren >= 1 ? true : false;
        }

        /// <summary>
        ///     Gets the elektrisiert.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool GetElektrisiert()
        {
            return EffektSumme.Elektrisiert >= 1 ? true : false;
        }

        /// <summary>
        ///     Gets the HP.
        /// </summary>
        /// <param name="_HP">The _ HP.</param>
        /// <returns>System.Int32.</returns>
        public int GetHP(int _HP)
        {
            return (int) ((_HP + EffektSumme.HP)*((float) (100 + EffektSumme.HPProzent)/100));
        }

        /// <summary>
        ///     Gets the max HP.
        /// </summary>
        /// <param name="_MaxHP">The _ max HP.</param>
        /// <returns>System.Int32.</returns>
        public int GetMaxHP(int _MaxHP)
        {
            return (int) ((_MaxHP + EffektSumme.MaxHP)*((float) (100 + EffektSumme.MaxHPProzent)/100));
        }

        /// <summary>
        ///     Gets the vergiftet.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool GetVergiftet()
        {
            return EffektSumme.Vergiftet >= 1 ? true : false;
        }

        /// <summary>
        ///     Gets the arbeitsbereich.
        /// </summary>
        /// <param name="_Arbeitsbereich">The _ arbeitsbereich.</param>
        /// <returns>System.Int32.</returns>
        public int GibArbeitsbereich(int _Arbeitsbereich)
        {
            return
                (int)
                    ((_Arbeitsbereich + EffektSumme.Arbeitsbereich)*
                     ((float) (100 + EffektSumme.ArbeitsbereichProzent)/100));
        }

        /// <summary>
        ///     Gets the damage.
        /// </summary>
        /// <param name="_Damage">The _ damage.</param>
        /// <returns>System.Int32.</returns>
        public int GibEingehendenSchaden(int _Damage)
        {
            var result =
                (int) ((_Damage - EffektSumme.Verteidigung)*((float) (100 - EffektSumme.VerteidigungProzent)/100));
            return result < 0 ? 0 : result;
        }

        /// <summary>
        ///     Gets the feuer schaden.
        /// </summary>
        /// <param name="_Schaden">The _ schaden.</param>
        /// <returns>System.Int32.</returns>
        public int GibFeuerSchaden(int _Schaden)
        {
            var result =
                (int) ((_Schaden - EffektSumme.FeuerResistenz)*((float) (100 - EffektSumme.FeuerResistenzProzent)/100));
            return result < 0 ? 0 : result;
        }

        /// <summary>
        ///     Gets the geschw R.
        /// </summary>
        /// <param name="_GeschwR">The _ geschw R.</param>
        /// <returns>System.Single.</returns>
        public float GibGeschwR(float _GeschwR)
        {
            return (_GeschwR + EffektSumme.GeschwR)*((float) (100 + EffektSumme.GeschwRProzent)/100);
        }

        /// <summary>
        ///     Gets the geschw V.
        /// </summary>
        /// <param name="_GeschwV">The _ geschw V.</param>
        /// <returns>System.Single.</returns>
        public float GibGeschwV(float _GeschwV)
        {
            return (_GeschwV + EffektSumme.GeschwV)*((float) (100 + EffektSumme.GeschwVProzent)/100);
        }

        /// <summary>
        ///     Gets the gift schaden.
        /// </summary>
        /// <param name="_Schaden">The _ schaden.</param>
        /// <returns>System.Int32.</returns>
        public int GibGiftSchaden(int _Schaden)
        {
            var result =
                (int) ((_Schaden - EffektSumme.GiftResistenz)*((float) (100 - EffektSumme.GiftResistenzProzent)/100));
            return result < 0 ? 0 : result;
        }

        /// <summary>
        ///     Gets the schaden.
        /// </summary>
        /// <param name="_Schaden">The _ schaden.</param>
        /// <returns>System.Int32.</returns>
        public int GibSchaden(int _Schaden)
        {
            var result = (int) ((_Schaden + EffektSumme.Schaden)*((float) (100 + EffektSumme.SchadenProzent)/100));
            return result < 0 ? 0 : result;
        }

        public float GibTreibstoffverbrauch(float Verbrauch)
        {
            float result = (1.0f + EffektSumme.VerbrauchProzent)*Verbrauch;
            return result < 0.0f ? 0.0f : result;
        }

        public bool GibZielhilfe()
        {
            return EffektSumme.Zielhilfe >= 1 ? true : false;
        }

        // Verwaltungsfunktionen

        /// <summary>
        ///     Hinzufügens the specified objekt.
        /// </summary>
        /// <param name="Objekt">The objekt.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Hinzufügen(Effekt Objekt) // nimmt einen neuen Effekt auf
        {
            if (Objekt == null) return false;
            if (Objekt.Sorte == 1)
            {
                for (int i = 0; i < Status.Count; i++)
                    if (Objekt.Name == Status[i].Name)
                        return false;
                if (Status.Count < 3)
                {
                    Status.Add(new Effekt(Objekt));
                }
            }
            else if (Objekt.Sorte == 2)
            {
                for (int i = 0; i < Upgrades.Count; i++)
                    if (Objekt.Name == Upgrades[i].Name)
                        return false;
                if (Upgrades.Count < 3)
                {
                    Upgrades.Add(new Effekt(Objekt));
                }
            }
            else if (Objekt.Sorte == 0)
            {
                Konsumierbares.Add(new Effekt(Objekt));
            }
            else
                return false;

            EffektSumme.Addieren(Objekt);
            return true;
        }

        /// <summary>
        ///     Prüfes the effektdauer.
        /// </summary>
        public void PrüfeEffektdauer()
            // Prüft für gespeicherte Effekte, ob diese Abgelaufen sind ud entfernt diese dann
        {
            for (int i = 0; i < Status.Count; i++)
                if (Status[i].Dauer > 0)
                {
                    Status[i].Dauer--;
                    if (Status[i].Dauer == 0)
                    {
                        StatusEntfernen(i);
                        i--;
                    }
                }

            for (int i = 0; i < Upgrades.Count; i++)
                if (Upgrades[i].Dauer > 0)
                {
                    Upgrades[i].Dauer--;
                    if (Upgrades[i].Dauer == 0)
                    {
                        UpgradeEntfernen(i);
                        i--;
                    }
                }
        }

        public List<String> Speichern()
        {
            var data = new List<String>();
            data.Add("[EFFECTPACKET]");
            data.Add("[STATUS]");
            for (int i = 0; i < Status.Count; i++)
                data.AddRange(Status[i].Speichern());
            data.Add("[/STATUS]");

            data.Add("[UPGRADES]");
            for (int i = 0; i < Status.Count; i++)
                data.AddRange(Upgrades[i].Speichern());
            data.Add("[/UPGRADES]");

            data.Add("[KONSUMIERBARES]");
            for (int i = 0; i < Status.Count; i++)
                data.AddRange(Konsumierbares[i].Speichern());
            data.Add("[/KONSUMIERBARES]");

            data.AddRange(EffektSumme.Speichern());

            data.Add("[/EFFECTPACKET]");
            return data;
        }

        /// <summary>
        ///     Statuses the entfernen.
        /// </summary>
        /// <param name="Objekt">The objekt.</param>
        public void StatusEntfernen(Effekt Objekt) // Entfernt einen Status
        {
            int i = Status.IndexOf(Objekt);
            StatusEntfernen(i);
        }

        /// <summary>
        ///     Statuses the entfernen.
        /// </summary>
        /// <param name="id">The id.</param>
        public void StatusEntfernen(int id) // Entfernt einen Status
        {
            if (id < 0 || id >= Status.Count) return;
            EffektSumme.Subtrahieren(Status[id]);
            Status.RemoveAt(id);
        }

        /// <summary>
        ///     Upgrades the entfernen.
        /// </summary>
        /// <param name="Objekt">The objekt.</param>
        public void UpgradeEntfernen(Effekt Objekt) // Entfernt ein Upgrade
        {
            int i = Upgrades.IndexOf(Objekt);
            UpgradeEntfernen(i);
        }

        /// <summary>
        ///     Upgrades the entfernen.
        /// </summary>
        /// <param name="id">The id.</param>
        public void UpgradeEntfernen(int id) // Entfernt ein Upgrade
        {
            if (id < 0 || id >= Upgrades.Count) return;
            EffektSumme.Subtrahieren(Upgrades[id]);
            Upgrades.RemoveAt(id);
        }

        #endregion Methods

        // Ist die aufaddierung aller Effekte in diesem Packet
    }
}