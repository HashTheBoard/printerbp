using BrokeProtocol.API;
using BrokeProtocol.Entities;
using BrokeProtocol.Required;
using System.Collections.Generic;

namespace Printer
{
    class Event
    {
        private PluginInfos getPluginInfos()
        {
            foreach (PluginInfos infos in Config.getJsonInfos())
                return infos;

            return null;
        }

        [CustomTarget]
        public void Eventhandler(ShEntity target, ShPlayer caller)
        {
            List<LabelID> option = new List<LabelID>();
            option.Add(new LabelID(getPluginInfos().StartPrinter, "startingprint"));
            option.Add(new LabelID(getPluginInfos().Mymoney1, "MyMoney"));
            option.Add(new LabelID(getPluginInfos().Upgrade1, "MyGrade"));
            option.Add(new LabelID("&3[ Make By Ya80 ]", "MyOptionP"));
            caller.svPlayer.SendOptionMenu("Printer", caller.ID, "PrinterGo", option.ToArray(), new LabelID[1] { new LabelID("Choisir", "choose") }, 0.25f, 0.1f, 0.75f, 0.9f);
        }


    }
}
