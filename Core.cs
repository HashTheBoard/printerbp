using BrokeProtocol.API;
using System;
using Newtonsoft.Json;
using BrokeProtocol.Managers;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Printer
{
    public class Core : Plugin
    {
        public Core()
        {
            Info = new PluginInfo("Printer system", "print") {
                Description = "a simple printer system make by ya80",
                Website = "https://terratech-heberg.fr"
            };
        }  
    }

    class PluginInfos
    {
        [JsonProperty("Time")]
        public int Time { get; set; }
        [JsonProperty("Price Tier 1")]
        public int Prices1 { get; set; }
        [JsonProperty("Price Tier 2")]
        public int Prices2 { get; set; }
        [JsonProperty("Price Tier 3")]
        public int Prices3 { get; set; }
        [JsonProperty("Price Tier 4")]
        public int Prices4 { get; set; }
        [JsonProperty("Take Money")]
        public string TakeMoney { get; set; }
        [JsonProperty("Upgrade Message")]
        public string Upgrade { get; set; }
        [JsonProperty("Not Enought Money")]
        public string MoneyEnought { get; set; }
        [JsonProperty("Printer Activated")]
        public string StartPrint { get; set; }
        [JsonProperty("Printer Disabled")]
        public string DisablePrint { get; set; }
        [JsonProperty("Money Received")]
        public string MoneyReceived { get; set; }
        [JsonProperty("Won Money Tier1")]
        public int Tier1Money { get; set; }
        [JsonProperty("Won Money Tier2")]
        public int Tier2Money { get; set; }
        [JsonProperty("Won Money Tier3")]
        public int Tier3Money { get; set; }
        [JsonProperty("Won Money Tier4")]
        public int Tier4Money { get; set; }

        [JsonProperty("Start Printer")]
        public string StartPrinter { get; set; }
        [JsonProperty("My Money")]
        public string Mymoney1 { get; set; }
        [JsonProperty("Upgrade")]
        public string Upgrade1 { get; set; }

        [JsonProperty("your printer has no money")]
        public string ppmoney { get; set; }
        [JsonProperty("you have already that")]
        public string alreadyt { get; set; }
        [JsonProperty("remove tier")]
        public string rmvtiere { get; set; }

        [JsonProperty("you removed tier")]
        public string resettier { get; set; }
        [JsonProperty("You have ... money")]
        public string yourmoney { get; set; }
        [JsonProperty("Default tier money")]
        public int defaultm { get; set; }
        [JsonProperty("you don't have vip")]
        public string msgvip { get; set; }
    }

    class Config
    {
        [Target(GameSourceEvent.ManagerStart, ExecutionMode.Event)]
        public void OnStart(SvManager svManager)
        {
            if (!Directory.Exists("./PrinterConfig")) { Debug.Log("Generating Printer Config Folder..."); Directory.CreateDirectory("./PrinterConfig"); }

            if (!File.Exists("./PrinterConfig/config.json"))
            {
                Debug.Log("Writting Config File...");
                File.WriteAllText("./PrinterConfig/config.json", "[]");
                var data = Config.getJsonInfos();
                data.Add(new PluginInfos()
                {
                    Time = 60,
                    Prices1 = 150,
                    Prices2 = 500,
                    Prices3 = 1000,
                    Prices4 = 1500,
                    TakeMoney = "Take money",
                    Upgrade = "You've upgrade your printer at level",
                    MoneyEnought = "You don't have enought money !",
                    StartPrint = "Printer activated !",
                    DisablePrint = "Printer disabled !",
                    MoneyReceived = "You just received your printer money !",
                    Tier1Money = 50,
                    Tier2Money = 150,
                    Tier3Money = 350,
                    Tier4Money = 1000,
                    StartPrinter = "start printer",
                    Mymoney1 = "my money",
                    Upgrade1 = "upgrade",
                    ppmoney = "your printer has no money",
                    alreadyt = "you have already that",
                    rmvtiere = "remove tier",
                    resettier = "you removed you tier, you can buy another now !",
                    yourmoney = "this is your money",
                    defaultm = 10,
                    msgvip = "you don't have vip"
                    
                }) ;
                File.WriteAllText("./PrinterConfig/config.json", JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }

        public static List<PluginInfos> getJsonInfos()
        {
            try {
                using (StreamReader r = new StreamReader("./PrinterConfig/config.json")) {
                    return JsonConvert.DeserializeObject<List<PluginInfos>>(r.ReadToEnd());
                }
            }
            catch (Exception ex) {
                Debug.Log(ex);
            }

            return null;
        }
    }
}