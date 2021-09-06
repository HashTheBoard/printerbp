using BrokeProtocol.API;
using BrokeProtocol.Entities;
using BrokeProtocol.Required;
using BrokeProtocol.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Printer
{
    internal class Utils
    {
        private List<ShPlayer> keys = new List<ShPlayer>();
        private Dictionary<ShPlayer, int> printvalue = new Dictionary<ShPlayer, int>();
        private Dictionary<ShPlayer, int> printgrade = new Dictionary<ShPlayer, int>();

        [Target(GameSourceEvent.PlayerOptionAction, ExecutionMode.Event)]
        public void OpenUI(ShPlayer player, int targetID, string menuID, string optionID, string actionID)
        {
            if (menuID == "PrinterGo" || menuID == "moneym" || menuID == "PrintGrade")
            {
                switch (optionID)
                {
                    case "startingprint":
                        if (keys.Contains(player))
                        {
                            player.svPlayer.SendGameMessage("Printer Toggle off");
                            keys.Remove(player);
                            player.svPlayer.StopCoroutine(Coroutine(player));
                        }
                        else
                        {
                            keys.Add(player);
                            player.svPlayer.StartCoroutine(Coroutine(player));
                        }
                        break;

                    case "MyMoney":
                        int money;
                        printvalue.TryGetValue(player, out money);
                        List<LabelID> option = new List<LabelID>();
                        option.Add(new LabelID(getPluginInfos().TakeMoney, "1"));

                        player.svPlayer.SendOptionMenu(getPluginInfos().yourmoney + " " + money.ToString() + "$", player.ID, "moneym", option.ToArray(), new LabelID[1] { new LabelID("Choisir", "choose") }, 0.25f, 0.1f, 0.75f, 0.9f);
                        break;

                    case "1":
                        int moneyz;
                        printvalue.TryGetValue(player, out moneyz);
                        printvalue.Remove(player);
                        if (moneyz == 0)
                        {
                            player.svPlayer.SendGameMessage(getPluginInfos().ppmoney);
                        }
                        else
                        {
                            player.TransferMoney(DeltaInv.AddToMe, moneyz);
                        }

                        break;

                    case "MyGrade":
                        List<LabelID> optione = new List<LabelID>();
                        optione.Add(new LabelID("[&6Tier 1] " + getPluginInfos().Prices1.ToString() + "$", "6"));
                        optione.Add(new LabelID("[&4Tier 2] " + getPluginInfos().Prices2.ToString() + "$", "2"));
                        optione.Add(new LabelID("[&2Tier 3] " + getPluginInfos().Prices3.ToString() + "$", "3"));
                        optione.Add(new LabelID("[&2Tier 4] VIP " + getPluginInfos().Prices4.ToString() + "$", "4"));
                        optione.Add(new LabelID(getPluginInfos().rmvtiere, "rmvtier"));
                        player.svPlayer.SendOptionMenu("Printer", player.ID, "PrintGrade", optione.ToArray(), new LabelID[1] { new LabelID("Choisir", "choose") }, 0.25f, 0.1f, 0.75f, 0.9f);
                        break;

                    case "6":
                        int earn = getPluginInfos().Tier1Money;
                        int price = getPluginInfos().Prices1;
                        if (!printgrade.ContainsKey(player))
                        {
                            if (player.MyMoneyCount >= price)
                            {
                                player.TransferMoney(DeltaInv.RemoveFromMe, price);
                                printgrade.Add(player, earn);
                                player.svPlayer.SendGameMessage($"{getPluginInfos().Upgrade}  1");
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(getPluginInfos().MoneyEnought);
                            }
                        }
                        else
                        {
                            player.svPlayer.SendGameMessage(getPluginInfos().alreadyt);
                        }
                        break;

                    case "2":
                        int earn2 = getPluginInfos().Tier2Money;
                        int price2 = getPluginInfos().Prices2;
                        if (!printgrade.ContainsKey(player))
                        {
                            if (player.MyMoneyCount >= price2)
                            {
                                player.TransferMoney(DeltaInv.RemoveFromMe, price2);
                                printgrade.Add(player, earn2);
                                player.svPlayer.SendGameMessage(getPluginInfos().Upgrade + " 2");
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(getPluginInfos().MoneyEnought);
                            }
                        }
                        else
                        {
                            player.svPlayer.SendGameMessage(getPluginInfos().alreadyt);
                        }
                        break;

                    case "3":
                        int earn3 = getPluginInfos().Tier3Money;
                        int price3 = getPluginInfos().Prices3;
                        if (!printgrade.ContainsKey(player))
                        {
                            if (player.MyMoneyCount >= price3)
                            {
                                player.TransferMoney(DeltaInv.RemoveFromMe, price3);
                                printgrade.Add(player, earn3);
                                player.svPlayer.SendGameMessage(getPluginInfos().Upgrade + " 3");
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(getPluginInfos().MoneyEnought);
                            }
                        }
                        else
                        {
                            player.svPlayer.SendGameMessage(getPluginInfos().alreadyt);
                        }
                        break;
                    case "4":
                        int earn4 = getPluginInfos().Tier3Money;
                        int price4 = getPluginInfos().Prices3;
                        if (!printgrade.ContainsKey(player))
                        {
                            if (player.svPlayer.HasPermission("print.vip"))
                            {
                                if (player.MyMoneyCount >= price4)
                                {
                                    player.TransferMoney(DeltaInv.RemoveFromMe, price4);
                                    printgrade.Add(player, earn4);
                                    player.svPlayer.SendGameMessage(getPluginInfos().Upgrade + " 4");
                                }
                                else
                                {
                                    player.svPlayer.SendGameMessage(getPluginInfos().MoneyEnought);
                                }
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(getPluginInfos().msgvip);
                            }
                        }
                        else
                        {
                            player.svPlayer.SendGameMessage(getPluginInfos().alreadyt);
                        }
                        break;
                    case "rmvtier":
                        printgrade.Remove(player);
                        player.svPlayer.SendGameMessage(getPluginInfos().resettier);
                        break;
                }
            }
        }

        private PluginInfos getPluginInfos()
        {
            foreach (PluginInfos infos in Config.getJsonInfos())
                return infos;

            return null;
        }

        public IEnumerator Coroutine(ShPlayer player)
        {
            int tier;
            printgrade.TryGetValue(player, out tier);
            if (!printgrade.ContainsKey(player)) tier = getPluginInfos().defaultm;
            player.svPlayer.SendGameMessage(getPluginInfos().StartPrint);

            while (keys.Contains(player))
            {
                int argent;
                printvalue.TryGetValue(player, out argent);
                yield return new WaitForSecondsRealtime(getPluginInfos().Time);
                argent += tier;
                printvalue.Remove(player);
                printvalue.Add(player, argent);
            }
            yield break;
        }
    }
}