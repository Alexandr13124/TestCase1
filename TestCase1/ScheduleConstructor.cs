using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCase1.Repositories;

namespace TestCase1.Models
{
    class ScheduleConstructor
    {
        private IReadable<Party> PartyRepository { get;set; }
        private IReadable<Nomenclature> NomenclatureRepository { get; set; }
        private IReadable<MachineTool> MachineToolRepository { get; set; }
        private IReadable<Time> TimeRepository { get; set; }
        private IWriteable<ScheduleElement> ScheduleElementRepository { get; set; }
      

        public ScheduleConstructor(PartyRepository partyRepository, NomenclatureRepository nomenclatureRepository, 
            MachineToolRepository machineToolRepository, TimeRepository timeRepository, ScheduleElementRepository elementRepository)
        {
            PartyRepository = partyRepository;
            NomenclatureRepository = nomenclatureRepository;
            MachineToolRepository = machineToolRepository;
            TimeRepository = timeRepository;
            ScheduleElementRepository = elementRepository;
        }

        public void CreateSchedule()
        {
           
            var schedule = ConstructSchedule();
            ScheduleElementRepository.Write(schedule);
        }

        private List<ScheduleElement> ConstructSchedule()
        {
            List<List<Time>> sortedQueue = new List<List<Time>>();
            List<int> stopTimes = new List<int>();

            List<Time> times = TimeRepository.GetAll().OrderByDescending(e => e.OperationTime).ToList();
            List<MachineTool> machineTools = MachineToolRepository.GetAll();
            List<Party> parties = PartyRepository.GetAll();
            List<Nomenclature> nomenclatures = NomenclatureRepository.GetAll();

            for (int i = 0; i < machineTools.Count; i++)
            {
                sortedQueue.Add(times.Where(e => e.MachineToolId == machineTools[i].MachineToolId).ToList());
                stopTimes.Add(0);
            }
          
            foreach (var party in parties)
            {
                party.Nomenclature = nomenclatures.FirstOrDefault(e => e.NomenclatureId == party.NomenclatureId);
            }

            return CreateScheduleElements(sortedQueue , parties, machineTools, stopTimes);
        }

        private List<ScheduleElement> CreateScheduleElements(List<List<Time>> sortedQueue, List<Party> parties,List<MachineTool> machineTools,
            List<int> stopTimes)
        {
            List<ScheduleElement> scheduleElements = new List<ScheduleElement>();
            int index = 0;

            while (sortedQueue[index].Count > 0)
            {
                Party party = parties.FirstOrDefault(e => e.NomenclatureId == sortedQueue[index][0].NomenclatureId);
                if (party != null)
                {
                    int startTime = stopTimes[index];

                    if (party != null)
                    {
                        stopTimes[index] = startTime + sortedQueue[index][0].OperationTime;

                        ScheduleElement scheduleElement = new ScheduleElement(party.PartyId, party.Nomenclature.Name, machineTools[index].Name, 
                            sortedQueue[index][0].MachineToolId, startTime, stopTimes[index]);

                        scheduleElements.Add(scheduleElement);
                        parties.Remove(party);

                    }

                    party = parties.FirstOrDefault(e => e.NomenclatureId == sortedQueue[index][0].NomenclatureId);
                }
                else
                {
                    sortedQueue[index].RemoveAt(0);
                    if (sortedQueue[index].Count == 0)
                    {
                        stopTimes[index] = Int32.MaxValue;
                    }
                }

                index = stopTimes.IndexOf(stopTimes.Min());
            }

            return scheduleElements;
        }
    }
}
