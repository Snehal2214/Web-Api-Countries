﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Models
{
    public class SubRegions
    {
        public Name name { get; set; }
        public string[] tld { get; set; }
        public string cca2 { get; set; }
        public string ccn3 { get; set; }
        public string cca3 { get; set; }
        public string cioc { get; set; }
        public bool independent { get; set; }
        public string status { get; set; }
        public bool unMember { get; set; }
        public Currencies currencies { get; set; }
        public Idd idd { get; set; }
        public string[] capital { get; set; }
        public string[] altSpellings { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public Languages languages { get; set; }
        public Translations translations { get; set; }
        public float[] latlng { get; set; }
        public bool landlocked { get; set; }
        public string[] borders { get; set; }
        public float area { get; set; }
        public Demonyms demonyms { get; set; }
        public string flag { get; set; }
        public Maps maps { get; set; }
        public int population { get; set; }
        public Gini gini { get; set; }
        public string fifa { get; set; }
        public Car car { get; set; }
        public string[] timezones { get; set; }
        public string[] continents { get; set; }
        public Flags flags { get; set; }
        public Coatofarms coatOfArms { get; set; }
        public string startOfWeek { get; set; }
        public Capitalinfo capitalInfo { get; set; }
        public Postalcode postalCode { get; set; }
    }

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }
        public Nativename nativeName { get; set; }
    }

    public class Nativename
    {
        public Nno nno { get; set; }
        public Nob nob { get; set; }
        public Smi smi { get; set; }
        public Eng eng { get; set; }
        public Fra fra { get; set; }
        public Nfr nfr { get; set; }
        public Swe swe { get; set; }
        public Nor nor { get; set; }
        public Nrf nrf { get; set; }
        public Dan dan { get; set; }
        public Fao fao { get; set; }
        public Isl isl { get; set; }
        public Lav lav { get; set; }
        public Glv glv { get; set; }
        public Est est { get; set; }
        public Lit lit { get; set; }
        public Fin fin { get; set; }
        public Gle gle { get; set; }
    }

    public class Nno
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Nob
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Smi
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Eng
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Fra
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Nfr
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Swe
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Nor
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Nrf
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Dan
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Fao
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Isl
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Lav
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Glv
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Est
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Lit
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Fin
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Gle
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Currencies
    {
        public NOK NOK { get; set; }
        public GBP GBP { get; set; }
        public GGP GGP { get; set; }
        public EUR EUR { get; set; }
        public SEK SEK { get; set; }
        public JEP JEP { get; set; }
        public DKK DKK { get; set; }
        public FOK FOK { get; set; }
        public ISK ISK { get; set; }
        public IMP IMP { get; set; }

    }

    public class NOK
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class GBP
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class GGP
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class EUR
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class SEK
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class JEP
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class DKK
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class FOK
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class ISK
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class IMP
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class Idd
    {
        public string root { get; set; }
        public string[] suffixes { get; set; }
    }

    public class Languages
    {
        public string nno { get; set; }
        public string nob { get; set; }
        public string smi { get; set; }
        public string eng { get; set; }
        public string fra { get; set; }
        public string nfr { get; set; }
        public string swe { get; set; }
        public string nor { get; set; }
        public string nrf { get; set; }
        public string dan { get; set; }
        public string fao { get; set; }
        public string isl { get; set; }
        public string lav { get; set; }
        public string glv { get; set; }
        public string est { get; set; }
        public string lit { get; set; }
        public string fin { get; set; }
        public string gle { get; set; }
    }

    public class Translations
    {
        public Ara ara { get; set; }
        public Bre bre { get; set; }
        public Ces ces { get; set; }
        public Cym cym { get; set; }
        public Deu deu { get; set; }
        public Est1 est { get; set; }
        public Fin1 fin { get; set; }
        public Fra1 fra { get; set; }
        public Hrv hrv { get; set; }
        public Hun hun { get; set; }
        public Ita ita { get; set; }
        public Jpn jpn { get; set; }
        public Kor kor { get; set; }
        public Nld nld { get; set; }
        public Per per { get; set; }
        public Pol pol { get; set; }
        public Por por { get; set; }
        public Rus rus { get; set; }
        public Slk slk { get; set; }
        public Spa spa { get; set; }
        public Srp srp { get; set; }
        public Swe1 swe { get; set; }
        public Tur tur { get; set; }
        public Urd urd { get; set; }
        public Zho zho { get; set; }
    }

    public class Ara
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Bre
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Ces
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Cym
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Deu
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Est1
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Fin1
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Fra1
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Hrv
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Hun
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Ita
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Jpn
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Kor
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Nld
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Per
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Pol
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Por
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Rus
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Slk
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Spa
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Srp
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Swe1
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Tur
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Urd
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Zho
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Demonyms
    {
        public Eng1 eng { get; set; }
        public Fra2 fra { get; set; }
    }

    public class Eng1
    {
        public string f { get; set; }
        public string m { get; set; }
    }

    public class Fra2
    {
        public string f { get; set; }
        public string m { get; set; }
    }

    public class Maps
    {
        public string googleMaps { get; set; }
        public string openStreetMaps { get; set; }
    }

    public class Gini
    {
        public float _2018 { get; set; }
        public float _2017 { get; set; }
    }

    public class Car
    {
        public string[] signs { get; set; }
        public string side { get; set; }
    }

    public class Flags
    {
        public string png { get; set; }
        public string svg { get; set; }
        public string alt { get; set; }
    }

    public class Coatofarms
    {
        public string png { get; set; }
        public string svg { get; set; }
    }

    public class Capitalinfo
    {
        public float[] latlng { get; set; }
    }

    public class Postalcode
    {
        public string format { get; set; }
        public string regex { get; set; }
    }

}