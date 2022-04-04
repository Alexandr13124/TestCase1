using System;
using System.Collections.Generic;
using System.Text;
using TestCase1.FileHandling;
using TestCase1.Models;

namespace TestCase1.Repositories
{
    class MachineToolRepository: IReadable<MachineTool>
    {
        IFileHandler fileHandler;

        public MachineToolRepository(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public List<MachineTool> GetAll()
        {
            List<MachineTool> machineTools= new List<MachineTool>();

            var stringMachineTools = fileHandler.ReadFile(FileConfig.MachineToolsFile);

            foreach (var element in stringMachineTools)
            {
                machineTools.Add(new MachineTool(element));
            }

            return machineTools;
        }
    }
}
