using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Models
{
    internal class ScheduleElement :IConvertable
    {
      

        public int PartyId { get; set; }

        public int MachineToolsId { get; set; }
        public string NomenclatureName { get; set; }
        public string ToolName { get; set; }
        public int StartTime { get; set; }
        public int StopTime { get; set; }
        public string[] ToArray()
        {
            return new string[] { PartyId.ToString(), MachineToolsId.ToString(), NomenclatureName, ToolName, StartTime.ToString(), StopTime.ToString() };
        }
        public ScheduleElement(int partyId,string nomenclatureName, string toolName, int machineToolsId, int startTime, int stopTime)
        {
            PartyId = partyId;
            MachineToolsId = machineToolsId;
            NomenclatureName = nomenclatureName;
            ToolName = toolName;
            StartTime = startTime;
            StopTime = stopTime;
        }


    }
}
